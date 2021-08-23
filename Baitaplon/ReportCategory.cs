using Baitaplon.Reports;
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
    public partial class ReportCategory : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public ReportCategory()
        {
            InitializeComponent();
        }

        private void report_Category_Load(object sender, EventArgs e)
        {
            var dataCat = from cat in data.Categories
                          select new
                          {
                              CategoryId = cat.CategoryId,
                              CategoryName = cat.CategoryName,
                              Discription = cat.Discription,
                          };
            CategoryReport c = new CategoryReport();
            c.SetDataSource(dataCat.ToList());
            report_Category.ReportSource = c;
            report_Category.Show();
        }
    }
}
