﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QLSV.Core.DataConnection;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public class SinhVienSql
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Lấy ra thông tin của tất cả sinh viên
        /// </summary>
        /// <returns>trả về 1 bảng</returns>
        public static DataTable LoadSinhVien()
        {
            try
            {
                const string str = "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID ORDER BY TenSinhVien";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra danh sách mã sinh biên
        /// </summary>
        /// <returns></returns>
        public static DataTable LoadMaSinhVien()
        {
            try
            {
                const string str = "select MaSinhVien from SinhVien ORDER BY TenSinhVien";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra thông tin các khoa
        /// </summary>
        /// <returns>trả về table khoa</returns>
        public static DataTable LoadKhoa()
        {
            try
            {
                const string str = "select * from Khoa";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy ra thông tin các khoa
        /// </summary>
        /// <returns>trả về table khoa</returns>
        public static DataTable LoadLop()
        {
            try
            {
                const string str = "select * from Lop";
                return Conn.getTable(str);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo khoa
        /// </summary>
        /// <returns>trả về bảng sinh  viên</returns>
        public static DataTable Timkiemtheokhoa(int id)
        {
            try
            {
                try
                {
                    var str =
                        "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID and Khoa.ID = " +
                        id + " ORDER BY TenSinhVien";
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo Lop
        /// </summary>
        /// <returns>trả về bảng thông tin sinh  viên</returns>
        public static DataTable Timkiemtheolop(int id)
        {
            try
            {
                try
                {
                    var str =
                        "select SinhVien.ID,MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,MaLop,TenKhoa from SinhVien,Lop,Khoa where SinhVien.IdLop = Lop.ID and Lop.IdKhoa = Khoa.ID and Lop.ID = " +
                        id + "ORDER BY TenSinhVien";
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra danh sách lớp thuộc khoa
        /// </summary>
        /// <returns>trả về bảng thông tin lop</returns>
        public static DataTable LoadLopTheoKhoa(int id)
        {
            try
            {
                try
                {
                    var str = "select * from Lop where IdKhoa = " + id;
                    return Conn.getTable(str);
                }
                catch (Exception ex)
                {
                    Log2File.LogExceptionToFile(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Thêm mới sinh viên
        /// </summary>
        /// <param name="list">danh sách sinh viên</param>
        public static void ThemSinhVien(IList<SinhVien> list)
        {
            try
            {
                int a = list.Count;
                foreach (var sql in list.Select(item =>
                                "insert into SinhVien(MaSinhVien,HoSinhVien,TenSinhVien,NgaySinh,IdLop) values(N'" +
                                item.MaSinhVien + "',N'" + item.HoSinhVien + "',N'" + item.TenSinhVien + "','" +
                                item.NgaySinh + "'," + item.IdLop + ")"))
                {
                    Conn.ExcuteQuerySql(sql);
                }

            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// lấy ra bản ghi lớp vừa được thêm khi chưa có lớp
        /// </summary>
        /// <param name="ma">mã lớp</param>
        /// <param name="id">id khoa</param>
        /// <returns></returns>
        public static Lop ThemLop(string ma, int id)
        {
            try
            {
                var sql1 = "insert into Lop(MaLop,idKhoa) values(N'" + ma + "'," + id + ")";
                var sql2 = "select * from Lop where MaLop = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.getTable(sql2);
                var newLop = new Lop
                {
                    MaLop = tb.Rows[0]["MaLop"].ToString(),
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    IdKhoa = int.Parse(tb.Rows[0]["IdKhoa"].ToString())
                };
                return newLop;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// lấy ra bản ghi khoa vừa được thêm vào bảng khoa
        /// </summary>
        /// <param name="ma">mã khoa</param>
        /// <returns></returns>
        public static Khoa ThemKhoa(string ma)
        {
            try
            {
                var sql1 = "insert into Khoa(TenKhoa) values(N'" + ma + "')";
                var sql2 = "select * from Khoa where TenKhoa = N'" + ma + "'";
                Conn.ExcuteQuerySql(sql1);
                var tb = Conn.getTable(sql2);
                var newKhoa = new Khoa
                {
                    ID = int.Parse(tb.Rows[0]["ID"].ToString()),
                    MaKhoa = tb.Rows[0]["MaKhoa"].ToString(),
                    TenKhoa = tb.Rows[0]["TenKhoa"].ToString()
                };
                return newKhoa;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Load danh sách lớp theo mã lớp
        /// </summary>
        /// <param name="ma"></param>
        /// <returns>trả về 1 đối tượng lớp</returns>
        public static Lop LoadLop(string ma)
        {
            try
            {
                var sql = "select * from Lop where MaLop = '" + ma + "'";
                var tb = Conn.getTable(sql);
                var lop = new Lop();
                foreach (DataRow row in tb.Rows)
                {
                    lop.ID = int.Parse(row["ID"].ToString());
                    lop.MaLop = row["MaLop"].ToString();
                    lop.IdKhoa = int.Parse(row["IdKhoa"].ToString());
                }
                return lop;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static void SuaSinhVien(SinhVien hs)
        {
            try
            {
                var sql = "UPDATE SinhVien SET HoSinhVien = N'" + hs.HoSinhVien + "', TenSinhVien = N'" + hs.TenSinhVien +
                          "', NgaySinh = N'" + hs.NgaySinh + "', IdLop = " + hs.IdLop + " WHERE MaSinhVien = N'" +
                          hs.MaSinhVien + "'";
                Conn.ExcuteQuerySql(sql);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
