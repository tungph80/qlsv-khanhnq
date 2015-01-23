﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;
using QLSV.Frm.Base;
using QLSV.Frm.Ultis.Frm;

namespace QLSV.Frm.FrmUserControl
{
    public partial class Frm_101_Danhmuckhoa : FunctionControlHasGrid
    {
        private readonly List<Khoa> _listAdd = new List<Khoa>();
        private readonly List<Khoa> _listUpdate = new List<Khoa>();

        public Frm_101_Danhmuckhoa()
        {
            InitializeComponent();
        }

        #region Exit

        protected virtual DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        protected virtual void LoadGrid()
        {
            try
            {
                _listAdd.Clear();
                _listUpdate.Clear();
                dgv_DanhSach.DataSource = LoadData.Load(15);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);

            }
        }

        protected override void LoadFormDetail()
        {
            LoadGrid();
            if (dgv_DanhSach.Rows.Count == 0)
            {
                InsertRow();
            }
            _listUpdate.Clear();
            _listAdd.Clear();
            IdDelete.Clear();
        }

        protected override void InsertRow()
        {
            InsertRow(dgv_DanhSach, "STT", "TenKhoa");
        }

        protected override void DeleteRow()
        {
            try
            {
                bool check;
                if (dgv_DanhSach.Selected.Rows.Count > 0)
                {
                    foreach (var row in dgv_DanhSach.Selected.Rows)
                    {
                        var id = row.Cells["ID"].Text;
                        if (!string.IsNullOrEmpty(id))
                        {
                            IdDelete.Add(int.Parse(id));
                        }
                    }
                    check = true;
                }
                else if (dgv_DanhSach.ActiveRow != null)
                {
                    var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                    if (string.IsNullOrEmpty(id)) return;
                    var index = dgv_DanhSach.ActiveRow.Index;
                    check = false;
                    var idStr = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                    if (!string.IsNullOrEmpty(idStr))
                        IdDelete.Add(int.Parse(idStr));
                }
                else
                {
                    MessageBox.Show(@"Chọn Khoa để xóa");
                    return;
                }
                var b = DeleteData.KtraXoaThongTin(2, IdDelete);
                if (!b)
                {
                    MessageBox.Show(@"không thể xóa Khoa. Cần xóa lớp trước khi xóa Khoa",
                        FormResource.MsgCaption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    IdDelete.Clear();
                    return;
                }
                if (IdDelete.Count > 0 && DialogResult.Yes ==
                        MessageBox.Show(@"Bạn có chắc chắn muốn xóa không ?",
                            FormResource.MsgCaption,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                {
                    DeleteData.XoaKhoa(IdDelete);
                    Stt();
                    if (check)
                        dgv_DanhSach.DeleteSelectedRows(false);
                    else
                        dgv_DanhSach.ActiveRow.Delete(false);

                    MessageBox.Show(@"Xóa dữ liệu thành công", FormResource.MsgCaption);
                }
                IdDelete.Clear();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected override void SaveDetail()
        {
            try
            {
                if (ValidateData())
                {
                    MessageBox.Show(@"Vui lòng nhập đầy đủ thông tin", @"Lỗi");
                }
                else
                {
                    foreach (var row in dgv_DanhSach.Rows.Where(row => string.IsNullOrEmpty(row.Cells["ID"].Text)))
                    {
                        var hs = new Khoa
                        {
                            TenKhoa = row.Cells["TenKhoa"].Text
                        };
                        _listAdd.Add(hs);
                    }
                    if (_listUpdate.Count <= 0 && _listAdd.Count <= 0) return;
                    if (_listUpdate.Count > 0) UpdateData.UpdateKhoa(_listUpdate);
                    if (_listAdd.Count > 0) InsertData.ThemKhoa(_listAdd);
                    MessageBox.Show(FormResource.MsgThongbaothanhcong, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadFormDetail();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected virtual bool ValidateData()
        {
            var inputTypes = new List<InputType>
            {
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.KhongKiemTra,
                InputType.ChuoiRong
                
            };
            return ValidateHighlight.UltraGrid(dgv_DanhSach, inputTypes);
        }

        protected override void XoaDetail()
        {
            try
            {
                DeleteData.Xoa("KHOA");
                LoadFormDetail();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Stt()
        {
            for (var i = 0; i < dgv_DanhSach.Rows.Count; i++)
            {
                dgv_DanhSach.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        #endregion

        #region Event_uG

        private void uG_DanhSach_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (B)
                {
                    B = false;
                    return;
                }
                var id = dgv_DanhSach.ActiveRow.Cells["ID"].Text;
                if (string.IsNullOrEmpty(id)) return;
                foreach (var item in _listUpdate.Where(item => item.ID == int.Parse(id)))
                {
                    item.TenKhoa = dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Text;
                    return;
                }
                var hs = new Khoa
                {
                    ID = int.Parse(id),
                    TenKhoa = dgv_DanhSach.ActiveRow.Cells["TenKhoa"].Text,
                };
                _listUpdate.Add(hs);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void uG_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                var band = e.Layout.Bands[0];
                band.Columns["ID"].Hidden = true;
                band.Columns["STT"].CellAppearance.TextHAlign = HAlign.Center;
                band.Columns["STT"].CellActivation = Activation.NoEdit;
                band.Columns["STT"].CellAppearance.BackColor = Color.LightCyan;
                band.Columns["STT"].MaxWidth = 70;
                band.Override.HeaderAppearance.TextHAlign = HAlign.Center;
                band.Override.HeaderAppearance.FontData.SizeInPoints = 10;
                band.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

                #region Caption

                band.Columns["TenKhoa"].Header.Caption = FormResource.txtTenkhoa;

                #endregion
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        #endregion

        #region MenuStrip

        private void menuStrip_themdong_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        private void menuStrip_xoadong_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void menuStripHuy_Click(object sender, EventArgs e)
        {
            LoadFormDetail();
        }

        private void menuStrip_luulai_Click(object sender, EventArgs e)
        {
            SaveDetail();
        }

        #endregion

        private void FrmDanhmuckhoa_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
