
namespace Baitaplon
{
    partial class PayReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pay_report = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // pay_report
            // 
            this.pay_report.ActiveViewIndex = -1;
            this.pay_report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pay_report.Cursor = System.Windows.Forms.Cursors.Default;
            this.pay_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pay_report.Location = new System.Drawing.Point(0, 0);
            this.pay_report.Name = "pay_report";
            this.pay_report.Size = new System.Drawing.Size(800, 450);
            this.pay_report.TabIndex = 0;
            this.pay_report.Load += new System.EventHandler(this.pay_report_Load);
            // 
            // PayReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pay_report);
            this.Name = "PayReport";
            this.Text = "PayReport";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer pay_report;
    }
}