using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitaplon
{
    public partial class frmLogin : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {

                string user = txtUsername.Text;
                string pass = txtPassword.Text;
                string type = "CASHIER";
                if (rdbAdmin.Checked == true)
                {
                    type = "ADMIN";
                }
                var q1 = from log in data.Acounts where log.username == user && log.password == pass select log;
                if (q1.SingleOrDefault() != null)
                {
                    this.Hide();
                    Main main = new Main();
                    main.Show();
                    
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Cơ sở dữ liệu không tồn tại. Vui lòng tạo mới theo file hướng dẫn", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ms = MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ms == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
