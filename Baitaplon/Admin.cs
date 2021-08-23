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
    public partial class Admin : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public Admin()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (txtUse.Text == "")
            {
                MessageBox.Show("UserName Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPass.Text == "")
            {
                MessageBox.Show("Password Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn size!!!");
                return;
            }
            if (txtPass.Text.Length > 0 && txtUse.Text.Length > 0)
            {
                Acount acou = new Acount();
                acou.username = txtUse.Text;
                acou.password = txtPass.Text;
                acou.role = cboType.SelectedItem.ToString();
                data.Acounts.InsertOnSubmit(acou);
                data.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAcount();
            }

            txtPass.Text = null;
            txtUse.Text = null;
            txtUse.Focus();
        }

        private void LoadAcount()
        {
            var dataAcou = from acou in data.Acounts
                           select new
                           {
                               Id = acou.id,
                               UserName = acou.username,
                               Password = acou.password,
                               Type = acou.role
                           };
            dgvAcount.DataSource = dataAcou.ToList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtUse.Text == "")
            {
                MessageBox.Show("UserName Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPass.Text == "")
            {
                MessageBox.Show("Password Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn size!!!");
                return;
            }
            if (txtPass.Text.Length > 0 && txtUse.Text.Length > 0)
            {
                int id = Convert.ToInt32(this.dgvAcount.CurrentRow.Cells[0].Value.ToString());
                Acount acou = data.Acounts.Single(a=>a.id.Equals(id));
                acou.username = txtUse.Text;
                acou.password = txtPass.Text;
                acou.role = cboType.SelectedValue.ToString();
                data.SubmitChanges();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAcount();
            }

            txtPass.Text = null;
            txtUse.Text = null;
            txtUse.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Bạn có muốn xóa không", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(this.dgvAcount.CurrentRow.Cells[0].Value.ToString());
                    Acount acou = data.Acounts.Single(a => a.id.Equals(id));
                    data.Acounts.DeleteOnSubmit(acou);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAcount();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bạn không thể xóa được bản ghi này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = from s in data.Acounts.Where(c => c.username.Contains(txtName.Text)) select s;
            dgvAcount.DataSource = result;
        }

        private void dgvAcount_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvAcount.CurrentRow.Cells[0].Value.ToString());
            Acount acou = data.Acounts.Single(a => a.id.Equals(id));
            txtPass.Text = acou.password;
            txtUse.Text = acou.username;
            cboType.SelectedItem = acou.role;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            LoadAcount();
        }
    }
}
