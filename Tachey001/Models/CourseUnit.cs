namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseUnit")]
    public partial class CourseUnit
    {
        [StringLength(128)]
        public string CourseID { get; set; }

        [StringLength(50)]
        public string ChapterID { get; set; }

        [StringLength(50)]
        public string UnitID { get; set; }

        [StringLength(200)]
        public string UnitName { get; set; }

        [Key]
        [StringLength(4000)]
        public string CourseURL { get; set; }

        [StringLength(4000)]
        public string PS { get; set; }
    }
}
