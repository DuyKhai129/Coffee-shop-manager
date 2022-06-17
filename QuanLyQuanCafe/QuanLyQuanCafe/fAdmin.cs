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
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        //sử dụng binging để lạo bỏ lỗi mất kết nỗi binding
        BindingSource Foodlist = new BindingSource();
        BindingSource Categorylist = new BindingSource();
        BindingSource Tablelist = new BindingSource();
        BindingSource Accountlist = new BindingSource();
        //lấy thông tin tk đăng đăng nhập
        public Account LoginAccount;
        public fAdmin()
        {
            InitializeComponent();
            // QL doanh thu
            loadDateTime();
            //QL thức ăn
            LoadListFood();
            AddFoodBuding();
            LoadCategoryCombobox(cbCategoryName);
            //QL danh mục
            LoadListCategory();
            AddCategoryBuding();
            // QL bàn ăn
            LoadListTable();
            AddTableBuding();
            // QL tài khoản
            LoadListAccount();
            AddAccountBuding();
        }
        #region Method
        #region thống kê
        void loadDateTime()
        {
            //công thức lấy ngày
            DateTime today = DateTime.Now;
            dtFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtToDate.Value = dtFromDate.Value.AddMonths(1).AddDays(-1);
        }
        //tải danh sách hóa đơn theo ngày đẵ chọn
        void loadListBillByDate(DateTime checkin, DateTime checkout)
        {
            dgvBill.DataSource = BillDAO.Instance.date(checkin, checkout);
        }
        #endregion

        #region thức ăn
        void LoadListFood()
        {
            dgvFood.DataSource = Foodlist;
            Foodlist.DataSource = FoodDAO.Instance.GetListFood();
            
        }
        void AddFoodBuding()
        {
            // kĩ thuật binding
            txbFoodName.DataBindings.Add(new Binding("text", dgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));//thay đổi never là có thể OnPropertyChanged or OnPropertyChanged để có thể nhập dữ liệu vào đó
            nmFoodPrice.DataBindings.Add(new Binding("value", dgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
            txbFoodId.DataBindings.Add(new Binding("text", dgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
        }
        //load danh mục món
        void LoadCategoryCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "name";
        }
        //search món ăn
        List<Food> searchfoodname(string name)
        {
            LoadListFood();
            List<Food> searchfood = FoodDAO.Instance.searchnamefood(name);
            return searchfood;
        }
        #endregion

        #region danh mục
        void LoadListCategory()
        {
            dgvCategory.DataSource = Categorylist;
            Categorylist.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void AddCategoryBuding()
        {
            // kĩ thuật binding
            txbCategoryName.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));//thay đổi never là có thể OnPropertyChanged or OnPropertyChanged để có thể nhập dữ liệu vào đó
            txbCategoryId.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region bàn ăn
        void LoadListTable()
        {
            dgvTable.DataSource = Tablelist;
            Tablelist.DataSource = TableDAO.Instance.loadtablelist();
        }
        void AddTableBuding()
        {
            // kĩ thuật binding
            txbTableName.DataBindings.Add(new Binding("text", dgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));//thay đổi never là có thể OnPropertyChanged or OnPropertyChanged để có thể nhập dữ liệu vào đó
            txbTableId.DataBindings.Add(new Binding("text", dgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            cbStatus.DataBindings.Add(new Binding("text", dgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region tài khoản
        void LoadListAccount()
        {
            dgvAccount.DataSource = Accountlist;

            Accountlist.DataSource = AccountDAO.Instance.Getlistaccount();
        }
        void AddAccountBuding()
        {
            // kĩ thuật binding
            txbUserName.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "username", true, DataSourceUpdateMode.Never));//thay đổi never là có thể OnPropertyChanged or OnPropertyChanged để có thể nhập dữ liệu vào đó
            txbDisplayName.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "displayname", true, DataSourceUpdateMode.Never));
            cbTypeAccount.DataBindings.Add(new Binding("text", dgvAccount.DataSource, "type", true, DataSourceUpdateMode.Never));

        }
        #endregion

        #endregion

        #region event
        #region thống kê
        //chức năng thống kê
        private void btStatistical_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtFromDate.Value, dtToDate.Value);
 		double totalprice = 0;
            
            for (int i = 0; i < dgvBill.Rows.Count-1; i++)
            {
                totalprice += double.Parse(dgvBill.Rows[i].Cells[3].Value.ToString());
            }
            CultureInfo culture = new CultureInfo("vi-VN");//bỏ đi máy sẽ dung theo c của máy tính
            Thread.CurrentThread.CurrentCulture = culture;
            txbTPrice.Text = totalprice.ToString("c", culture);
        }
        // chức năng xuất ra file excel
        private void exportExcel(string path)
        {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgvBill.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dgvBill.Columns[i].HeaderText;
            }
            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                for (int j = 0; j < dgvBill.Columns.Count; j++)
                {
                    application.Cells[i + 2, j + 1] = dgvBill.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved = true;
        }
        private void btExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Excel";
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2013 (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    exportExcel(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file excel thành công!");
                }
                catch { }
            }
        }
        #endregion

        #region event thức ăn
        // chức năng xem 
        private void btShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        //thay đổi theo tên món
        private void txbFoodId_TextChanged(object sender, EventArgs e)
        {

            //lấy cells bất kì từ dữ liệu
            try
            {
                if (dgvFood.SelectedCells.Count > 0 && dgvFood.SelectedCells[0].OwningRow.Cells["idCategory"].Value != null)
                {
                    int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["idCategory"].Value;

                    Category cateogory = CategoryDAO.Instance.getcategory(id);

                    cbCategoryName.SelectedItem = cateogory;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCategoryName.Items)
                    {
                        if (item.Id == cateogory.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbCategoryName.SelectedIndex = index;

                }
            }
            catch { }
        }
        // chức năng thêm món
        private void btAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idcategory = (cbCategoryName.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.insertfood(name, idcategory, price))
            {
                MessageBox.Show("Bạn đã thêm món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (insertfood != null)
                    insertfood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn thêm món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chức năng sửa món
        private void btEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idcategory = (cbCategoryName.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodId.Text);

            if (FoodDAO.Instance.updatefood(id, name, idcategory, price))
            {
                MessageBox.Show("Bạn đã sửa món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (updatefood != null)
                    updatefood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn sửa món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chức năng xóa món
        private void btDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodId.Text);
            if (FoodDAO.Instance.deletefood(id))
            {
                MessageBox.Show("Bạn đã xóa món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (deletefood != null)
                    deletefood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn xóa món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //event
        private event EventHandler insertfood;
        public event EventHandler Insertfood
        {
            add { insertfood += value; }
            remove { insertfood -= value; }
        }

        private event EventHandler updatefood;
        public event EventHandler Updatefood
        {
            add { updatefood += value; }
            remove { updatefood -= value; }
        }
        private event EventHandler deletefood;
        public event EventHandler Deletefood
        {
            add { deletefood += value; }
            remove { deletefood -= value; }
        }
        // chức năng tìm kiếm
        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            dgvFood.DataSource = Foodlist;
            Foodlist.DataSource = searchfoodname(txbSearch.Text);
        }
        #endregion

        #region danh mục
        //chức năng thêm danh mục
        private void btAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.insertcategory(name))
            {
                MessageBox.Show("Bạn đã thêm danh mục món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListCategory();
                LoadCategoryCombobox(cbCategoryName);
                if (insertcategory != null)
                    insertcategory(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn thêm danh mục món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chức năng sửa danh mục
        private void btEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryId.Text);

            if (CategoryDAO.Instance.updatecategory(name, id))
            {
                MessageBox.Show("Bạn đã sửa danh mục món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListCategory();
                LoadCategoryCombobox(cbCategoryName);
                if (updatecategory != null)
                    updatecategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn sửa danh mục món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chức năng xóa danh mục
        private void btDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryId.Text);

            if (CategoryDAO.Instance.deletecategory(id))
            {
                MessageBox.Show("Bạn đã xóa danh mục món ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListCategory();
                LoadListFood();
                LoadCategoryCombobox(cbCategoryName);
                if (deletecategory != null)
                    deletecategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn xóa danh mục món ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chức năng xem danh mục
        private void btShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        #region event
        private event EventHandler insertcategory;
        public event EventHandler Insertcategory
        {
            add { insertcategory += value; }
            remove { insertcategory -= value; }
        }
        private event EventHandler deletecategory;
        public event EventHandler Deletecategory
        {
            add { deletecategory += value; }
            remove { deletecategory -= value; }
        }
        private event EventHandler updatecategory;
        public event EventHandler Updatecategory
        {
            add { updatecategory += value; }
            remove { updatecategory -= value; }
        }
        #endregion

        #endregion

        #region bàn ăn
        // chức năng thêm bàn
        private void btAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            if (TableDAO.Instance.inserttable(name))
            {
                MessageBox.Show("Bạn đã thêm bàn ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListTable();
                if (inserttable != null)
                    inserttable(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn thêm bàn ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng sửa bàn
        private void btEditTable_Click(object sender, EventArgs e)
        {
            String status = cbStatus.Text;
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbTableId.Text);
            if (TableDAO.Instance.updatetable(id, name, status))
            {
                MessageBox.Show("Bạn đã cập nhập bàn ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListTable();
                if (updatetable != null)
                    updatetable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn cập nhập bàn ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng xóa bàn
        private void btDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);
            if (TableDAO.Instance.deletetablefood(id))
            {
                MessageBox.Show("Bạn đã xóa bàn ăn thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListTable();
                if (deletetable != null)
                    deletetable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn xóa bàn ăn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng xem bàn
        private void btShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }
        #region event
        private event EventHandler inserttable;
        public event EventHandler Inserttable
        {
            add { inserttable += value; }
            remove { inserttable -= value; }
        }
        private event EventHandler deletetable;
        public event EventHandler Deletetable
        {
            add { deletetable += value; }
            remove { deletetable -= value; }
        }
        private event EventHandler updatetable;
        public event EventHandler Updatetable
        {
            add { updatetable += value; }
            remove { updatetable -= value; }
        }
        #endregion

        #endregion

        #region tài khoản
        // chức năng thêm tài khoản
        private void btAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type =Convert.ToInt32(cbTypeAccount.Text);
            if (AccountDAO.Instance.insertaccount(username, displayname, type))
            {
                MessageBox.Show("Bạn đã thêm tài khoản thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListAccount();

            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn thêm tài khoản!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng thêm sửa khoản
        private void btEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = Convert.ToInt32(cbTypeAccount.Text);
            if (AccountDAO.Instance.updateaccount(username, displayname, type))
            {
                MessageBox.Show("Bạn đã cập nhập tài khoản thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListAccount();

            }
            else
            {
                MessageBox.Show("Có lỗi khi bạn cập nhập tài khoản!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng thêm xóa khoản
        private void btDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            if (LoginAccount.Username.Equals(username))
            {
                MessageBox.Show("Bạn Ko thể xóa khi tài khoản này đăng login", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (AccountDAO.Instance.deleteaccount(username))
            {
                MessageBox.Show("Bạn đã xóa tài khoản  thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListAccount();

            }
            else
            {
                MessageBox.Show("có lỗi khi bạn xóa tài khoản!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // chức năng thêm xem khoản
        private void btShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }
        // chức năng đặt lại mk
        private void btReset_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            if (AccountDAO.Instance.Resetpass(username))
            {
                MessageBox.Show(" Khôi phục mật khẩu thành công!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(" Khôi phục mật khẩu thất bại thất bại!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

    }
}
