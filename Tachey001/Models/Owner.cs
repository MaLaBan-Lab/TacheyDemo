namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Owner")]
    public partial class Owner
    {
        [Key]
        [Column(Order = 0)]
        public string MemberID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string CourseID { get; set; }
    }
}
