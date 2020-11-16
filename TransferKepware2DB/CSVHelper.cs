using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TransferKepware2DB
{
    public class CSVHelper
    {
        private string _csvPath;
        private List<string[]> _context;

        public CSVHelper(string csvPath)
        {
            this._csvPath = csvPath;

            this.Initialize();
        }
        private void Initialize(bool firstIsHead = true)
        {
            this._context = new List<string[]>();
            if (!File.Exists(_csvPath) || !new FileInfo(_csvPath).Extension.Equals(".csv"))
            {
                return;
            }
            try
            {
                string[] csvLineSet = File.ReadAllLines(_csvPath, Encoding.UTF8);
                int i = 0;
                if (firstIsHead)
                {
                    i = 1;
                }
                for (; i < csvLineSet.Length; i++)
                {
                    if (string.IsNullOrEmpty(csvLineSet[i]))
                    {
                        break;
                    }
                    var csv = SplitStringWithComma(csvLineSet[i]);
                    this._context.Add(csv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Count
        {
            get
            {
                return this._context.Count;
            }
        }

        public IList<string[]> Context
        {
            get
            {
                return this._context;
            }
        }

        /// <summary>
        /// 以逗号拆分字符串
        /// 若字段中包含逗号(备注：包含逗号的字段必须有双引号引用)则对其进行拼接处理
        /// 最后在去除其字段的双引号
        /// </summary>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        private static string[] SplitStringWithComma(string splitStr)
        {
            var newstr = string.Empty;
            List<string> sList = new List<string>();

            bool isSplice = false;
            string[] array = splitStr.Split(new char[] { ',' });
            foreach (var str in array)
            {
                if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
                {
                    var firstchar = str.Substring(0, 1);
                    var lastchar = string.Empty;
                    if (str.Length > 0)
                    {
                        lastchar = str.Substring(str.Length - 1, 1);
                    }
                    if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                    {
                        isSplice = true;
                    }
                    if (lastchar.Equals("\""))
                    {
                        if (!isSplice)
                            newstr += str;
                        else
                            newstr = newstr + "," + str;

                        isSplice = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                }

                if (isSplice)
                {
                    //添加因拆分时丢失的逗号
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                    else
                        newstr = newstr + "," + str;
                }
                else
                {
                    sList.Add(newstr.Replace("\"", "").Trim());//去除字符中的双引号和首尾空格
                    newstr = string.Empty;
                }
            }
            return sList.ToArray();
        }
        /// <summary>
        /// 获取保存CSV字符串，并判断是否需要加入双引号“”
        /// </summary>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        public static string SaveStringWithComma(string[] splitStr)
        {
            foreach (var i in Enumerable.Range(0, splitStr.Length))
            {
                if (string.IsNullOrEmpty(splitStr[i]))
                {
                    splitStr[i] = string.Empty;
                }
                if (splitStr[i].Contains(","))
                {
                    splitStr[i] = $"\"{splitStr[i]}\"";
                }
            }
            var result = string.Join(",", splitStr);
            return result;
        }
    }
}
