using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance { get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; } private set => instance = value; }
        private BillDAO() { }
        //lấy thông tin món theo bàn 
        public int GetCheckOutTableID(int id)
        {

            DataTable data = DataProvider.Instance.ExecuteQuery("select *from Bill where idTable = " + id + " and status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        //thêm món ăn vào hóa đơn
        public void InsearchBill(int id)
        {
            DataProvider.Instance.ExecuteNomQuery("exec USB_insearchBill @idTable", new object[] { id });
        }

        //bill đc  thêm là bill mới nên cho nó là lớn nhất
        public int BillMax()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(id) from Bill");
            }
            catch
            {
                return 1;
            }
        }
        //thanh toán 
        public void CheckOut(int id)
        {
            string query = "update Bill set DateCheckOut = getdate(), status = 1 where id = " + id;
            DataProvider.Instance.ExecuteNomQuery(query);
        }
        // doanh thu
        public DataTable date(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExecuteQuery("exec USA_revenue @checkin , @checkout", new object[] { checkin, checkout });
        }
        // truy vẫn xóa idtable trong bill khi bàn đã xóa
        public void deleteBillTableId(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete bill where idtable = " + id);
        }
    }
}
