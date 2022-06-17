using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        #region// hàm dựng đang nhập
        private Account loginAccount;

        public Account LoginAccount
        { get => loginAccount; set => loginAccount = value;}
    #endregion
    public fAccountProfile(Account login)
        {
            InitializeComponent();
            loginAccount = login;
            changeaccount(loginAccount);
        }
        void changeaccount(Account login)
        {
            txbUserName.Text = loginAccount.Username;
            txbDisplayName.Text = loginAccount.Displayname;
        }
        //chức năng sửa thông tin tài khoản
        void UpdateAccount()
        {
            string displayname = txbDisplayName.Text;
            string password = txbOldPassword.Text;
            string newpassword = txbNewPassword.Text;
            string reenterpassword = txbReenterPassword.Text;
            string username = txbUserName.Text;
            if(displayname == "")
            {
                MessageBox.Show(" Không được để trống!", "Thông báo", MessageBoxButtons.OK);
            }
            if (!newpassword.Equals(reenterpassword))
            {
                MessageBox.Show("Cập nhật thất bại!", "\tThông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txbNewPassword.Text = "";
                txbReenterPassword.Text = "";
                txbNewPassword.Focus();
            }
            else
            {
                if (AccountDAO.Instance.updateAccount(username, displayname, password, newpassword))
                {
                    MessageBox.Show("Bạn đã cập nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (udAccount != null)
                        udAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountUserName(username)));//event
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbOldPassword.Text = "";
                    txbOldPassword.Focus();
                }


            }
        }
        #region làm event chuyển dữ liệu từ con đến cha
        private event EventHandler<AccountEvent> udAccount;
        public event EventHandler<AccountEvent> UdAccount
        {
            add { udAccount += value; }
            remove { udAccount += value; }
        }
        public class AccountEvent : EventArgs
        {
            private Account acc;

            public Account Acc { get => acc; set => acc = value; }
            public AccountEvent(Account acc)
            {
                this.Acc = acc;
            }
        }
        #endregion
        //button thoát
        private void btEscape_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
}
