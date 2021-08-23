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
    public partial class ReportProduct : Form
    {
        BaitapDataContext data = new BaitapDataContext();
        public ReportProduct()
        {
            InitializeComponent();
        }
        
        private void report_product_Load(object sender, EventArgs e)
        {

           
            Report_Product rp = new Report_Product();
            rp.Load("PUT CRYSTAL REPOST PATH HERE\\Report_Product.rpt");
            report_product.ReportSource = rp;
            report_product.Refresh();
        }
    }
}
