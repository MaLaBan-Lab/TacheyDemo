namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course Unit")]
    public partial class Course_Unit
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ChapterID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UnitID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string UnitName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4000)]
        public string URL { get; set; }

        [StringLength(4000)]
        public string PS { get; set; }

        public virtual Course_Chapter Course_Chapter { get; set; }
    }
}
