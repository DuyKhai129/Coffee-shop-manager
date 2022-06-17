using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        //tạo singerthon
        private static TableDAO instance;//ctrl+r+e

        public static TableDAO Instance { get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; } private set => instance = value; }
        private TableDAO() { }
        //Singleton tồn tại một thể hiện trong một chương trình nếu ta có nhu cầu xài đi xài lại
        public static int TableWidth = 95;
        public static int TableHeight = 95;
        public List<Table> loadtablelist()
        {
            List<Table> tablelist = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("usb_gettablelist");

            foreach (DataRow item in data.Rows)//duyệt tuần tự các phần tử trong mảng hoặc tập hợp
            {
                Table table = new Table(item);

                tablelist.Add(table);
            }
            return tablelist;
        }
        public void switchtable(int id1, int id2)//chuyển bàn
        {
            DataProvider.Instance.ExecuteQuery("UAD_switchtable @idtb1 , @idtb2", new object[] { id1, id2 });

        }
        public bool inserttable(string name)
        {
            string query = string.Format("insert tablefood(name,status) values (N'{0}',N'Trống')", name);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        public bool updatetable(int idtable, string name, string status)
        {
            string query = string.Format("update tablefood set name = N'{0}',status = N'{1}' where id = {2}", name, status, idtable);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        public bool deletetablefood(int idtable)
        {
            BillDAO.Instance.deleteBillTableId(idtable);
            string query = string.Format("delete tablefood where id = {0}", idtable);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
    }
}
