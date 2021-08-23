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
    public partial class PayReport : Form
    {
        public PayReport()
        {
            InitializeComponent();
        }

        private void pay_report_Load(object sender, EventArgs e)
        {
            ReportPay rp = new ReportPay();
            rp.Load("PUT CRYSTAL REPOST PATH HERE\\ReportPay.rpt");
            pay_report.ReportSource = rp;
            pay_report.Refresh();
        }
    }
}
