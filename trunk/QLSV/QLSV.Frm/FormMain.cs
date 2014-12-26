﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Infragistics.Win;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using Infragistics.Win.UltraWinExplorerBar;
using QLSV.Frm.Frm;
using Infragistics.Win.UltraWinTabControl;
using QLSV.Frm.FrmUserControl;
using Infragistics.Win.UltraWinGrid;

namespace QLSV.Frm
{
    public partial class FormMain : Form
    {
        private static FrmDangNhap _frmDangNhap;
        private static Frm_101_QuanLyNguoiDung _frmQuanLyNguoiDung;
        private static Frm_102_Danhmuckhoa _frmDanhmuckhoa;
        private static Frm_103_Danhmuclop _frmDanhmuclop;
        private static Frm_104_InportSinhVien _frmInportSinhVien;
        private static Frm_105_QuanLySinhVien _frmQuanlySinhVien;
        private static Frm_106_QuanLyKyThi _frmQuanLyKyThi;
        private static Frm_108_SxphongthichoSv _frmmChonSinhVienThi;
        private static Frm_107_Danhsachphongthi _frmDanhsachphongthi;
        private static Frm_109_SinhVienDuThi _frmSinhVienDuThi;
        private static Frm_201_InportDapAn _frmInportDapAn;
        private static Frm_203_InportBaiLam _frmInportBaiLam;
        private static Frm_202_DanhSachDapAn _frmDapAnCacMaDe;
        private static Frm_204_DanhSachBaiLam _frmDanhSachBaiLam;
        private static Frm_206_NhapThangDiem _frmNhapThangDiem;
        private static Frm_207_ChamDiemThi _frmChamDiemThi;
        private static Frm_208_ThongKeDiem _frmThongKeDiem;
        private bool _dangnhap = false;
        private bool _thoat = false;
        private int _idkythi ;

        public FormMain()
        {
            InitializeComponent();
            _frmDangNhap = new FrmDangNhap();
            _frmDangNhap.CheckDangNhap += CheckDangNhap;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            cboChonkythi.DataSource = LoadData.Load(18);
            LoadDefaul(null);
            _frmDangNhap.ShowDialog();
        }

