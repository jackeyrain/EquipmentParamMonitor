using ShipperToQAD.Entity;
using ShipperToQAD.QADShipper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ShipperToQAD
{
    class Program
    {
        static IFreeSql fsql = null;
        static List<string> LaborPartCategory = null;
        static string LaborPartNumber = string.Empty;

        static void Main(string[] args)
        {
            fsql = new FreeSql.FreeSqlBuilder()
                 .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                 // .UseMonitorCommand(o => Console.WriteLine(o.CommandText), (o, p) => Console.WriteLine(o.CommandText))
                 .Build();

            LaborPartCategory = new List<string>();
            LaborPartCategory.AddRange(ConfigurationManager.AppSettings["LaborPartCategory"]?.Split(new[] { ',' }));
            LaborPartNumber = ConfigurationManager.AppSettings["LaborPartNumber"];

            //请务必定义成 Singleton 单例模式
            //#if DEBUG
            //            var obj = EntityExtend.Deserial<INSEQShipper>("<INSEQShipper><VIA_CODE>CTIIM</VIA_CODE><TRUCK_ID>1234</TRUCK_ID><BOL_NBR>N8LAL83100023</BOL_NBR><PCK_SLP_NBR>N8LAL83100023</PCK_SLP_NBR><SITE_NBR>1066</SITE_NBR><SHIP_TS>2020.08.31T04:09:49</SHIP_TS><ADDR_CODE>81560059</ADDR_CODE><SERL_NBR>53</SERL_NBR><INSEQShippers><INSEQShipper><SERL_NBR_TYPE>PRT</SERL_NBR_TYPE><SERL_NBR>23715</SERL_NBR><SERL_QTY>1</SERL_QTY><LIN_CUST_ITEM>6FK871X7AG</LIN_CUST_ITEM><LIN_VIN>151329</LIN_VIN><LIN_PO_NBR /><RECID_PKG>1</RECID_PKG><PARENTRECID_PKG>1</PARENTRECID_PKG><RECID_PRT>1</RECID_PRT><LIN_JOB_SEQ_A>4863490</LIN_JOB_SEQ_A><LIN_JOB_SEQ_B /></INSEQShipper><INSEQShipper><SERL_NBR_TYPE>PRT</SERL_NBR_TYPE><SERL_NBR>23716</SERL_NBR><SERL_QTY>1</SERL_QTY><LIN_CUST_ITEM>6FK871X7AG</LIN_CUST_ITEM><LIN_VIN>153550</LIN_VIN><LIN_PO_NBR /><RECID_PKG>1</RECID_PKG><PARENTRECID_PKG>1</PARENTRECID_PKG><RECID_PRT>2</RECID_PRT><LIN_JOB_SEQ_A>4863500</LIN_JOB_SEQ_A><LIN_JOB_SEQ_B /></INSEQShipper><INSEQShipper><SERL_NBR_TYPE>PRT</SERL_NBR_TYPE><SERL_NBR>23717</SERL_NBR><SERL_QTY>1</SERL_QTY><LIN_CUST_ITEM>6FK881X7AG</LIN_CUST_ITEM><LIN_VIN>154618</LIN_VIN><LIN_PO_NBR /><RECID_PKG>1</RECID_PKG><PARENTRECID_PKG>1</PARENTRECID_PKG><RECID_PRT>3</RECID_PRT><LIN_JOB_SEQ_A>4863510</LIN_JOB_SEQ_A><LIN_JOB_SEQ_B /></INSEQShipper></INSEQShippers></INSEQShipper>");
            //            obj.BOL_NBR = AdjustShipperNumber(long.Parse(obj.SERL_NBR));
            //            obj.PCK_SLP_NBR = obj.BOL_NBR;
            //            var proxy = new ServiceINSEQShipperHttpService();
            //            proxy.Timeout = 1000 * 60;
            //            var response = proxy.INSEQShipper(obj);
            //#endif
            try
            {
                Implement(fsql);
            }
            catch (Exception ex)
            {
                Log("SEND TO ESB", ex.Message, 4, "InternalError");
            }
        }

        private static void Log(string key, string xml, int result, string error)
        {
            using (var uom = fsql.CreateUnitOfWork())
            {
                var log = fsql.Insert<SYS_QAD_OUTBOUND_LOG>().AppendData(new SYS_QAD_OUTBOUND_LOG
                {
                    FID = Guid.NewGuid(),
                    METHORD_NAME = "SEND TO ESB",
                    EXECUTE_START_TIME = DateTime.Now,
                    EXECUTE_END_TIME = DateTime.Now,
                    EXECUTE_RESULT = result,
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
                //.Where(o=> o.ID == 70)
                .IncludeMany(o => o.LOADING_LIST_DETAILS.Where(p => o.FID == p.LOADING_LIST_FID))
                .ToList();

            var opcsOrm = fsql.GetRepository<OPCS>();
            // 遍历装车单
            foreach (LOADING_LIST loading in loadings)
            {
                int index = 1;
                // 获取年度标识
                List<CIM_VEHICLE_CATEGORY> cIM_VEHICLE_CATEGORies = fsql.GetRepository<CIM_VEHICLE_CATEGORY>().Where(o => o.VALID_FLAG).ToList();

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
                        // var modelYear = cIM_VEHICLE_CATEGORies.FirstOrDefault(o => o.VEHICLE_YEAR.Equals(opcs.MODELYEAR, StringComparison.OrdinalIgnoreCase));
                        var shipping_detail = part_shipping.pART_SHIPPING_DETAILs.FirstOrDefault(o => o.PART_NO.Equals(detail.PART_NO, StringComparison.OrdinalIgnoreCase));
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
                            LIN_VIN = detail.LZ_VIN_CODE, // modelYear.VEHICLE_CATEGORY_CODE + opcs.VIN, // VIN DETAIL ON THE BROADCAST. 

                            LIN_PO_NBR = string.Empty, // define as empty

                            RECID_PKG = "1", // PACKSLIP RANGE DEFINED FOR HIGHLAND PARK
                            PARENTRECID_PKG = "1", // PACKSLIP RANGE DEFINED FOR HIGHLAND PARK
                            RECID_PRT = index++.ToString(), // BOL ALWAYS EQUALS THE PACKSLIP IN QAD

                            LIN_JOB_SEQ_A = opcs.CARSEQUENCE.ToString(), // CUSTOMER BROADCAST SEQUENCE
                            LIN_JOB_SEQ_B = string.Empty, // define as empty
                        });
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Add shipper detail {part_shipping.PART_SHIPPING_CODE} - {opcs.VIN}");

                        // 判断当前发运组是否是符合添加Labor Part规则
                        if (LaborPartCategory.Any(o => part_shipping.PART_SHIPPING_CODE.Equals(o, StringComparison.OrdinalIgnoreCase)))
                        {
                            iNSEQShipperTypes.Add(new INSEQShipperType
                            {
                                SERL_NBR_TYPE = "PRT", // RCK IS THE VALUE OF THE SERIAL TYPE LEVEL 1
                                SERL_NBR = detail.ID.ToString(), // INTERNAL IPC SERIAL, WILL NOT BE TRANSFERRED TO QAD. what's this field's meaning.
                                SERL_QTY = detail.ACTUAL_QTY.ToString(), // QTY OF ITEM BASED ON THE VIN DETAIL
                                LIN_CUST_ITEM = LaborPartNumber, // CUSTOMER ITEM IN THE BROADCAST
                                LIN_VIN = detail.LZ_VIN_CODE, // modelYear.VEHICLE_CATEGORY_CODE + opcs.VIN, // VIN DETAIL ON THE BROADCAST. 

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
                }

                INSEQShipper iNSEQShipper = new INSEQShipper
                {
                    VIA_CODE = "CTIIM", // THIS FIELD IS A HARDCODED VALUE
                    TRUCK_ID = "1234", // THIS FIELD IS A HARDCODED VALUE
                    BOL_NBR = AdjustShipperNumber(loading.ID), // Pisces shipper order number
                    PCK_SLP_NBR = AdjustShipperNumber(loading.ID), // Pisces shipper order number
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
                    proxy.Url = ConfigurationManager.AppSettings["esb"];
                    proxy.Timeout = 1000 * 600;
                    var response = proxy.INSEQShipper(iNSEQShipper);
                    if (response.Result.Equals("success"))
                    {
                        Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), 1, string.Empty);

                        loading.REMARK = "ESB_SUCCESS";
                        fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Success.");
                    }
                    else if (response.Result.Equals("warning", StringComparison.OrdinalIgnoreCase))
                    {
                        Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), 3, response.Exceptions?.ERR_MSG.Msg_Desc);

                        loading.REMARK = "ESB_WARNING";
                        fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Success.");
                    }
                    else
                    {
                        Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), 2, response.Exceptions?.ERR_MSG.Msg_Desc);
                        loading.REMARK = "ESB_ERROR";
                        fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Fail.");
                    }
                }
                catch (Exception ex)
                {
                    Log(loading.LOADING_LIST_CODE, iNSEQShipper.ToXml(), 2, ex.Message);
                    loading.REMARK = "ESB_FAIL";
                    fsql.Update<LOADING_LIST>().SetSource(loading).ExecuteAffrows();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Send to ESB {loading.LOADING_LIST_CODE} is Success.");
                }
            }
        }

        private static string AdjustShipperNumber(long id)
        {
            var number = id;
            var pre = 36.ToString();
            var rea = (id + 6000).ToString().PadLeft(6, '0');
            var serialNumber = pre + rea;
            return serialNumber;
        }
    }
}
