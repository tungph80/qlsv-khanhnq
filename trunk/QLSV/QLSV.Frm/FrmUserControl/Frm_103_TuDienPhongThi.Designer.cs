﻿namespace QLSV.Frm.FrmUserControl
{
    partial class Frm_103_TuDienPhongThi
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
            this.components = new System.ComponentModel.Container();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters1 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            PerpetuumSoft.Reporting.Export.ExtraParameters extraParameters2 = new PerpetuumSoft.Reporting.Export.ExtraParameters();
            this.menu_ug = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip_themdong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_xoadong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_luulai = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Huy = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_DanhSach = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.reportManager1 = new PerpetuumSoft.Reporting.Components.ReportManager(this.components);
            this.rptdanhsachphongthi = new PerpetuumSoft.Reporting.Components.FileReportSlot(this.components);
            this.excelExportFilter1 = new PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter(this.components);
            this.pdfExportFilter1 = new PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter(this.components);
            this.menu_ug.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // menu_ug
            // 
            this.menu_ug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_themdong,
            this.menuStrip_xoadong,
            this.menuStrip_luulai,
            this.menuStrip_Huy});
            this.menu_ug.Name = "contextMenuStrip1";
            this.menu_ug.Size = new System.Drawing.Size(137, 92);
            // 
            // menuStrip_themdong
            // 
            this.menuStrip_themdong.Name = "menuStrip_themdong";
            this.menuStrip_themdong.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_themdong.Text = "Thêm dòng";
            this.menuStrip_themdong.Click += new System.EventHandler(this.menuStrip_themdong_Click);
            // 
            // menuStrip_xoadong
            // 
            this.menuStrip_xoadong.Name = "menuStrip_xoadong";
            this.menuStrip_xoadong.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_xoadong.Text = "Xóa dòng";
            this.menuStrip_xoadong.Click += new System.EventHandler(this.menuStrip_xoadong_Click);
            // 
            // menuStrip_luulai
            // 
            this.menuStrip_luulai.Name = "menuStrip_luulai";
            this.menuStrip_luulai.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_luulai.Text = "Lưu lại";
            this.menuStrip_luulai.Click += new System.EventHandler(this.menuStrip_luulai_Click);
            // 
            // menuStrip_Huy
            // 
            this.menuStrip_Huy.Name = "menuStrip_Huy";
            this.menuStrip_Huy.Size = new System.Drawing.Size(136, 22);
            this.menuStrip_Huy.Text = "Hủy";
            this.menuStrip_Huy.Click += new System.EventHandler(this.menuStripHuy_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_DanhSach);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 209);
            this.panel2.TabIndex = 12;
            // 
            // dgv_DanhSach
            // 
            this.dgv_DanhSach.ContextMenuStrip = this.menu_ug;
            this.dgv_DanhSach.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.dgv_DanhSach.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.dgv_DanhSach.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.dgv_DanhSach.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dgv_DanhSach.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dgv_DanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DanhSach.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_DanhSach.Location = new System.Drawing.Point(0, 0);
            this.dgv_DanhSach.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dgv_DanhSach.Name = "dgv_DanhSach";
            this.dgv_DanhSach.Size = new System.Drawing.Size(274, 209);
            this.dgv_DanhSach.TabIndex = 25;
            this.dgv_DanhSach.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uG_DanhSach_InitializeLayout);
            this.dgv_DanhSach.AfterExitEditMode += new System.EventHandler(this.uG_DanhSach_AfterExitEditMode);
            this.dgv_DanhSach.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.dgv_DanhSach_BeforeRowsDeleted);
            this.dgv_DanhSach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uG_DanhSach_KeyDown);
            // 
            // reportManager1
            // 
            this.reportManager1.DataSources = new PerpetuumSoft.Reporting.Components.ObjectPointerCollection(new string[0], new object[0]);
            this.reportManager1.Reports.AddRange(new PerpetuumSoft.Reporting.Components.ReportSlot[] {
            this.rptdanhsachphongthi});
            // 
            // rptdanhsachphongthi
            // 
            this.rptdanhsachphongthi.FilePath = "D:\\HocTap\\DoAnTN\\QLSV\\QLSV.Frm\\Reports\\danhsachphongthi.rst";
            this.rptdanhsachphongthi.ReportName = "";
            this.rptdanhsachphongthi.ReportScriptType = typeof(PerpetuumSoft.Reporting.Rendering.ReportScriptBase);
            // 
            // excelExportFilter1
            // 
            this.excelExportFilter1.ExportInLargePage = true;
            this.excelExportFilter1.ExportInOnePage = true;
            this.excelExportFilter1.ExportWithoutPageDelimeters = true;
            this.excelExportFilter1.ExtraParameters = extraParameters1;
            // 
            // pdfExportFilter1
            // 
            this.pdfExportFilter1.ChangePermissionsPassword = null;
            this.pdfExportFilter1.Compress = true;
            this.pdfExportFilter1.ExtraParameters = extraParameters2;
            this.pdfExportFilter1.UserPassword = null;
            // 
            // Frm_103_TuDienPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "Frm_103_TuDienPhongThi";
            this.Size = new System.Drawing.Size(274, 209);
            this.Load += new System.EventHandler(this.FrmDanhsachphongthi_Load);
            this.menu_ug.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menu_ug;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_themdong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_xoadong;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_luulai;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Huy;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinGrid.UltraGrid dgv_DanhSach;
        private PerpetuumSoft.Reporting.Components.ReportManager reportManager1;
        private PerpetuumSoft.Reporting.Export.OpenXML.ExcelExportFilter excelExportFilter1;
        private PerpetuumSoft.Reporting.Export.Pdf.PdfExportFilter pdfExportFilter1;
        private PerpetuumSoft.Reporting.Components.FileReportSlot rptdanhsachphongthi;
    }
}