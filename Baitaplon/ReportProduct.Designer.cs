
namespace Baitaplon
{
    partial class ReportProduct
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
            this.report_product = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // report_product
            // 
            this.report_product.ActiveViewIndex = -1;
            this.report_product.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.report_product.Cursor = System.Windows.Forms.Cursors.Default;
            this.report_product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report_product.Location = new System.Drawing.Point(0, 0);
            this.report_product.Name = "report_product";
            this.report_product.Size = new System.Drawing.Size(800, 450);
            this.report_product.TabIndex = 0;
            this.report_product.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.report_product.Load += new System.EventHandler(this.report_product_Load);
            // 
            // ReportProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.report_product);
            this.Name = "ReportProduct";
            this.Text = "ReportProduct";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer report_product;
    }
}