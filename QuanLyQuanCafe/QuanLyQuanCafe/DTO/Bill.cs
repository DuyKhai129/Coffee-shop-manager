using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? datecheckin, DateTime? datecheckout, int status)
        {
            this.id = id;
            this.Datecheckin = datecheckin;
            this.datecheckout = datecheckout;
            this.status = status;
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.datecheckin = (DateTime?)row["datecheckin"];

            var datecheckoutteep = row["datecheckout"];// kiểm tra
            if (datecheckoutteep.ToString() != "")
                this.Datecheckout = (DateTime?)datecheckoutteep;

            this.status = (int)row["status"];
            
        }
        
        private int status;
        private DateTime? datecheckout;
        private DateTime? datecheckin;// thêm ? vì kiểu dl này ko cho phép lưu
        private int id;

        public int Id { get => id; set => id = value; }
        public int Status { get => status; set => status = value; }
        public DateTime? Datecheckin { get => datecheckin; set => datecheckin = value; }
        public DateTime? Datecheckout { get => datecheckout; set => datecheckout = value; }
       
    }
}
