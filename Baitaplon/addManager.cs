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
    public partial class addManager : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public addManager()
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


        private void tabPage1_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
            LoadProduct();
        }

        private void LoadCategory1()
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
            LoadCategory1();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (txtNamePro.Text == "")
            {
                MessageBox.Show("Không được để trống tên sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Không được để trống giá sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text.Length > 0 && txtNamePro.Text.Length > 0)
            {
                Product pro = new Product();
                pro.ProductName = txtNamePro.Text;
                pro.Price = Convert.ToDouble(txtPrice.Text);
                pro.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                data.Products.InsertOnSubmit(pro);
                data.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtNamePro.Text = null;
            txtPrice.Text = null;
            LoadProduct();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNamePro.Text == "")
            {
                MessageBox.Show("Không được để trống tên sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Không được để trống giá sản phẩm ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPrice.Text.Length > 0 && txtNamePro.Text.Length > 0)
            {
                int id = Convert.ToInt32(this.dgvProduct.CurrentRow.Cells[0].Value.ToString());
                Product pro = data.Products.Single(p => p.ProductId.Equals(id));
                pro.ProductName = txtNamePro.Text;
                pro.Price = Convert.ToDouble(txtPrice.Text);
                pro.CategoryId = int.Parse(cboCategory.SelectedValue.ToString());
                data.SubmitChanges();
            }
            txtNamePro.Text = null;
            txtPrice.Text = null;
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProduct();
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
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProduct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvProduct.CurrentRow.Cells[0].Value.ToString());
            Product pro = data.Products.Single(p => p.ProductId.Equals(id));
            txtNamePro.Text = pro.ProductName.ToString();
            txtPrice.Text = pro.Price.ToString();
            cboCategory.SelectedValue = pro.CategoryId;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            LoadDep1();
        }

        private void LoadDep()
        {
            var dataDep = from dep in data.Departments
                            select new
                            {
                                DepartmentId = dep.DepId,
                                DepartmentName = dep.DepName,
                                TotalEmployee = dep.TotalEmployee
                            };
            dgvDep.DataSource = dataDep.ToList();
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result = from s in data.Departments.Where(dep => dep.DepName.Contains(txtSearchDe.Text)) select s;
            dgvDep.DataSource = result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Department dep = new Department();
            dep.DepName = txtDepName.Text;
            dep.TotalEmployee = Convert.ToInt32(txtTotal.Text);

            data.Departments.InsertOnSubmit(dep);
            data.SubmitChanges();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtDepName.Text = null;
            txtTotal.Text = null;
            txtDepName.Focus();
            LoadDep();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvDep.CurrentRow.Cells[0].Value.ToString());
            Department dep = data.Departments.Single(d => d.DepId.Equals(id));
            dep.DepName = txtDepName.Text;
            dep.TotalEmployee = Convert.ToInt32(txtTotal.Text);
            data.SubmitChanges();
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtDepName.Text = null;
            txtTotal.Text = null;
            txtDepName.Focus();
            LoadDep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtDepName.Text = null;
            txtTotal.Text = null;
        }

        private void dgvDep_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvDep.CurrentRow.Cells[0].Value.ToString());
            Department dep = data.Departments.Single(d => d.DepId.Equals(id));
            txtDepName.Text = dep.DepName;
            txtTotal.Text = dep.TotalEmployee.ToString();
        }

        private void dgvEmployee_Click(object sender, EventArgs e)
        {
            string ma = this.dgvEmployee.CurrentRow.Cells[0].Value.ToString();
            Employee emp = data.Employees.Single(em => em.EmployeeId.Equals(ma));
            txtAddress.Text = emp.Address.ToString();
            txtEmail.Text = emp.Email.ToString();
            txtEnd.Value = (DateTime)emp.EndStart;
            txtStart.Value = (DateTime)emp.DateStart;
            txtFirstName.Text = emp.Name.ToString();
            txtPhone.Text = emp.Phone.ToString();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            LoadDep1();
            LoadEmployee();
        }

        private void LoadDep1()
        {
            var dataDep = from dep in data.Departments
                          select new
                          {
                              DepId = dep.DepId,
                              DepName = dep.DepName
                          };
            cboDepartment.DataSource = dataDep;
            cboDepartment.DisplayMember = "DepName";
            cboDepartment.ValueMember = "DepId";
        }

        private void LoadEmployee()
        {
            var dataEmp = from emp in data.Employees
                          join dep in data.Departments on emp.DepId equals dep.DepId
                          select new
                          {
                              EmployeeId = emp.EmployeeId,
                              Name = emp.Name,
                              Email = emp.Email,
                              Phone = emp.Phone,
                              Address = emp.Address,
                              DateStart = emp.DateStart,
                              EndDate = emp.EndStart,
                              Department = dep.DepName,
                          };
            dgvEmployee.DataSource = dataEmp.ToList();
            LoadDep();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Không được để trống mã ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Không được để trống tên ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Không được để trống email ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Không được để trống số điện thoại", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Không được để trống địa chỉ ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtId.Text != null)
            {
                MessageBox.Show("Đã tồn tại nhân viên này", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtId.Text.Length > 0 && txtEmail.Text.Length > 0 && txtAddress.Text.Length > 0 && txtFirstName.Text.Length > 0 && txtPhone.Text.StartsWith("09") || txtPhone.Text.StartsWith("03") && txtPhone.Text.Length == 10)
            {
                Employee emp = new Employee();
                emp.EmployeeId = txtId.Text;
                emp.Name = txtFirstName.Text;
                emp.Email = txtEmail.Text;
                emp.Phone = txtPhone.Text;
                emp.Address = txtAddress.Text;
                emp.DateStart = txtStart.Value;
                emp.EndStart = txtEnd.Value;
                emp.DepId = int.Parse(cboDepartment.SelectedValue.ToString());
                data.Employees.InsertOnSubmit(emp);
                data.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtAddress.Text = null;
            txtEmail.Text = null;
            txtFirstName.Text = null;
            txtPhone.Text = null;
            txtId.Text = null;
            LoadEmployee();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Không được để trống tên ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Không được để trống email ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Không được để trống số điện thoại", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Không được để trống địa chỉ ", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtEmail.Text.Length > 0 && txtAddress.Text.Length > 0 && txtFirstName.Text.Length > 0 && txtPhone.Text.StartsWith("09") || txtPhone.Text.StartsWith("03") && txtPhone.Text.Length == 10)
            {
                string ma = this.dgvEmployee.CurrentRow.Cells[0].Value.ToString();
                Employee emp = data.Employees.Single(em => em.EmployeeId.Contains(ma));
                txtId.ReadOnly = true;
                emp.Name = txtFirstName.Text;
                emp.Email = txtEmail.Text;
                emp.Phone = txtPhone.Text;
                emp.Address = txtAddress.Text;
                emp.DateStart = txtStart.Value;
                emp.EndStart = txtEnd.Value;
                emp.DepId = int.Parse(cboDepartment.SelectedValue.ToString());
                data.SubmitChanges();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtAddress.Text = null;
            txtEmail.Text = null;
            txtFirstName.Text = null;
            txtPhone.Text = null;
            txtId.Text = null;

            LoadEmployee();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Bạn có muốn xóa không", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    string ma = this.dgvEmployee.CurrentRow.Cells[0].Value.ToString();
                    Employee emp = data.Employees.Single(em => em.EmployeeId.Equals(ma));
                    data.Employees.DeleteOnSubmit(emp);
                    data.SubmitChanges();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployee();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bạn không thể xóa được bản ghi này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var result = from emp in data.Employees.Where(em => em.Name.Contains(txtSearchE.Text))
                         join dep in data.Departments on emp.DepId equals dep.DepId
                         select new
                         {
                             EmployeeId = emp.EmployeeId,
                             Name = emp.Name,
                             Email = emp.Email,
                             Phone = emp.Phone,
                             Address = emp.Address,
                             DateStart = emp.DateStart,
                             EndDate = emp.EndStart,
                             Department = dep.DepName,

                         };
            dgvEmployee.DataSource = result;
        }

        private void addManager_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadProduct();
            LoadEmployee();
            LoadDep();
        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ReportCategory report = new ReportCategory();
            report.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ReportProduct report = new ReportProduct();
            report.Show();
        }
    }
}
