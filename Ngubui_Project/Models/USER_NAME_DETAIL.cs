//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ngubui_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class USER_NAME_DETAIL
    {
        public int ID_User { get; set; }
        public string TenTaiKhoan { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string ThanhPho { get; set; }
        public string TinhHuyen { get; set; }
        public string MaBuuDien { get; set; }
        public string QuocGia { get; set; }
        public byte[] AnhDaiDien { get; set; }
    
        public virtual USER_NAME USER_NAME { get; set; }
    }
}