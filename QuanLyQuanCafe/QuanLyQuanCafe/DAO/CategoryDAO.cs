using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance { get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; } private set => instance = value; }

        private CategoryDAO() { }
        public List<Category> GetListCategory()
        {
            List<Category> l = new List<Category>();

            String query = "select * from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);

                l.Add(category);
            }

            return l;
        }
        //lấy category theo id
        public Category getcategory(int id)
        {
            Category category = null;

            String query = "select * from FoodCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);

                return category;
            }

            return category;
        }
        // truy vấn thêm category
        public bool insertcategory(string name)
        {
            string query = string.Format("insert foodcategory values (N'{0}')", name);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        // truy vấn sửa category
        public bool updatecategory(string name, int id)
        {
            string query = string.Format("update foodcategory set name = N'{0}' where id = {1}", name, id);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        // truy vấn xóa category
        public bool deletecategory(int idcategory)
        {
            FoodDAO.Instance.deletefooddid(idcategory);

            string query = string.Format("delete foodcategory where id = {0}", idcategory);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
    }
}
