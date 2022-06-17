using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance { get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; } private set => instance = value; }

        private FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> lf = new List<Food>();
            string query = "select* from Food where idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);

                lf.Add(food);
            }
            return lf;
        }
        //lấy ds các món ăn
        public List<Food> GetListFood()
        {
            List<Food> lf = new List<Food>();
            string query = "select *  from Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);

                lf.Add(food);
            }
            return lf;
        }
        //câu truy vẫn thêm món
        public bool insertfood(string name, int idCategory, float price)
        {
            string query = string.Format("insert Food values (N'{0}',{1},{2})", name, idCategory, price);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        //câu truy vẫn sửa món
        public bool updatefood(int idfood, string name, int idcategory, float price)
        {
            string query = string.Format("update food set name = N'{0}',idcategory = {1} , price = {2} where id = {3}", name, idcategory, price, idfood);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        //câu truy vẫn xóa món
        public bool deletefood(int idfood)
        {
            BillInforDAO.Instance.deletebillinforidfood(idfood);

            string query = string.Format("delete Food where id = {0}", idfood);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        // câu truy vấn chức năng tìm kiếm
        public List<Food> searchnamefood(string name)
        {
            List<Food> lf = new List<Food>();
            string query = string.Format("select * from Food where [dbo].[GetUnsignString](name) like N'%' +[dbo].[GetUnsignString](N'{0}') + N'%'", name); //%trc tìm tất cả chuỗi cuối cùng kết thúc bàng kí tự đó và ngc lại % ở cuối
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);

                lf.Add(food);
            }
            return lf;
        }
        // truy vẫn xóa idfood khi idcategory đã xóa
        public void deletefooddid(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete Food where idCategory = " + id);
        }
    }
}
