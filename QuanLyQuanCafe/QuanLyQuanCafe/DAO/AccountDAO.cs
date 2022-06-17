using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
     public class AccountDAO
    {
        private static AccountDAO instance;

        internal static AccountDAO Instance { get { if (instance == null) instance = new AccountDAO(); return instance; } private set => instance = value; }
        private AccountDAO() { }
        //csdl chức năng đăng nhập
        public bool Login(string username, string password)
        {
            //Md5 dùng để thêm thư viện vào, mã hóa mật khẩu
            /*            byte[] teep = ASCIIEncoding.ASCII.GetBytes(password);
                        byte[] hasdata = new MD5CryptoServiceProvider().ComputeHash(teep);
                        string haspass = "";
                        // hàm lặp
                        foreach(byte item in hasdata)
                        {
                            haspass += item;
                        }*/
            /*
           var list = hasdata.ToString();
           list.Reverse;*/

            string query = "usb_login @username , @password";//sử dụng parapasster để cách  ra ko lỗi
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password/*list thay password*/ });
            return result.Rows.Count > 0;
        }
        // truy vấn lấy username
        public Account GetAccountUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where username = '" + username + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        //sửa thông tin cá nhân
        public bool updateAccount(string username, string displayname,  string password, string newpassword)
        {
            int result = DataProvider.Instance.ExecuteNomQuery("exec USD_UpdateAccountIdentityCard @username  , @displayname ,  @password , @newpassword ", new object[] { username, displayname, password, newpassword });

            return result > 0;
        }
        //truy vấn lấy thông tin bảng account
        public DataTable Getlistaccount()
        {
            return DataProvider.Instance.ExecuteQuery("select UserName,DisplayName,type from Account");
        }
        //truy vấn thêm tài khoản
        public bool insertaccount(string username, string displayname, int type)
        {
            string query = string.Format("insert account(username, displayname,type,PassWord) values (N'{0}', N'{1}' , {2} , N'{3}' )", username, displayname, type,"kaka");
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        //truy vấn sửa tài khoản
        public bool updateaccount(string username, string displayname, int type)
        {
            string query = string.Format("update account set   displayname = N'{0}'  , type = {1} where username = N'{2}'", displayname, type, username);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        //truy vấn xóa tài khoản
        public bool deleteaccount(string username)
        {
            string query = string.Format("delete account where username = N'{0}'", username);
            int resurt = DataProvider.Instance.ExecuteNomQuery(query);
            return resurt > 0;
        }
        // truy vấn đặt lại mật khẩu
        public bool Resetpass(string username)
        {
            string query = string.Format("update account set password = 'kaka' where username = N'{0}'", username);
            int passw = DataProvider.Instance.ExecuteNomQuery(query);
            return passw > 0;
        }
    }
}
