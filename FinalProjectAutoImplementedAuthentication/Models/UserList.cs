//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProjectAutoImplementedAuthentication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserList()
        {
            this.ListInfoes = new HashSet<ListInfo>();
        }
    
        public int ListId { get; set; }
        public string ListName { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public string OwnerId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListInfo> ListInfoes { get; set; }
    }
}
