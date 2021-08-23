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
    public partial class addCategory : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public addCategory()
        {
            InitializeComponent();
        }
        private void LoadCategory()
        {
            var dataCat = from cat in data.Categories
                          select new
                          {
                              CategoryId = cat.CategoryId,
                              CategoryName = cat.CategoryName,
                              Description = cat.Discription,
                          };
            dgvCategory.DataSource = dataCat.ToList();
        }
        private void addCategory_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = from s in data.Categories.Where(c => c.CategoryName.Contains(txtSearch.Text)) select s;
            dgvCategory.DataSource = result;
        }

        private void dgvCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvCategory.CurrentRow.Cells[0].Value.ToString());
            Category cat = data.Categories.Single(c => c.CategoryId.Equals(id));

            txtCode.Text = cat.CategoryId.ToString();
            txtName.Text = cat.CategoryName;
            rtDescription.Text = cat.Discription;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtName.Text.Length > 0 && txtCode.Text.Length > 0)
            {
                Category cat = new Category();
                cat.CategoryId = Convert.ToInt32(txtCode.Text);
                cat.CategoryName = txtName.Text;
                cat.Discription = rtDescription.Text;
                data.Categories.InsertOnSubmit(cat);
                data.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategory();
            }

            txtCode.Text = null;
            txtName.Text = null;
            rtDescription.Text = null;
            txtName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvCategory.CurrentRow.Cells[0].Value.ToString());
            Category cat = data.Categories.Single(c => c.CategoryId.Equals(id));
            if (txtName.Text == null)
            {
                MessageBox.Show("Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtName.Text.Length > 0)
            {
                cat.CategoryId = Convert.ToInt32(txtCode.Text);
                cat.CategoryName = txtName.Text;
                cat.Discription = rtDescription.Text;
                data.SubmitChanges();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategory();
            }



            txtCode.Text = null;
            txtName.Text = null;
            rtDescription.Text = null;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = null;
            txtCode.Text = null;
            rtDescription.Text = null;
            txtCode.Focus();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Bạn có muốn xóa không", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(this.dgvCategory.CurrentRow.Cells[0].Value.ToString());
                    Category cat = data.Categories.Single(c => c.CategoryId.Equals(id));
                    data.Categories.DeleteOnSubmit(cat);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bạn không thể xóa được bản ghi này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtName.Text = null;
                txtCode.Text = null;
                rtDescription.Text = null;
                txtCode.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportCategory report = new ReportCategory();
            report.Show();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvCategory.CurrentRow.Cells[0].Value.ToString());
            Category cat = data.Categories.Single(c => c.CategoryId.Equals(id));
            if (txtName.Text == null)
            {
                MessageBox.Show("Không được để tên trống", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtName.Text.Length > 0)
            {
                cat.CategoryId = Convert.ToInt32(txtCode.Text);
                cat.CategoryName = txtName.Text;
                cat.Discription = rtDescription.Text;
                data.SubmitChanges();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategory();
            }



            txtCode.Text = null;
            txtName.Text = null;
            rtDescription.Text = null;
            txtCode.Focus();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCode.Text = null;
            txtName.Text = null;
            rtDescription.Text = null;
            txtCode.Focus();
        }
    }
}
