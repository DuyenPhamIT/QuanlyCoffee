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
    public partial class addDepartment : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public addDepartment()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = from s in data.Departments.Where(dep => dep.DepName.Contains(txtSearch.Text)) select s;
            dgvDep.DataSource = result;
        }

        private void dgvDep_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dgvDep.CurrentRow.Cells[0].Value.ToString());
            Department dep = data.Departments.Single(d => d.DepId.Equals(id));
            txtDepName.Text = dep.DepName;
            txtTotal.Text = dep.TotalEmployee.ToString();
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
        private void btnAddNew_Click(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDepName.Text = null;
            txtTotal.Text = null;
        }

        private void addDepartment_Load(object sender, EventArgs e)
        {
            LoadDep();
        }
    }
}