        private static void ShowControl(Control frm, Control panel)
        {
            try
            {
                panel.Controls.Clear();
                frm.Dock = DockStyle.Fill;
                panel.Controls.Add(frm);
                panel.Controls[frm.Name].Focus();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void ChonChucNang(string strChucNang)
        {
            try
            {
                TabPageControl.Visible = true;
                switch (strChucNang)
                {
                    case "login":
                        _frmDangNhap.ShowDialog();
                        break;
                    case "logout":
                        _dangnhap = false;
                        LoadDefaul(null);
                        _frmDangNhap.txtMatKhau.Clear();
                        lbusername.Text = "";
                        _frmDangNhap.ShowDialog();
                        break;
                    case "doimatkhau":
                        break;
                    case "thoat":
                        
                        if (DialogResult.Yes ==
                            MessageBox.Show(FormResource.msgThoat, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question))
                        {
                            _thoat = true;
                            Application.Exit();
                        }
                        break;
                    case "101":
                        _frmQuanLyNguoiDung = new Frm_101_QuanLyNguoiDung();
                        Tabquanlynguoidung.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlynguoidung.Tab;
                        ShowControl(_frmQuanLyNguoiDung, pn_quanlynguoidung);
                        break;
                    case "102":
                        _frmDanhmuckhoa = new Frm_102_Danhmuckhoa();
                        Tabdanhmuckhoa.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuckhoa.Tab;
                        ShowControl(_frmDanhmuckhoa, pn_danhmuckhoa);
                        break;
                    case "103":
                        _frmDanhmuclop = new Frm_103_Danhmuclop();
                        Tabdanhmuclop.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhmuclop.Tab;
                        ShowControl(_frmDanhmuclop, pn_danhmuclop);
                        break;
                    case "104":
                        _frmInportSinhVien = new Frm_104_InportSinhVien();
                        _frmInportSinhVien.ShowDialog += ShowLoading;
                        _frmInportSinhVien.CloseDialog += KillLoading;
                        _frmInportSinhVien.UpdateDialog += UpdateLoading;
                        TabInportsinhvien.Tab.Visible = true;
                        Tabquanlysinhvien.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabInportsinhvien.Tab;
                        ShowControl(_frmInportSinhVien, pn_inportsinhvien);
                        break;
                    case "105":
                        _frmQuanlySinhVien = new Frm_105_QuanLySinhVien();
                        _frmQuanlySinhVien.ShowDialog += ShowLoading;
                        _frmQuanlySinhVien.CloseDialog += KillLoading;
                        _frmQuanlySinhVien.UpdateDialog += UpdateLoading;
                        Tabquanlysinhvien.Tab.Visible = true;
                        TabInportsinhvien.Tab.Visible = false;
                        TabPageControl.SelectedTab = Tabquanlysinhvien.Tab;
                        ShowControl(_frmQuanlySinhVien, pn_quanlysinhvien);
                        break;
                    case "106":
                        _frmQuanLyKyThi = new Frm_106_QuanLyKyThi();
                        Tabquanlykythi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabquanlykythi.Tab;
                        ShowControl(_frmQuanLyKyThi, pn_quanlykythi);
                        break;
                    case "107":
                        _frmDanhsachphongthi = new Frm_107_Danhsachphongthi();
                        Tabdanhsachphongthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabdanhsachphongthi.Tab;
                        ShowControl(_frmDanhsachphongthi, pn_danhsachphong);
                        break;
                    case "108":
                        _frmmChonSinhVienThi = new Frm_108_SxphongthichoSv(_idkythi);
                        _frmmChonSinhVienThi.ShowDialog += ShowLoading;
                        _frmmChonSinhVienThi.CloseDialog += KillLoading;
                        _frmmChonSinhVienThi.UpdateDialog += UpdateLoading;
                        TabSapxepphongthi.Tab.Visible = true;
                        Tabdaxepphong.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabSapxepphongthi.Tab;
                        ShowControl(_frmmChonSinhVienThi, pnl_sapxepphongthi);
                        break;
                    case "109":
                        _frmSinhVienDuThi = new Frm_109_SinhVienDuThi(_idkythi);
                        _frmSinhVienDuThi.ShowDialog += ShowLoading;
                        _frmSinhVienDuThi.CloseDialog += KillLoading;
                        _frmSinhVienDuThi.UpdateDialog += UpdateLoading;
                        Tabdaxepphong.Tab.Visible = true;
                        TabSapxepphongthi.Tab.Visible = false;
                        TabPageControl.SelectedTab = Tabdaxepphong.Tab;
                        ShowControl(_frmSinhVienDuThi, pnl_daxepphong);
                        break;
                    case "201":
                        _frmInportDapAn = new Frm_201_InportDapAn(_idkythi);
                        _frmInportDapAn.ShowDialog += ShowLoading;
                        _frmInportDapAn.CloseDialog += KillLoading;
                        _frmInportDapAn.UpdateDialog += UpdateLoading;
                        TabInportdapdan.Tab.Visible = true;
                        TabDanhsachbailam.Tab.Visible = false;
                        TabInportbailam.Tab.Visible = false;
                        TabDapanmade.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabInportdapdan.Tab;
                        ShowControl(_frmInportDapAn, pnl_Inportdapan);
                        break;
                    case "202":
                        _frmDapAnCacMaDe = new Frm_202_DanhSachDapAn(_idkythi);
                        _frmDapAnCacMaDe.ShowDialog += ShowLoading;
                        _frmDapAnCacMaDe.CloseDialog += KillLoading;
                        _frmDapAnCacMaDe.UpdateDialog += UpdateLoading;
                        TabDapanmade.Tab.Visible = true;
                        TabInportdapdan.Tab.Visible = false;
                        TabInportbailam.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabDapanmade.Tab;
                        ShowControl(_frmDapAnCacMaDe, pnl_Dapanmade);
                        break;
                    case "203":
                        _frmInportBaiLam = new Frm_203_InportBaiLam(_idkythi);
                        _frmInportBaiLam.ShowDialog += ShowLoading;
                        _frmInportBaiLam.CloseDialog += KillLoading;
                        _frmInportBaiLam.UpdateDialog += UpdateLoading;
                        TabInportbailam.Tab.Visible = true;
                        TabDanhsachbailam.Tab.Visible = false;
                        TabInportdapdan.Tab.Visible = false;
                        TabDapanmade.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabInportbailam.Tab;
                        ShowControl(_frmInportBaiLam, pnl_Inportbailam);
                        break;
                    case "204":
                        _frmDanhSachBaiLam = new Frm_204_DanhSachBaiLam(_idkythi);
                        _frmDanhSachBaiLam.ShowDialog += ShowLoading;
                        _frmDanhSachBaiLam.CloseDialog += KillLoading;
                        _frmDanhSachBaiLam.UpdateDialog += UpdateLoading;
                        TabDanhsachbailam.Tab.Visible = true;
                        TabInportbailam.Tab.Visible = false;
                        TabInportdapdan.Tab.Visible = false;
                        TabPageControl.SelectedTab = TabDanhsachbailam.Tab;
                        ShowControl(_frmDanhSachBaiLam, pnl_danhsachbailam);
                        break;
                    case "205":
                        var frm = new FrmKiemTraLoiLogic(_idkythi);
                        frm.Indanhsach();
                        break;
                    case "206":
                        _frmNhapThangDiem = new Frm_206_NhapThangDiem(_idkythi);
                        _frmNhapThangDiem.ShowDialog += ShowLoading;
                        _frmNhapThangDiem.CloseDialog += KillLoading;
                        _frmNhapThangDiem.UpdateDialog += UpdateLoading;
                        TabNhapthangdiem.Tab.Visible = true;
                        TabPageControl.SelectedTab = TabNhapthangdiem.Tab;
                        ShowControl(_frmNhapThangDiem, pnl_nhapthangdiem);
                        break;
                    case "207":
                        _frmChamDiemThi = new Frm_207_ChamDiemThi(_idkythi);
                        _frmChamDiemThi.ShowDialog += ShowLoading;
                        _frmChamDiemThi.CloseDialog += KillLoading;
                        _frmChamDiemThi.UpdateDialog += UpdateLoading;
                        Tabchamdiemthi.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabchamdiemthi.Tab;
                        ShowControl(_frmChamDiemThi, pnl_chamdiemthi);
                        break;
                    case "208":
                        _frmThongKeDiem = new Frm_208_ThongKeDiem(_idkythi);
                        _frmThongKeDiem.ShowDialog += ShowLoading;
                        _frmThongKeDiem.CloseDialog += KillLoading;
                        _frmThongKeDiem.UpdateDialog += UpdateLoading;
                        Tabthongkediem.Tab.Visible = true;
                        TabPageControl.SelectedTab = Tabthongkediem.Tab;
                        ShowControl(_frmThongKeDiem, pnl_thongkediem);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void LoadDefaul(string quyen)
        {
            switch (quyen)
            {
                case "quantri":
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    cboChonkythi.Enabled = true;
                    break;
                case "nguoidung":
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.True;
                    cboChonkythi.Enabled = true;
                    break;
                default:
                    TabPageControl.Visible = false;
                    MenuBar.Groups["hethong"].Items["login"].Settings.Enabled = DefaultableBoolean.True;
                    MenuBar.Groups["hethong"].Items["logout"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["hethong"].Items["doimatkhau"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["101"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["102"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["103"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["104"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["105"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["106"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["109"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["201"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["202"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["203"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["204"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["205"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["206"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["207"].Settings.Enabled = DefaultableBoolean.False;
                    MenuBar.Groups["chuongtrinh"].Items["208"].Settings.Enabled = DefaultableBoolean.False;
                    Tabquanlynguoidung.Tab.Visible = false;
                    Tabdanhmuckhoa.Tab.Visible = false;
                    Tabdanhmuclop.Tab.Visible = false;
                    TabInportsinhvien.Tab.Visible = false;
                    Tabquanlysinhvien.Tab.Visible = false;
                    Tabquanlykythi.Tab.Visible = false;
                    Tabdanhsachphongthi.Tab.Visible = false;
                    TabSapxepphongthi.Tab.Visible = false;
                    Tabdaxepphong.Tab.Visible = false;
                    TabInportdapdan.Tab.Visible = false;
                    TabDapanmade.Tab.Visible = false;
                    TabInportbailam.Tab.Visible = false;
                    TabDanhsachbailam.Tab.Visible = false;
                    TabNhapthangdiem.Tab.Visible = false;
                    Tabchamdiemthi.Tab.Visible = false;
                    Tabthongkediem.Tab.Visible = false;
                    cboChonkythi.Enabled = false;
                    cboChonkythi.Value = null;
                    break;
            }

        }

        private void CheckDangNhap(object sender, bool checkState, Taikhoan hs)
        {
            if (hs.Quyen == null)
            {
                LoadDefaul(null);
                return;
            }
            LoadDefaul(hs.Quyen);
            _dangnhap = true;
            lbusername.Text = hs.HoTen;
            _frmDangNhap.Close();
        }

        private void MenuBar_ItemClick(object sender, ItemEventArgs e)
        {
            try
            {
                ChonChucNang(e.Item.Key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        #region Exit

        private void Dong_Esc()
        {
            if (Tabquanlynguoidung.Tab.Active)
            {
                Tabquanlynguoidung.Tab.Visible = false;
            }
            else if (Tabdanhmuckhoa.Tab.Active)
            {
                Tabdanhmuckhoa.Tab.Visible = false;
            }
            else if (Tabdanhmuclop.Tab.Active)
            {
                Tabdanhmuclop.Tab.Visible = false;
            }
            else if (TabInportsinhvien.Tab.Active)
            {
                TabInportsinhvien.Tab.Visible = false;
            }
            else if (Tabquanlysinhvien.Tab.Active)
            {
                Tabquanlysinhvien.Tab.Visible = false;
            }
            else if (Tabdanhsachphongthi.Tab.Active)
            {
                Tabdanhsachphongthi.Tab.Visible = false;
            }
            else if (Tabquanlykythi.Tab.Active)
            {
                Tabquanlykythi.Tab.Visible = false;
            }
            else if (TabSapxepphongthi.Tab.Active)
            {
                TabSapxepphongthi.Tab.Visible = false;
            }else if (Tabdaxepphong.Tab.Active)
            {
                Tabdaxepphong.Tab.Visible = false;
            }
            else if (TabInportdapdan.Tab.Active)
            {
                TabInportdapdan.Tab.Visible = false;
            }
            else if (TabDapanmade.Tab.Active)
            {
                TabDapanmade.Tab.Visible = false;
            }
            else if (TabInportbailam.Tab.Active)
            {
                TabInportbailam.Tab.Visible = false;
            }
            else if (TabDanhsachbailam.Tab.Active)
            {
                TabDanhsachbailam.Tab.Visible = false;
            }
        }

        private void Huy_F12()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.LoadForm();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.LoadForm();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.LoadForm();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Huy();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.LoadForm();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.LoadForm();
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Huy();
            }
            else if (TabDanhsachbailam.Tab.Active)
            {
                
            }
        }

        private void Luu_F5()
        {
            btnLuu.Focus();
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.Save();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.Save();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.Save();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.Ghi();
                ChonChucNang("105");
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Save();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Save();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.Save();
                cboChonkythi.DataSource = LoadData.Load(18);
            }
            else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
            {
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.Save();
            }
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.Ghi();
                ChonChucNang("202");
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Save();
            }
            else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {
                _frmInportBaiLam.Ghi();
                ChonChucNang("204");
            }
            else if (TabDanhsachbailam.Tab.Active)
            {
                //_frmDanhSachBaiLam.Save();
            }
            else if (TabNhapthangdiem.Tab.Active)
            {
                _frmNhapThangDiem.Ghi();
            }
            else if (Tabchamdiemthi.Tab.Active)
            {
                _frmChamDiemThi.Ghi();
            }
        }

        private void Xoa_F3()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.Xoa();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.Xoa();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.Xoa();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Xoa();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Xoa();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.Xoa();
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.Xoa();
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.Xoa();
            }
            else if (TabDanhsachbailam.Tab.Active)
            {
                _frmDanhSachBaiLam.Xoa();
            }
        }

        private void Xoadong_F11()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.uG_DeleteRow();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.uG_DeleteRow();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.uG_DeleteRow();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.uG_DeleteRow();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.uG_DeleteRow();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.uG_DeleteRow();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.uG_DeleteRow();
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
                _frmSinhVienDuThi.uG_DeleteRow();
            }
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.uG_DeleteRow();
            }
            else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {

            }
            else if (TabDanhsachbailam.Tab.Active)
            {

            }
        }

        private void Themmoi_Insert()
        {
            if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
            {
                _frmQuanLyNguoiDung.uG_InsertRow();
            }
            else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
            {
                _frmDanhmuckhoa.uG_InsertRow();
            }
            else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
            {
                _frmDanhmuclop.uG_InsertRow();
            }
            else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.uG_InsertRow();
            }
            else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Themmoi();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.uG_InsertRow();
            }
            else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
            {
                _frmQuanLyKyThi.uG_InsertRow();
            }
            else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.uG_InsertRow();
            }
            else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {
                //_frmInportBaiLam.uG_InsertRow();
            }
            else if (TabDanhsachbailam.Tab.Active)
            {

            }
        }

        private void In_F10()
        {
            if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
            {
                _frmQuanlySinhVien.Rptdanhsach();
            }
            else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
            {
                _frmDanhsachphongthi.Rptdanhsach();
            }
            else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
            {
               _frmSinhVienDuThi.InDanhSach();
            }
            else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
            {
                _frmDapAnCacMaDe.InDanhSach();
            }
            else if (TabDanhsachbailam.Tab.Active)
            {

            }
            else if (Tabchamdiemthi.Tab.Active)
            {
                _frmChamDiemThi.InDanhSach();
            }
            else if (Tabthongkediem.Tab.Active)
            {
                _frmThongKeDiem.InDanhSach();
            }

        }

        private void NapDuLieu_F8()
        {
            if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
            {
                _frmInportSinhVien.Napdulieu();
            }
            if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
            {
                _frmInportDapAn.Napdulieu();
            }
            if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
            {
               _frmInportBaiLam.Napdulieu();
            }
            else if (TabDanhsachbailam.Tab.Active)
            {

            }
        }

        #endregion

        #region Button

        private void btnDong_Click(object sender, EventArgs e)
        {
            Dong_Esc();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy_F12();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Luu_F5();
        }

        private void btnXoadong_Click(object sender, EventArgs e)
        {
            Xoadong_F11();
        }

        private void btnthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi_Insert();
        }

        private void btnInds_Click(object sender, EventArgs e)
        {
            In_F10();
        }

        private void btnNapDuLieu_Click(object sender, EventArgs e)
        {
            NapDuLieu_F8();
        }

        #endregion

        #region Loadding

        private FrmLoadding _loading;

        private void ShowLoading(object sender, string msg)
        {
            _loading = new FrmLoadding();
            _loading.Update(msg);
            _loading.ShowDialog();
        }

        private void KillLoading(object sender)
        {
            try
            {
                if (_loading != null)
                {
                    _loading.Invoke((Action)(() =>
                    {
                        _loading.Close();
                        //_loading.Dispose();
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

        public void UpdateLoading(object sender, string strInfo)
        {
            if (_loading != null)
            {
                _loading.Invoke((Action)(() => _loading.Update(strInfo)));
            }
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.F):
                    if (!_dangnhap) break;
                    var frmChon = new FrmChonChucNang();
                    frmChon.ShowDialog();
                    if (frmChon.StrChucNang != "" && frmChon.StrChucNang != "login")
                    {
                        ChonChucNang(frmChon.StrChucNang);
                    }
                    break;
                case (Keys.Escape):
                    Dong_Esc();
                    break;
                case (Keys.F3):
                    Xoa_F3();
                    break;
                case (Keys.F5):
                    Luu_F5();
                    break;
                case (Keys.F8):
                    NapDuLieu_F8();
                    break;
                case (Keys.F10):
                    In_F10();
                    break;
                case (Keys.F11):
                    Xoadong_F11();
                    break;
                case (Keys.F12):
                    Huy_F12();
                    break;
                case (Keys.Insert):
                    Themmoi_Insert();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(_thoat) return;
                var a = MessageBox.Show(@"Bạn có muốn thoát chương trình không?",
                    @"Thông báo",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != DialogResult.OK;
                e.Cancel = a;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void cboChonkythi_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            cboChonkythi.Rows.Band.Columns["ID"].Hidden = true;
            cboChonkythi.Rows.Band.Columns["MaKT"].Width = 70;
            cboChonkythi.Rows.Band.Columns["TenKT"].Width = 250;
            cboChonkythi.Rows.Band.ColHeadersVisible = false;
        }

        private void cboChonkythi_ValueChanged(object sender, EventArgs e)
        {
            lbXoa.Focus();
            var index = cboChonkythi.Value;
            if(index == null) return;
            if (!IsNumber(index.ToString())) return;
            _idkythi = (int) index;

            #region TabControl

            TabSapxepphongthi.Tab.Visible = false;
            Tabdaxepphong.Tab.Visible = false;
            TabInportdapdan.Tab.Visible = false;
            TabDapanmade.Tab.Visible = false;
            TabInportbailam.Tab.Visible = false;
            TabDanhsachbailam.Tab.Visible = false;
            TabNhapthangdiem.Tab.Visible = false;
            Tabchamdiemthi.Tab.Visible = false;
            Tabthongkediem.Tab.Visible = false;

            #endregion

            #region menu

            MenuBar.Groups["chuongtrinh"].Items["107"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["108"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["109"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["201"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["202"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["203"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["204"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["205"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["206"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["207"].Settings.Enabled = DefaultableBoolean.True;
            MenuBar.Groups["chuongtrinh"].Items["208"].Settings.Enabled = DefaultableBoolean.True;

            #endregion
        }

        private static bool IsNumber(string pText)
        {
            var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void TabPageControl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                if (!_dangnhap) return;
                var bCheck = false;
                if (Tabquanlynguoidung.Tab.Visible && Tabquanlynguoidung.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhmuckhoa.Tab.Visible && Tabdanhmuckhoa.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhmuclop.Tab.Visible && Tabdanhmuclop.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabInportsinhvien.Tab.Visible && TabInportsinhvien.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabquanlysinhvien.Tab.Visible && Tabquanlysinhvien.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabdanhsachphongthi.Tab.Visible && Tabdanhsachphongthi.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabquanlykythi.Tab.Visible && Tabquanlykythi.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabSapxepphongthi.Tab.Visible && TabSapxepphongthi.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabdaxepphong.Tab.Visible && Tabdaxepphong.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabInportdapdan.Tab.Visible && TabInportdapdan.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabDapanmade.Tab.Visible && TabDapanmade.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (TabInportbailam.Tab.Visible && TabInportbailam.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = true;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = true;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = true;
                    btnXoadong.Visible = true;
                    btnLuu.Visible = true;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabDanhsachbailam.Tab.Visible && TabDanhsachbailam.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = true;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (TabNhapthangdiem.Tab.Visible && TabNhapthangdiem.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = false;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = true;
                    btnHuy.Visible = true;
                    btnDong.Visible = true;
                }
                else if (Tabchamdiemthi.Tab.Visible && Tabchamdiemthi.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                else if (Tabthongkediem.Tab.Visible && Tabthongkediem.Tab.Active)
                {
                    bCheck = true;
                    lbInsert.Visible = false;
                    lbXoa.Visible = false;
                    btnNapDuLieu.Visible = false;
                    btnInds.Visible = true;
                    btnthemmoi.Visible = false;
                    btnXoadong.Visible = false;
                    btnLuu.Visible = false;
                    btnHuy.Visible = false;
                    btnDong.Visible = true;
                }
                if (!bCheck) TabPageControl.Visible = false;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (DateTime.Now.Hour > 12)
        //    {
        //        lbtime.Text = DateTime.Now.ToString();
        //    }
        //    else
        //    {
        //        lbtime.Text = DateTime.Now.ToString();
        //    }
        //}

    }
}