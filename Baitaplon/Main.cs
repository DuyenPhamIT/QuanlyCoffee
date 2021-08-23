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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        
        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager addMan = new Manager();
            addMan.MdiParent = this;
            addMan.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin addAcou = new Admin();
            addAcou.MdiParent = this;
            addAcou.Show();
        }

        private void quanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addManager addMa = new addManager();
            addMa.MdiParent = this;
            addMa.Show();
        }

        private void categoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addCategory addCat = new addCategory();
            addCat.MdiParent = this;
            addCat.Show();
        }

        private void productToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            addProduct addpro = new addProduct();
            addpro.MdiParent = this;
            addpro.Show();
        }

        private void departmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addDepartment addDep = new addDepartment();
            addDep.MdiParent = this;
            addDep.Show();
        }

        private void employeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addEmployee addEmp = new addEmployee();
            addEmp.MdiParent = this;
            addEmp.Show();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PayReport report = new PayReport();
            report.Show();
        }
    }
}
