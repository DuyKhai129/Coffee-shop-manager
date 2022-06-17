using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        public Account(string username, string displayname, int type, string password = null)
        {
            this.username = username;
            this.displayname = displayname;
            this.Type = type;
            this.password = password;
        }
        public Account(DataRow row)
        {
            this.username = row["username"].ToString();
            this.displayname = row["displayname"].ToString();
            this.Type = (int)row["type"];
            this.password = row["password"].ToString();
        }


        private int type;
        private string password;
        private string displayname;
        private string username;

        public string Username { get => username; set => username = value; }
        public string Displayname { get => displayname; set => displayname = value; }
        public int Type { get => type; set => type = value; }
        public string Password { get => password; set => password = value; }
    }
}
