using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Configuration;

namespace Execl2Sqlite.DAL
{
    public class SqliteHelper
    {
        public static int ExecuteNonQuery(string sql, string path, params SQLiteParameter[] param)
        {
            string str = "Data Source=" + path;  //连接字符串
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
