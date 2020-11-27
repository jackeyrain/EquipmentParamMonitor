using ProjectArrow.Entity;

namespace ProjectArrow.Models
{
    public class LoginHelper
    {
        public bool Login(string account, string pwd)
        {
            var userInfo = DBHelper.Db.Select<MES_TS_SYS_USER>().Where(o => o.USER_LOGIN_NAME.Equals(account) && o.USER_STATUS != 0).First();
            return userInfo != null;
        }
    }
}