
namespace Baitaplon
{
    partial class ReportCategory
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
            this.report_Category = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // report_Category
            // 
            this.report_Category.ActiveViewIndex = -1;
            this.report_Category.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.report_Category.Cursor = System.Windows.Forms.Cursors.Default;
            this.report_Category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report_Category.Location = new System.Drawing.Point(0, 0);
            this.report_Category.Name = "report_Category";
            this.report_Category.Size = new System.Drawing.Size(1016, 523);
            this.report_Category.TabIndex = 0;
            this.report_Category.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.report_Category.Load += new System.EventHandler(this.report_Category_Load);
            // 
            // ReportCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 523);
            this.Controls.Add(this.report_Category);
            this.Name = "ReportCategory";
            this.Text = "ReportCategory";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer report_Category;
    }
}