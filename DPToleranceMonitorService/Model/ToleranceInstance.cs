using DPToleranceMonitorService.Enum;
using System;
using System.Text;

namespace DPToleranceMonitorService.Model
{
    public class ToleranceInstance
    {
        public string OPCServer { get; set; }
        public ToleranceVerifType Verification { get; set; }
        public Data[] Data { get; set; }
        public Require Require { get; set; }
        public Result Result { get; set; }
        public Ack Ack { get; set; }
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
        public bool Show(string area = "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (string.IsNullOrWhiteSpace(area))
            {
                Array.ForEach(Data, o =>
                {
                    Console.WriteLine(o.ToString());
                });
                Console.ResetColor();
                return true;
            }
            else
            {
                var data = Array.Find(Data, o => o.Area.Equals(area, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine(data?.ToString() ?? $"{area} doesn't exist.");
                Console.ResetColor();
                return !(data is null);
            }
        }
    }
    public class Data
    {
        public string Area { get; set; }
        public ToleranceEntity[] Tolerances { get; set; }
        public override string ToString()
        {
            var length = Console.WindowWidth;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("".PadRight(length, '='));
            builder.AppendLine(Area);
            builder.AppendLine("".PadRight(length, '-'));

            for (int i = 1; i <= Tolerances.Length; i++)
            {
                builder.Append($"{Tolerances[i - 1].Name} - {Tolerances[i - 1].Value ?? "N/A"}".PadRight(30, ' '));
                // 5个一换行
                if (i % 5 == 0)
                {
                    builder.Append(Environment.NewLine);
                }
            }
            builder.Append(Environment.NewLine);

            builder.AppendLine("".PadRight(length, '='));
            return builder.ToString();
        }
    }
    public class Result
    {
        public string TagAddress { get; set; }
        public int Value { get; set; }
    }
    public class Require
    {
        public string TagAddress { get; set; }
        public int Value { get; set; }
    }
    public class Ack
    {
        public string TagAddress { get; set; }
        public int Value { get; set; }
    }
}
