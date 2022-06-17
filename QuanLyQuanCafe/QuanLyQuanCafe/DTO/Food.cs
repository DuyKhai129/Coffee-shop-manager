using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        public Food(int id, string name, int idcategory, float price)
        {
            this.id = id;
            this.name = name;
            this.idcategory = idcategory;
            this.price = price;
        }
        public Food(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            this.idcategory = (int)row["idcategory"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
        }
        private float price;
        private int idcategory;
        private string name;
        private int id;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Idcategory { get => idcategory; set => idcategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
