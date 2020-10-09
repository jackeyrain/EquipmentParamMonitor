using DPToleranceMonitorService.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPToleranceMonitorService.Model
{
    public class ToleranceInstance
    {
        public string OPCServer { get; set; }
        public ToleranceVerifType Verification { get; set; }
        public Data[] Data { get; set; }
        public Result Result { get; set; }

        public bool CheckTolerance()
        {
            bool result = true;
            switch (Verification)
            {
                case ToleranceVerifType.NONEED:
                    return true;
                case ToleranceVerifType.RANGE:
                    Array.ForEach(Data, o =>
                    {
                        Array.ForEach(o.Tolerances, p =>
                        {
                            result &= p.ToleranceRangeCheck();
                        });
                    });
                    return result;
                case ToleranceVerifType.ACCURATE:
                    Array.ForEach(Data, o =>
                    {
                        Array.ForEach(o.Tolerances, p =>
                        {
                            result &= p.ToleranceAccurateCheck();
                        });
                    });
                    return result;
                case ToleranceVerifType.HASVALUE:
                    Array.ForEach(Data, o =>
                    {
                        Array.ForEach(o.Tolerances, p =>
                        {
                            result &= p.HasValue();
                        });
                    });
                    return result;
            }
            return true;
        }

        public bool CheckEmpty()
        {
            bool result = true;
            Array.ForEach(Data, o =>
            {
                Array.ForEach(o.Tolerances, p =>
                {
                    result &= p.HasValue();
                });
            });
            return false;
        }
    }
    public class Data
    {
        public string Area { get; set; }
        public ToleranceEntity[] Tolerances { get; set; }
    }
    public class Result
    {
        public string TagAddress { get; set; }
        public int Value { get; set; }
    }
}
