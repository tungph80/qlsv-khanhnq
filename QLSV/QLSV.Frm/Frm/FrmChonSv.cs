﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonSv : Form
    {
        private IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private readonly int _idkythi;
        private FrmLoadding _loading = new FrmLoadding();
        private readonly BackgroundWorker _bgwInsert;

        public FrmChonSv(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
            _bgwInsert = new BackgroundWorker();
            _bgwInsert.DoWork += bgwInsert_DoWork;
            _bgwInsert.RunWorkerCompleted += bgwInsert_RunWorkerCompleted;
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("Chon", typeof (bool));
            table.Columns.Add("MaSV", typeof (string));
            table.Columns.Add("HoSV", typeof (string));
            table.Columns.Add("TenSV", typeof (string));
            table.Columns.Add("NgaySinh", typeof (string));
            table.Columns.Add("MaLop", typeof (string));
            return table;
        }

        /// <summary>
        /// Tìm theo khoa , lớp
        /// </summary>
        private void Timkiemtheolop()
        {
            try
            {
                var indexkhoa = cbokhoa.SelectedValue;
                var indexlop = cbolop.SelectedValue;
                if (indexlop == null)
                {
                    if (indexkhoa == null) return;
                    if (IsNumber(indexkhoa.ToString()))
                    {
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa((int) indexkhoa, _idkythi);
                    }
                }
                else
                {
                    if (IsNumber(indexlop.ToString()))
                    {
                        dgv_DanhSach.DataSource = SearchData.Timkiemtheolop((int) indexlop, _idkythi);
                    }
                }


            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Tìm theo niên khóa
        /// </summary>
        private void Timkiemtheokhoa()
        {
            try
            {
                if (string.IsNullOrEmpty(txtkhoa.Text)) return;
                dgv_DanhSach.DataSource = SearchData.Timkiemtheokhoa(txtkhoa.Text, _idkythi);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void ChonSinhVien()
        {
            try
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    if (!bool.Parse(row.Cells["Chon"].Text)) continue;
                    var masv = int.Parse(row.Cells["MaSV"].Text);
                    var hspp = new XepPhong
                    {
                        IdKyThi = _idkythi,
                        IdSV = masv
                    };
                    _listXepPhong.Add(hspp);
                }
                InsertData.Chonsinhvien(_listXepPhong);
                Invoke((Action)(()=>MessageBox.Show(@"Lưu lại thành công", @"Thông báo")));
                Invoke((Action)(Close));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Luu()
        {
            _bgwInsert.RunWorkerAsync();
            OnShowDialog("Đang lưu dữ liệu");
        }

        private void FrmChonSv_Load(object sender, EventArgs e)
        {
            try
            {
                dgv_DanhSach.DataSource = GetTable();
                cbokhoa.DataSource = LoadData.Load(3);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbokhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = cbokhoa.SelectedValue;
                if (obj == null) return;
                cbolop.DataSource = SearchData.Timkiem(int.Parse(obj.ToString()));
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnTimtheokhoa_Click(object sender, EventArgs e)
        {
            Timkiemtheokhoa();
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;

                #region Caption

                band.Columns["Chon"].Header.Caption = @"Chọn";
                band.Columns["MaSV"].Header.Caption = @"Mã SV";
                band.Columns["HoSV"].Header.Caption = FormResource.txtHosinhvien;
                band.Columns["TenSV"].Header.Caption = FormResource.txtTensinhvien;
                band.Columns["NgaySinh"].Header.Caption = @"Ngày Sinh";
                band.Columns["MaLop"].Header.Caption = @"Lớp";

                #endregion

                band.Columns["Chon"].Style = ColumnStyle.CheckBox;
                band.Columns["MaSV"].CellActivation = Activation.NoEdit;
                band.Columns["HoSV"].CellActivation = Activation.NoEdit;
                band.Columns["TenSV"].CellActivation = Activation.NoEdit;
                band.Columns["NgaySinh"].CellActivation = Activation.NoEdit;
                band.Columns["MaLop"].CellActivation = Activation.NoEdit;

                band.Columns["TenSV"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaLop"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["MaSV"].CellAppearance.TextHAlign = HAlign.Center;

                band.Columns["HoSV"].Width = 170;
                band.Columns["MaSV"].Width = 150;
                band.Columns["TenSV"].Width = 150;
                band.Columns["NgaySinh"].Width = 150;
                band.Columns["MaLop"].Width = 150;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void ckbChon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbChon.Checked)
                {
                    foreach (var row in dgv_DanhSach.Rows)
                    {
                        row.Cells["Chon"].Value = "true";

                    }
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows)
                    {
                        row.Cells["Chon"].Value = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            Luu();
        }

        private void txtkhoa_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Timkiemtheokhoa();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cbolop_SelectedValueChanged(object sender, EventArgs e)
        {
            Timkiemtheolop();
        }

        private void txtkhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F5):
                    Luu();
                    break;
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region BackgroundWorker

        private void bgwInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            ChonSinhVien();
        }

        private void bgwInsert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnCloseDialog();
        }

        private void OnShowDialog(string msg)
        {
            try
            {
                _loading = new FrmLoadding();
                _loading.Update(msg);
                _loading.ShowDialog();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void OnCloseDialog()
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action) (() =>
                    {
                        _loading.Close();
                        _loading = null;
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion
    }
}