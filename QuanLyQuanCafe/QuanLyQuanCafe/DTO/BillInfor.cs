using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfor
    {
        public BillInfor(int id, int idbill, int idfood, int count)
        {
            this.id = id;
            this.Idbill = idbill;
            this.Idfood = idfood;
            this.count = count;
        }
        public BillInfor(DataRow row)
        {
            this.id = (int)row["id"];//kiểu dữ liệu bđ là ofjec
            this.Idbill = (int)row["idbill"];
            this.Idfood = (int)row["idfood"];
            this.count = (int)row["count"];
        }
        private int count;
        private int idfood;
        private int idbill;
        private int id;

        public int Id { get => id; set => id = value; }
        public int Count { get => count; set => count = value; }
        public int Idfood { get => idfood; set => idfood = value; }
        public int Idbill { get => idbill; set => idbill = value; }
    }
}
