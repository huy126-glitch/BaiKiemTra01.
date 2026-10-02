using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsAutoSpeed
{
    // ==========================================
    // A. Abstract Class PhuongTien (Lớp cha trừu tượng)
    // ==========================================
    public abstract class PhuongTien
    {
        // Private Fields
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        // Properties với Encapsulation & Validation
        public string MaPT
        {
            get => _maPT;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _maPT = "PT000";
                }
                else
                {
                    _maPT = value.Trim();
                }
            }
        }

        public string TenHang
        {
            get => _tenHang;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tên hãng không được để trống!");
                }
                _tenHang = value.Trim();
            }
        }

        public int NamSanXuat
        {
            get => _namSanXuat;
            set
            {
                int currentYear = DateTime.Now.Year;
                if (value < 1900 || value > currentYear)
                {
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                }
                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get => _giaGoc;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Giá gốc phải lớn hơn 0!");
                }
                _giaGoc = value;
            }
        }

        // Constructor khởi tạo đầy đủ tham số
        public PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        // Abstract Method
        public abstract decimal TinhGiaLanBanh();

        // Virtual Method
        public virtual string GetInfo()
        {
            return $"Mã: {MaPT} | Hãng: {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }

    // ==========================================
    // B. Class OTo kế thừa từ PhuongTien
    // ==========================================
    public class OTo : PhuongTien
    {
        private int _soChongoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get => _soChongoi;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
                }
                _soChongoi = value;
            }
        }

        public double DungTichDongCo
        {
            get => _dungTichDongCo;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
                }
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        // Override TinhGiaLanBanh()
        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                // Thuế trước bạ (12%) + Thuế tiêu thụ đặc biệt (30%)
                decimal thueTruocBa = GiaGoc * 0.12m;
                decimal thueTTDB = GiaGoc * 0.30m;
                return GiaGoc + thueTruocBa + thueTTDB;
            }
            else
            {
                // Thuế trước bạ (10%)
                decimal thueTruocBa = GiaGoc * 0.10m;
                return GiaGoc + thueTruocBa;
            }
        }

        // Override GetInfo()
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Số chỗ: {SoChoNgoi} | Dung tích đ/cơ: {DungTichDongCo}L";
        }
    }

    // ==========================================
    // C. Class XeMay kế thừa từ PhuongTien
    // ==========================================
    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get => _dungTichXylanh;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Dung tích xi lanh phải lớn hơn 0!");
                }
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        // Override TinhGiaLanBanh()
        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
            {
                // Thuế trước bạ (2%)
                return GiaGoc + (GiaGoc * 0.02m);
            }
            else
            {
                // Thuế trước bạ (5%)
                return GiaGoc + (GiaGoc * 0.05m);
            }
        }

        // Override GetInfo()
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Dung tích xi-lanh: {DungTichXylanh}cc";
        }
    }
    // ==========================================
    // D. Class QuanLyPhuongTien (Quản lý Tập hợp)
    // ==========================================
    public class QuanLyPhuongTien
    {
        private List<PhuongTien> _danhSach = new List<PhuongTien>();

        // 1. AddPhuongTien
        public void AddPhuongTien(PhuongTien pt)
        {
            if (pt != null)
            {
                _danhSach.Add(pt);
            }
        }

        // 2. DisplayAll
        public void DisplayAll()
        {
            Console.WriteLine("--- DANH SÁCH TOÀN BỘ PHƯƠNG TIỆN ---");
            foreach (var pt in _danhSach)
            {
                Console.WriteLine($"{pt.GetInfo()} => Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        // 3. FindMaxGiaLanBanh (Đa hình)
        public PhuongTien FindMaxGiaLanBanh()
        {
            if (_danhSach.Count == 0) return null;

            PhuongTien maxPT = _danhSach[0];
            foreach (var pt in _danhSach)
            {
                if (pt.TinhGiaLanBanh() > maxPT.TinhGiaLanBanh())
                {
                    maxPT = pt;
                }
            }
            return maxPT;
        }

        // 4. SearchByName (LINQ)
        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<PhuongTien>();

            return _danhSach
                .Where(pt => pt.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    // ==========================================
    // CHƯƠNG TRÌNH KIỂM THỬ (TEST CASES)
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            QuanLyPhuongTien ql = new QuanLyPhuongTien();

            Console.WriteLine("========== BẮT ĐẦU CHẠY KỊCH BẢN KIỂM THỬ (TEST CASES) ==========\n");

            // --- TC01: Kiểm tra Validation Năm sản xuất (< 1900) ---
            Console.WriteLine("--> TC01: Kiểm tra Validation Năm sản xuất (NamSanXuat = 1850)");
            try
            {
                OTo otoLoi = new OTo("OT001", "Toyota", 1850, 1000000000m, 5, 2.0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[KẾT QUẢ KỲ VỌNG TRẢ VỀ SEED EXCEPTION]: {ex.Message}");
            }
            Console.WriteLine();

            // Khởi tạo các đối tượng chuẩn cho các TC tiếp theo
            OTo oto1 = new OTo("OT002", "Toyota Camry", 2022, 1000000000m, 5, 2.5); // 1 Tỷ
            XeMay xemay1 = new XeMay("XM001", "Honda SH", 2023, 50000000m, 150);     // 50 Triệu

            // --- TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô (5 chỗ, GiáGốc = 1,000,000,000 VNĐ) ---
            Console.WriteLine("--> TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô (5 chỗ)");
            decimal giaLanBanhOTo = oto1.TinhGiaLanBanh();
            Console.WriteLine($"Giá gốc: {oto1.GiaGoc:N0} VNĐ");
            Console.WriteLine($"Giá lăn bánh thực tế tính ra: {giaLanBanhOTo:N0} VNĐ");
            Console.WriteLine($"Kỳ vọng: 1,420,000,000 VNĐ => {(giaLanBanhOTo == 1420000000m ? "PASSED" : "FAILED")}\n");

            // --- TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy (150cc, GiáGốc = 50,000,000 VNĐ) ---
            Console.WriteLine("--> TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy (150cc)");
            decimal giaLanBanhXeMay = xemay1.TinhGiaLanBanh();
            Console.WriteLine($"Giá gốc: {xemay1.GiaGoc:N0} VNĐ");
            Console.WriteLine($"Giá lăn bánh thực tế tính ra: {giaLanBanhXeMay:N0} VNĐ");
            Console.WriteLine($"Kỳ vọng: 51,000,000 VNĐ => {(giaLanBanhXeMay == 51000000m ? "PASSED" : "FAILED")}\n");

            // --- TC04: Kiểm tra Đa hình List<PhuongTien> ---
            Console.WriteLine("--> TC04: Kiểm tra Đa hình List<PhuongTien>");
            ql.AddPhuongTien(oto1);
            ql.AddPhuongTien(xemay1);
            ql.DisplayAll();
            Console.WriteLine();

            // --- TC05: Kiểm tra Tìm Giá Lăn Bánh Max ---
            Console.WriteLine("--> TC05: Kiểm tra Tìm Giá Lăn Bánh Max");
            PhuongTien maxPT = ql.FindMaxGiaLanBanh();
            if (maxPT != null)
            {
                Console.WriteLine($"Phương tiện có giá lăn bánh cao nhất: {maxPT.TenHang}");
                Console.WriteLine($"Giá lăn bánh: {maxPT.TinhGiaLanBanh():N0} VNĐ");
                bool isCorrect = (maxPT == oto1) && (maxPT.TinhGiaLanBanh() == 1420000000m);
                Console.WriteLine($"Kết quả kiểm thử TC05 => {(isCorrect ? "PASSED (Đã trả về đúng đối tượng Ô tô 5 chỗ giá 1.42 tỷ)" : "FAILED")}");
            }

            Console.WriteLine("\n========== HOÀN THÀNH TẤT CẢ TEST CASES ==========");
        }
    }
}
