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
    public partial class fPrint : Form
    {
        public fPrint()
        {
            InitializeComponent();
        }
        //chức năng hiện hóa đơn
        private void buttonX1_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyCafeDataSet.getBillinfor' table. You can move, or remove it, as needed.
            this.getBillinforTableAdapter.Fill(this.QuanLyCafeDataSet.getBillinfor);

            this.reportViewer1.RefreshReport();
        }
    }
}
