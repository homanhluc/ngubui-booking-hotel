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
    
    public partial class WHISLIST_ROOM
    {
        public Nullable<int> ID_User { get; set; }
        public Nullable<int> ID_Room { get; set; }
        public Nullable<System.DateTime> NgayThemVao { get; set; }
        public int ID_Whislist { get; set; }
    
        public virtual ROOM_NAME ROOM_NAME { get; set; }
        public virtual USER_NAME USER_NAME { get; set; }
    }
}
