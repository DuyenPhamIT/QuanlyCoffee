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
    public partial class addProduct : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public addProduct()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Không được để trống tên sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Không được để trống giá sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text.Length > 0 && txtName.Text.Length > 0)
            {
                Product pro = new Product();
                pro.ProductName = txtName.Text;
                pro.Price = Convert.ToDouble(txtPrice.Text);
                pro.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                data.Products.InsertOnSubmit(pro);
                data.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtName.Text = null;
            txtPrice.Text = null;
            cboCategory.Text = null; 
            LoadProduct();
        }

        private void addProduct_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadProduct();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Không được để trống tên sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Không được để trống giá sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text.Length > 0 && txtName.Text.Length > 0)
            {
                int id = Convert.ToInt32(this.dgvProduct.CurrentRow.Cells[0].Value.ToString());
                Product pro = data.Products.Single(p => p.ProductId.Equals(id));
                pro.ProductName = txtName.Text;
                pro.Price = Convert.ToDouble(txtPrice.Text);
                pro.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                data.SubmitChanges();
            }
            txtName.Text = null;
            txtPrice.Text = null;
            cboCategory.Text = null;
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProduct();



        }

        private void LoadProduct()
        {
            var dataPro = from Pro in data.Products
                          join c in data.Categories on Pro.CategoryId equals c.CategoryId
                          select new
                          {
                              ProductId = Pro.ProductId,
                              CategoryName = c.CategoryName,
                              ProductName = Pro.ProductName,
                              Price = Pro.Price,
                          };
            dgvProduct.DataSource = dataPro.ToList();
            LoadCategory();
        }

        private void LoadCategory()
        {
            var dataCat = from Cat in data.Categories
                          select new
                          {
                              CategoryId = Cat.CategoryId,
                              CategoryName = Cat.CategoryName

                          };
            cboCategory.DataSource = dataCat;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Bạn có muốn xóa không", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(this.dgvProduct.CurrentRow.Cells[0].Value.ToString());
                    Product pro = data.Products.Single(p => p.ProductId.Equals(id));
                    data.Products.DeleteOnSubmit(pro);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProduct();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bạn không thể xóa được bản ghi này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtName.Text = null;
                txtPrice.Text = null;
                cboCategory.Text = null;
            }
        }

        private void dgvProduct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvProduct.CurrentRow.Cells[0].Value.ToString());
            Product pro = data.Products.Single(p => p.ProductId.Equals(id));
            txtName.Text = pro.ProductName.ToString();
            txtPrice.Text = pro.Price.ToString();
            cboCategory.SelectedValue = pro.CategoryId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = from Pro in data.Products.Where(p => p.ProductName.Contains(txtSearch.Text))
                         join c in data.Categories on Pro.CategoryId equals c.CategoryId
                         select new
                         {
                             ProductId = Pro.ProductId,
                             CategoryName = c.CategoryName,
                             ProductName = Pro.ProductName,
                             Price = Pro.Price,
                         };
            dgvProduct.DataSource = result;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportProduct report = new ReportProduct();
            report.Show();
        }
    }
}
