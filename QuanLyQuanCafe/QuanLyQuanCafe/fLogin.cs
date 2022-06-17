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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void power_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string UserName = txbUserName.Text;
            string PassWord = txbPassWord.Text;
            if(txbUserName.Text == "" && txbPassWord.Text == "")
            {
                MessageBox.Show(" Không được phép để trống!", "Thông báo", MessageBoxButtons.OK);
            }
            if (Login(UserName, PassWord))
            {
                Account loginaccount = AccountDAO.Instance.GetAccountUserName(UserName);
                fTableManager ql = new fTableManager(loginaccount);
                MessageBox.Show("Bạn đã đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK);

               this.Hide();
                ql.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Bạn đăng nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);//đang nhập thất bại        
               /* txbUserName.Text = "";*/
                txbPassWord.Text = "";
                txbUserName.Focus();
            }


        }
        bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }


        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát chương chình hay ko..?","Thông báo",MessageBoxButtons.OKCancel ) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }
    }
}
