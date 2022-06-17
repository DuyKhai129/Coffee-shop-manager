using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyQuanCafe.DTO.Menu;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        // hàm dựng đang nhập
        private Account loginAccount;

        public Account LoginAccount { get => loginAccount; set => loginAccount = value; }
        public fTableManager(Account Login)
        {
            InitializeComponent();
            Loadtable();
            Loadcategory();
            Loadcbtable(cbMoveTable);
            this.LoginAccount = Login;
            ChangeAccount(LoginAccount.Type);

        }
        #region Method
        //chuyền thông tin tài khoản  
        void ChangeAccount(int type)
        {
            // phân quyền tài khoản
            adminToolStripMenuItem.Enabled = type == 1;
            //ghi tài khoản đăng nhập
            thôngTinToolStripMenuItem.Text += " ⊹⊱-(¯`" + LoginAccount.Displayname + "´¯)-⊰⊹ ";
        }
        void Loadcbtable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.loadtablelist();
            cb.DisplayMember = "name";
        }
        void Loadcategory()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";//chỉ ra trường cần hiển thị
        }
        void loadfood(int id)
        {
            List<Food> foods = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = foods;
            cbFood.DisplayMember = "name";//chỉ ra trường cần hiển thị
        }
        
        void Loadtable()
        {
            flpTable.Controls.Clear();//xóa bỏ bảng
            List<Table> tableList = TableDAO.Instance.loadtablelist();
            foreach (Table item in tableList)
            {
                Button bt = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                bt.Text = item.Name;// trạng thái bàn
                bt.Click += Bt_Click;
                bt.Tag = item;//lưu
                //bt.Image = Image.FromFile(@"C:\Users\duykhai\Downloads\UML\empty.png");// thêm ảnh vào button
                if (item.Status == "Trống")
                {
                    bt.BackColor = Color.Chartreuse;
                }
                else
                {
                    bt.BackColor = Color.Red;
                }    
                    
                flpTable.Controls.Add(bt);
               
            }
        }

        void showbill(int id)
        {
            lvBill.Items.Clear();
            List<Menu> b = MenuDAO.Instance.GetListMenuTable(id);
            float totalprice = 0;
            foreach (Menu item in b)
            {
                ListViewItem isvitem = new ListViewItem(item.Foodname.ToString());
                isvitem.SubItems.Add(item.Count.ToString());
                isvitem.SubItems.Add(item.Price.ToString());

                isvitem.SubItems.Add(item.Totalprice.ToString());
                totalprice += item.Totalprice;
                lvBill.Items.Add(isvitem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");//bỏ đi máy sẽ dung theo c của máy tính
            Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalprice.ToString("c", culture);
        }
        #endregion

        #region event
        private void Bt_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).Id;
            lvBill.Tag = (sender as Button).Tag;//tag vào thàng nào thì listview co tag thàng đó -- lưu bàn lại
            showbill(tableId);
        }
        //chức năng đăng xuất
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //chức năng thông tin cá nhân
        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile profile = new fAccountProfile(LoginAccount);
            profile.UdAccount += Profile_UdAccount;
            profile.ShowDialog();
        }
        //event thay đổi tên đăng nhập khi ta cập nhập lại thông tin
        private void Profile_UdAccount(object sender, fAccountProfile.AccountEvent e)
        {
            thôngTinToolStripMenuItem.Text = "Thông Tin cá nhân ⊹⊱-(¯`" + e.Acc.Displayname + "´¯)-⊰⊹ ";
        }
        // chức năng admin
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin admin = new fAdmin();
            //event cập nhập lại thông tin món
            admin.Insertfood += Admin_Insertfood;
            admin.Updatefood += Admin_Updatefood;
            admin.Deletefood += Admin_Deletefood;
            //event cập nhập lại thông tin category
            admin.Insertcategory += Admin_Insertcategory;
            admin.Updatecategory += Admin_Updatecategory;
            admin.Deletecategory += Admin_Deletecategory;
            //event cập nhập lại thông tin bàn ăn
            admin.Inserttable += Admin_Inserttable;
            admin.Updatetable += Admin_Updatetable;
            admin.Deletetable += Admin_Deletetable;
            // lấy thông tin tài khoản đang nhập
            admin.LoginAccount = loginAccount;
            admin.ShowDialog();
        }

        #region event cập nhập thông tin khi admin cập nhập lại
        private void Admin_Deletetable(object sender, EventArgs e)
        {
            Loadtable();
        }

        private void Admin_Updatetable(object sender, EventArgs e)
        {
            Loadtable();
        }

        private void Admin_Inserttable(object sender, EventArgs e)
        {
            Loadtable();
        }

        private void Admin_Deletecategory(object sender, EventArgs e)
        {
            Loadcategory();
        }

        private void Admin_Updatecategory(object sender, EventArgs e)
        {
            Loadcategory();
        }

        private void Admin_Insertcategory(object sender, EventArgs e)
        {
            Loadcategory();
        }
        private void Admin_Deletefood(object sender, EventArgs e)
        {
            loadfood((cbCategory.SelectedItem as Category).Id);
            Table table = lvBill.Tag as Table;
            if (lvBill.Tag != null)
            {
                showbill((lvBill.Tag as Table).Id);
                Loadtable();
            }
        }

        private void Admin_Updatefood(object sender, EventArgs e)
        {
            loadfood((cbCategory.SelectedItem as Category).Id);
            Table table = lvBill.Tag as Table;
            if (lvBill.Tag != null)
            {
                showbill((lvBill.Tag as Table).Id);
            }
        }

        private void Admin_Insertfood(object sender, EventArgs e)
        {
            loadfood((cbCategory.SelectedItem as Category).Id);
            Table table = lvBill.Tag as Table;
            if (lvBill.Tag != null)
            {
                showbill((lvBill.Tag as Table).Id);
            }
        }
        #endregion
        //load lại món ăn theo category
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;// tv đã chọn
            id = selected.Id;
            loadfood(id);
        }
        //chức năng thêm món vào bill
        private void btAddFood_Click(object sender, EventArgs e)
        {
            //th1 sử dụng table đã lưu 
            Table table = lvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Bạn hãy chọn bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idbill = BillDAO.Instance.GetCheckOutTableID(table.Id);
            int idfood = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;

            if (idbill == -1)
            {
                BillDAO.Instance.InsearchBill(table.Id);
                BillInforDAO.Instance.InsertBillInfo(BillDAO.Instance.BillMax(), idfood, count);
            }
            else//th2 bill đã tồn tại 
            {
                BillInforDAO.Instance.InsertBillInfo(idbill, idfood, count);
            }
            showbill(table.Id);
            Loadtable();
        }
        //chức năng thanh toán
        private void btPay_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
            int idbill = BillDAO.Instance.GetCheckOutTableID(table.Id);
           
            if (idbill != -1)
            {
                DialogResult k;//sd yes&no
                k = MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho {0} hay ko...?", table.Name), "\tThông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (k == DialogResult.Yes)
                {
                    BillDAO.Instance.CheckOut(idbill);
                    showbill(table.Id);

                    Loadtable();
                }
            }
        }
        //chức năng chuyển bàn
        private void btMoveTable_Click(object sender, EventArgs e)
        {
            int id1 = (lvBill.Tag as Table).Id;
            int id2 = (cbMoveTable.SelectedItem as Table).Id;
            DialogResult x;//sd yes&no
            x = MessageBox.Show(string.Format("Bạn có chắc muốn chuyển từ {0} sang {1} hay ko...?", (lvBill.Tag as Table).Name, (cbMoveTable.SelectedItem as Table).Name), "\tThông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (x == DialogResult.Yes)
            {
                TableDAO.Instance.switchtable(id1, id2);
                Loadtable();
            }
        }
        #endregion
        //chức năng in hóa đơn
        private void btPrint_Click(object sender, EventArgs e)
        {
            fPrint print = new fPrint();
            print.ShowDialog();
        }
    }
}
