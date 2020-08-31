using ShipperToQAD.Entity;
using ShipperToQAD.QADShipper;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD
{
    class Program
    {
        static IFreeSql fsql = null;

        static void Main(string[] args)
        {
            fsql = new FreeSql.FreeSqlBuilder()
                 .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                 // .UseMonitorCommand(o => Console.WriteLine(o.CommandText), (o, p) => Console.WriteLine(o.CommandText))
                 .Build(); //请务必定义成 Singleton 单例模式

            Implement(fsql);
        }

        private static void Log(string key, string xml, string error)
        {
            using (var uom = fsql.CreateUnitOfWork())
            {
                var log = fsql.Insert<SYS_QAD_OUTBOUND_LOG>().AppendData(new SYS_QAD_OUTBOUND_LOG
                {
                    FID = Guid.NewGuid(),
                    METHORD_NAME = "SEND TO ESB",
                    EXECUTE_START_TIME = DateTime.Now,
                    EXECUTE_END_TIME = DateTime.Now,
                    EXECUTE_RESULT = string.IsNullOrEmpty(error) ? 1 : 2,
                    KEY_VALUE = key,
                    SOURCE_XML = xml,
                    ERROR_DESCRIPTION = error,
                    EXECUTE_TIMES = 1,
                    VALID_FLAG = true,
                    CREATOR = "SHIPPERSERVICE",

                }).ExecuteInserted();
                uom.Commit();
            }
        }

        private static void Implement(IFreeSql fsql)
        {
            // 获取已完成未同步装车单
            var loadings = fsql.GetRepository<LOADING_LIST>()
                .Where(o => o.STATUS == 40 &&
                    (string.IsNullOrEmpty(o.REMARK)
                        // || o.REMARK.Equals("ESB_FAIL", StringComparison.OrdinalIgnoreCase)
                        || o.REMARK.Equals("ESB_RESEND", StringComparison.OrdinalIgnoreCase)))
                .IncludeMany(o => o.LOADING_LIST_DETAILS.Where(p => o.FID == p.LOADING_LIST_FID))
                .ToList();

            var opcsOrm = fsql.GetRepository<OPCS>();
            // 遍历装车单
            foreach (LOADING_LIST loading in loadings)
            {
                int index = 1;

                List<INSEQShipperType> iNSEQShipperTypes = new List<INSEQShipperType>();
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Start to send truck {loading.LOADING_LIST_CODE}");
                // 获取装车单明细
                foreach (LOADING_LIST_DETAIL loadingDetail in loading.LOADING_LIST_DETAILS)
                {
                    // ruck
                    var shipping = fsql.GetRepository<SHIPPING>().Where(o => loadingDetail.SHIPPING_FID == o.FID)
                        .IncludeMany(o => o.SHIPPING_DETAILs.Where(p => p.SHIPPING_FID == o.FID))
                        .First();

                    var part_shipping = fsql.GetRepository<PART_SHIPPING>().Where(o => o.FID == shipping.PART_SHIPPING_FID)
                        .IncludeMany(o => o.pART_SHIPPING_DETAILs.Where(p => p.PART_SHIPPING_FID == o.FID))
                        .First();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Start to send ruck {shipping.SHIPPING_CODE}");
                    // 遍历ruck明细
                    foreach (SHIPPING_DETAIL detail in shipping.SHIPPING_DETAILs)
                    {
                        var opcs = opcsOrm.Where(o => o.CARSEQUENCE == detail.CUST_INFO_SEQ).First();
                        var shipping_detail = part_shipping.pART_SHIPPING_DETAILs.FirstOrDefault(o => o.CUST_PART_NO.Equals(detail.CUST_PART_NO, StringComparison.OrdinalIgnoreCase));
                        // 判断发运明细是否需要发运，以FRAME_AGREEMENT_CODE作为判断依据
                        if (shipping_detail == null || !shipping_detail.FRAME_AGREEMENT_CODE.Equals("1066", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        // 添加到同步列表
                        iNSEQShipperTypes.Add(new INSEQShipperType
                        {
                            SERL_NBR_TYPE = "PRT", // RCK IS THE VALUE OF THE SERIAL TYPE LEVEL 1
                            SERL_NBR = detail.ID.ToString(), // INTERNAL IPC SERIAL, WILL NOT BE TRANSFERRED TO QAD. what's this field's meaning.
                            SERL_QTY = detail.ACTUAL_QTY.ToString(), // QTY OF ITEM BASED ON THE VIN DETAIL
                            LIN_CUST_ITEM = detail.CUST_PART_NO, // CUSTOMER ITEM IN THE BROADCAST
                            LIN_VIN = opcs.VIN, // VIN DETAIL ON THE BROADCAST. 

                            LIN_PO_NBR = string.Empty, // define as empty

                            RECID_PKG = "1", // PACKSLIP RANGE DEFINED FOR HIGHLAND PARK
                            PARENTRECID_PKG = "1", // PACKSLIP RANGE DEFINED FOR HIGHLAND PARK
                            RECID_PRT = index++.ToString(), // BOL ALWAYS EQUALS THE PACKSLIP IN QAD

                            LIN_JOB_SEQ_A = opcs.CARSEQUENCE.ToString(), // CUSTOMER BROADCAST SEQUENCE
                            LIN_JOB_SEQ_B = string.Empty, // define as empty
                        });
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Add shipper detail {part_shipping.PART_SHIPPING_CODE} - {opcs.VIN}");
                    }
                }

                INSEQShipper iNSEQShipper = new INSEQShipper
                {
                    VIA_CODE = "CTIIM", // THIS FIELD IS A HARDCODED VALUE
                    TRUCK_ID = "1234", // THIS FIELD IS A HARDCODED VALUE
                    BOL_NBR = loading.LOADING_LIST_CODE, // Pisces shipper order number
                    PCK_SLP_NBR = loading.LOADING_LIST_CODE, // Pisces shipper order number
                    SITE_NBR = "1066", // YFAI SITE CODE IN QAD
                    SHIP_TS = loading.END_SCAN_TIME.Value.ToString("yyyy.MM.ddTHH:mm:ss"), // "2019.08.12T17:57:06 GMT-05:00",
                    ADDR_CODE = loading.CUST_CODE, // QAD SHIP-TO ADDRESS
                    SERL_NBR = loading.ID.ToString(), // INTERNAL IPC SERIAL, WILL NOT BE TRANSFERRED TO QAD. what's this field's meaning.
                    INSEQShippers = iNSEQShipperTypes.ToArray(),
                };


                try
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE}");
                    var proxy = new ServiceINSEQShipperHttpService();
                    proxy.Timeout = 1000 * 30;
                    var response = proxy.INSEQShipper(iNSEQShipper);
                    if (response.Result == "SUCCESS")
                    {
                        Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), string.Empty);

                        loading.REMARK = "ESB_SUCCESS";
                        fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Success.");
                    }
                    else
                    {
                        Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), response.Exceptions?.ERR_MSG.Msg_Desc);
                        loading.REMARK = "ESB_FAIL";
                        fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Fail.");
                    }
                }
                catch (Exception ex)
                {
                    Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), ex.Message);
                    loading.REMARK = "ESB_FAIL";
                    fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Success.");
                }
            }
        }
    }
}
