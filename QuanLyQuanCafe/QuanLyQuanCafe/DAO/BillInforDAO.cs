using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInforDAO
    {
        private static BillInforDAO instance;

        public static BillInforDAO Instance { get { if (instance == null) instance = new BillInforDAO(); return BillInforDAO.instance; } private set => instance = value; }
        private BillInforDAO() { }
        public List<BillInfor> GetListBillInfor(int id)
        {
            List<BillInfor> b = new List<BillInfor>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select *from billInfo where idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfor info = new BillInfor(item);
                b.Add(info);
            }
            return b;
        }
        //thêm vào chi tiết hóa đơn
        public void InsertBillInfo(int idbill, int idfood, int count)
        {
            DataProvider.Instance.ExecuteNomQuery("exec USB_BillInfor @idbill , @idfood , @count", new object[] { idbill, idfood, count });

        }
        //xóa thông tin của idfood trong billinfor
        public void deletebillinforidfood(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete billInfo where idFood = " + id);
        }
    }
}
