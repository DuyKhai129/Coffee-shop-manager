
namespace QuanLyQuanCafe
{
    partial class fLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.power = new System.Windows.Forms.Button();
            this.btLogin = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbPassWord = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txbUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.power);
            this.panelEx1.Controls.Add(this.btLogin);
            this.panelEx1.Controls.Add(this.pictureBox3);
            this.panelEx1.Controls.Add(this.pictureBox1);
            this.panelEx1.Controls.Add(this.txbPassWord);
            this.panelEx1.Controls.Add(this.txbUserName);
            this.panelEx1.Controls.Add(this.pictureBox2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(602, 549);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // power
            // 
            this.power.BackgroundImage = global::QuanLyQuanCafe.Properties.Resources.power;
            this.power.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.power.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.power.FlatAppearance.BorderSize = 0;
            this.power.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(99)))));
            this.power.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.power.Location = new System.Drawing.Point(567, 3);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(32, 32);
            this.power.TabIndex = 4;
            this.power.UseVisualStyleBackColor = true;
            this.power.Click += new System.EventHandler(this.power_Click);
            // 
            // btLogin
            // 
            this.btLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btLogin.Location = new System.Drawing.Point(234, 410);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(128, 39);
            this.btLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btLogin.TabIndex = 3;
            this.btLogin.Text = "Đăng nhập";
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::QuanLyQuanCafe.Properties.Resources.key;
            this.pictureBox3.Location = new System.Drawing.Point(117, 325);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QuanLyQuanCafe.Properties.Resources.custom;
            this.pictureBox1.Location = new System.Drawing.Point(117, 269);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txbPassWord
            // 
            // 
            // 
            // 
            this.txbPassWord.Border.Class = "TextBoxBorder";
            this.txbPassWord.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbPassWord.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPassWord.Location = new System.Drawing.Point(155, 325);
            this.txbPassWord.Name = "txbPassWord";
            this.txbPassWord.PreventEnterBeep = true;
            this.txbPassWord.Size = new System.Drawing.Size(301, 32);
            this.txbPassWord.TabIndex = 2;
            this.txbPassWord.Text = "1234567";
            this.txbPassWord.UseSystemPasswordChar = true;
            // 
            // txbUserName
            // 
            // 
            // 
            // 
            this.txbUserName.Border.Class = "TextBoxBorder";
            this.txbUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbUserName.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbUserName.Location = new System.Drawing.Point(155, 269);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.PreventEnterBeep = true;
            this.txbUserName.Size = new System.Drawing.Size(301, 32);
            this.txbUserName.TabIndex = 1;
            this.txbUserName.Text = "zero";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::QuanLyQuanCafe.Properties.Resources.person_icon;
            this.pictureBox2.Location = new System.Drawing.Point(234, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 128);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // fLogin
            // 
            this.AcceptButton = this.btLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.power;
            this.ClientSize = new System.Drawing.Size(602, 549);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "fLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fLogin_FormClosing);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.Controls.TextBoxX txbPassWord;
        private DevComponents.DotNetBar.Controls.TextBoxX txbUserName;
        private System.Windows.Forms.PictureBox pictureBox3;
        private DevComponents.DotNetBar.ButtonX btLogin;
        private System.Windows.Forms.Button power;
    }
}

