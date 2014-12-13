using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Execl2Sqlite.Model;
using System.Data.SqlClient;

namespace Execl2Sqlite.DAL
{
    public class UserDAL
    {
        //往数据库中添加数据
        public int InserUser(User user, string path)
        {
            string sql = "insert into User(UserID,UserName) values(@UserID,@UserName)";
            SQLiteParameter[] pms ={
                              new SQLiteParameter("UserID",user.UserId),
                              new SQLiteParameter("UserName",user.UserName)
                              };
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            list.AddRange(pms);

            return SqliteHelper.ExecuteNonQuery(sql, path, list.ToArray());

        }


    }
}
