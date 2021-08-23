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
    public partial class addEmployee : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public addEmployee()
        {
            InitializeComponent();
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

        private void LoadDep()
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

        private void btnNew_Click(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
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

        private void btnDelete_Click(object sender, EventArgs e)
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
                txtAddress.Text = null;
                txtEmail.Text = null;
                txtFirstName.Text = null;
                txtPhone.Text = null;
                txtId.Text = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadEmployee();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = from emp in data.Employees.Where(em => em.Name.Contains(txtSearch.Text))
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

        private void addEmployee_Load_1(object sender, EventArgs e)
        {
                  
            LoadDep();
            LoadEmployee();
        }
    }
}
