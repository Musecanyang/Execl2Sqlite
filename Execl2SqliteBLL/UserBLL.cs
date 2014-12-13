using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Execl2Sqlite.Model;
using Execl2Sqlite.DAL;
namespace Execl2Sqlite.BLL
{
    public class UserBLL
    {
        UserDAL dal = new UserDAL();


        //往数据库中添加数据
        public bool InserUser(User user,string path)
        {
            int r = -1;
            r = dal.InserUser(user,path);
            return r > 0 ? true : false;
        }
    }
}
