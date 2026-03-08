// Models/ChiTietDonHang.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebKhoaHoc.Models;

public class ChiTietDonHang
{
    [Key]
    public int Id { get; set; }
    public int DonHangId { get; set; }
    public int KhoaHocId { get; set; }
    public decimal GiaMua { get; set; }

    [ForeignKey("DonHangId")]
    public virtual DonHang? DonHang { get; set; }
    [ForeignKey("KhoaHocId")]
    public virtual KhoaHoc? KhoaHoc { get; set; }
}