using EquipmentParamMonitor.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.ACCESS
{
    public class DbContext<T>
        where T : class, new()
    {
        public SqlSugarClient Db;
        public SimpleClient<T> dbTable
        {
            get
            {
                return new SimpleClient<T>(Db);
            }
        }
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConfigurationManager.AppSettings["PISCES"],
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });

            //Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //};
        }

        public virtual List<T> GetList()
        {
            return dbTable.GetList();
        }
        public virtual List<T> GetList(Expression<Func<T, bool>> condition)
        {
            return dbTable.GetList(condition);
        }
        public virtual bool Delete(dynamic id)
        {
            return dbTable.Delete(id);
        }
        public virtual bool Update(T obj)
        {
            return dbTable.Update(obj);
        }
        public virtual bool Add(T obj)
        {
            return dbTable.Insert(obj);
        }
    }
}
