using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Execl2Sqlite.Model;
using Execl2Sqlite.BLL;
using System.IO;
using NPOI.HSSF.UserModel;

namespace Execl2Sqlite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            // 创建一个打开文件对话框
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 将路径存到TestBox中
                txtFileRead.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //获取数据库存放的路径
            string path = txtFileWrite.Text.TrimEnd('\\') + "\\" + txtFileWriteName.Text;

            //创建数据库并添加表
            CreatDB(path);

            //获取数据
            User user = new User();

            using (Stream s = File.OpenRead(txtFileRead.Text.ToString()))
            {
                HSSFWorkbook work = new HSSFWorkbook(s);//获取表
                HSSFSheet sheet = work.GetSheetAt(0);//获取页
                int rowNum = sheet.LastRowNum;//获取有多少行

                for (int i = 1; i <= rowNum; i++)//因为第一行是标题，所以跳过，从索引1开始循环
                {
                    HSSFRow row = sheet.GetRow(i);

                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        HSSFCell cell = row.GetCell(j);

                        //判断这个列是不是数字类型
                        if (cell.CellType == HSSFCell.CELL_TYPE_NUMERIC)
                        {
                            user.UserId = Convert.ToInt32(cell.NumericCellValue);
                        }
                        else if (cell.CellType == HSSFCell.CELL_TYPE_STRING)
                        {
                            user.UserName = cell.StringCellValue;
                        }
                    }// end for

                    //将获取的数据添加到数据库中
                    UserBLL bll = new UserBLL();
                    if (bll.InserUser(user, path))
                    {
                    }
                    else
                    {
                        MessageBox.Show("导入失败");
                    }
                }// end for

            }// end stream
            MessageBox.Show("导入成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileWrite.Text = fbd.SelectedPath;
            }
        }


        private void CreatDB(string path)
        {
            //创建一个数据库文件  
            System.Data.SQLite.SQLiteConnection.CreateFile(path);
            //连接数据库  
            System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection();
            System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            connstr.DataSource = path;
            conn.ConnectionString = connstr.ToString();
            conn.Open();
            //创建表  
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            string sql = "CREATE TABLE User(UserId int,UserName varchar(20))";
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }
    }
}
