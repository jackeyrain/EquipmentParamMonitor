﻿using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace FRAEquipmentParamService.Implement
{
    public class StationMonitor : IDisposable, IStation
    {
        public StationEntity Entity { get; set; }

        private ConcurrentDictionary<string, NodeEntity> DicParamSet;
        private Jakware.UaClient.JakwareUaClient jakware;

        public StationMonitor(StationEntity entity)
        {
            LogHelper.Log.LogInfo($"{entity.Name} createad...", LogHelper.LogType.Information, false);
            this.Entity = entity;
            this.DicParamSet = new ConcurrentDictionary<string, NodeEntity>();
            this.jakware = new Jakware.UaClient.JakwareUaClient();
        }
        public void Dispose()
        {
            this.jakware?.DisConnect();
            this.DicParamSet.Clear();
            GC.Collect();
        }

        public void Initialize()
        {
            this.DicParamSet.TryAdd(Entity.PalletNode.TagAddress, Entity.PalletNode);
            this.DicParamSet.TryAdd(Entity.RunningNode.TagAddress, Entity.RunningNode);
            foreach (var param in Entity.ParamSet)
            {
                foreach (var p in param.TagAddress)
                {
                    this.DicParamSet.TryAdd(p.TagAddress, p);
                }
            }
            LogHelper.Log.LogInfo($"{Entity.Name} collection data {this.DicParamSet.Count}.", LogHelper.LogType.Information, false);

            this.jakware.connStr = this.Entity.OPCConnectionStr;
            this.jakware.Initialize();
            this.jakware.Connect();
            LogHelper.Log.LogInfo($"{Entity.Name} connect to OPC.", LogHelper.LogType.Information, false);

            this.jakware.StartSubscription();
            var tags = this.DicParamSet.Keys.ToArray();
            var nodes = tags.Select(o => NodeId.Parse($"{o}")).ToArray();
            this.jakware.AddMonitorNodeId(nodes);
            this.jakware.JakwareDataChangedEventHandler += Jakware_JakwareDataChangedEventHandler;
            LogHelper.Log.LogInfo($"{Entity.Name} initialize completed.", LogHelper.LogType.Warn, false);
        }

        public Task<int> SaveIntoDB()
        {
            return new Task<int>(() =>
            {
                try
                {
                    var data = Array.ConvertAll(this.DicParamSet.Values.ToArray(),
                    o => new MES_FRAEQUIPPARAMLOG()
                    {
                        CARRIERID = Entity.MES_TP_FRA_Pallet.PalletID,
                        WORKORDER = Entity.MES_TP_FRA_Pallet.WorkOrder,
                        SEQUENCE = Entity.MES_TP_FRA_Pallet.Sequence,
                        VINCODE = Entity.MES_TP_FRA_Pallet.Vin,
                        STATION = Entity.Name,
                        PARAMTAG = o.TagAddress,
                        VALUE = o.Value == null ? string.Empty : o.Value.ToString(),
                        CREATEDATETIME = o.CreateDT,
                    }
                    ).ToList();
                    var result = DBAccess.Instance.Insert<MES_FRAEQUIPPARAMLOG>().AppendData(data.OrderBy(o => o.PARAMTAG)).NoneParameter().ExecuteAffrows();
                    LogHelper.Log.LogInfo($"{Entity.Name} insert db success.", LogHelper.LogType.Information, false);
                    return result;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.LogInfo($"{Entity.Name} insert db fail.", LogHelper.LogType.Warn, false);
                    LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
                    return -1;
                }
            });
        }

        public Task<int> SaveIntoToBeRepaired()
        {
            return new Task<int>(() =>
            {
                try
                {
                    var firstLocation = Entity.MES_TT_APS_WORK_ORDER.mES_TT_APS_WORK_ORDER_ASSEMBLies.FirstOrDefault(o => o.LOCATION.Contains("170"))?.LOCATION;
                    LogHelper.Log.LogInfo($"Get fail resilt insert into {firstLocation}");

                    var toBeRepaired = Entity.GetFailResult();
                    LogHelper.Log.LogInfo($"Get fail quantity {toBeRepaired.Count}");

                    if (toBeRepaired.Count <= 0)
                    {
                        LogHelper.Log.LogInfo($"All the result is Good.");
                        return 0;
                    }
                    var data = Array.ConvertAll(toBeRepaired.ToArray(), o => new MES_TR_CIM_TOBE_REPAIRED
                    {
                        FID = Guid.NewGuid(),
                        ORDER_CODE = Entity.MES_TP_FRA_Pallet.WorkOrder,
                        ASSEMBLY_LINE = Entity.MES_TT_APS_WORK_ORDER.ASSEMBLY_LINE,
                        LOCATION = firstLocation,
                        DESCRIPTION = $"{this.Entity.Name}-{o.Name}",
                        RESULT = "NOK",
                        INSPECTED = false,
                        REMARK = $"{o.Name}",
                        ADD_BY_INSPECTOR = true,
                        VALID_FLAG = true,
                        CREATE_USER = AppDomain.CurrentDomain.FriendlyName,
                        CREATE_DATE = DateTime.Now,
                        DATA_SOURCE = 1,
                        REPAIRE_TYPE = 6,
                    }
                    ).ToList();
                    LogHelper.Log.LogInfo($"Ready to insert into DB repair table.");
                    var result = DBAccess.Instance.Insert<MES_TR_CIM_TOBE_REPAIRED>().AppendData(data).NoneParameter().ExecuteAffrows();
                    LogHelper.Log.LogInfo($"{Entity.Name} insert repair success.", LogHelper.LogType.Information, false);
                    return result;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.LogInfo($"{Entity.Name} insert repair fail.", LogHelper.LogType.Warn, false);
                    LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
                    return -1;
                }
            });
        }

        private async void Jakware_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            await new TaskFactory().StartNew(() =>
            {
                foreach (var item in e.JakwareDataChanges)
                {
                    if (!item.IsGood) continue;

                    var tagAddress = item.MonitoredItem.NodeId.ToString();
                    this.DicParamSet.TryGetValue(tagAddress, out var node);
                    if (node == null)
                    {
                        LogHelper.Log.LogInfo($"{Entity.Name}-{tagAddress} wasn't registed.", LogHelper.LogType.Error, false);
                        continue;
                    }

                    node.Value = item.Value.Value;
                    node.CreateDT = DateTime.Now;
                    LogHelper.Log.LogInfo($"{Entity.Name}-{tagAddress}-{node.Value?.ToString()}", LogHelper.LogType.Information, false);
                    if (node.Flag.Equals("result", StringComparison.OrdinalIgnoreCase))
                    {
                        if (node.Value.ToString() == "2")
                            LogHelper.Log.LogInfo($"{Entity.Name}-{tagAddress}-{node.Value?.ToString()}", LogHelper.LogType.Warn, false);
                    }

                    if (node.Flag.Equals("running", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(node.Value))
                        {
                            LogHelper.Log.LogInfo($"{Entity.Name} receive running START.", LogHelper.LogType.Warn, false);
                            Entity.GetPallet();
                        }
                        else
                        {
                            LogHelper.Log.LogInfo($"{Entity.Name} receive running STOP.", LogHelper.LogType.Warn, false);
                            if (Entity.MES_TP_FRA_Pallet == null)
                            {
                                LogHelper.Log.LogInfo($"{Entity.Name} Pallet Entity is NULL, Search Again.", LogHelper.LogType.Exception, false);
                                if (!Entity.GetPallet())
                                {
                                    LogHelper.Log.LogInfo($"{Entity.Name} Pallet Entity is NULL, Skip current action.", LogHelper.LogType.Exception, false);
                                    continue;
                                }
                            }

                            var t1 = SaveIntoDB();
                            var t2 = SaveIntoToBeRepaired();
                            t1.Start();
                            t2.Start();
                            Task.WaitAll(t1, t2);
                            LogHelper.Log.LogInfo($"{Entity.Name} saved parameter's quantity is {t1.Result}.", LogHelper.LogType.Warn, false);
                            LogHelper.Log.LogInfo($"{Entity.Name} saved defect's quantity is {t2.Result}.", LogHelper.LogType.Warn, false);

                            Entity.Initialize();
                            this.Reinitialize();
                        }
                    }
                }
            });
        }

        private void Reinitialize()
        {
            foreach (var item in this.DicParamSet.Values.Where(o => !o.Flag.Equals("PALLET", StringComparison.OrdinalIgnoreCase)))
            {
                item.Value = null;
            }
            LogHelper.Log.LogInfo($"{Entity.Name} reinitialize completed.", LogHelper.LogType.Information, false);
        }
    }
}
