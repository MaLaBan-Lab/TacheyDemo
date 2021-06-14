namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseBuyed")]
    public partial class CourseBuyed
    {
        [Key]
        public string CourseID { get; set; }

        public int OrderID { get; set; }

        [Required]
        [StringLength(128)]
        public string MemberID { get; set; }
    }
}
