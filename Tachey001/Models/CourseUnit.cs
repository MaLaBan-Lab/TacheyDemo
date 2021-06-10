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
        [Required]
        [StringLength(50)]
        public string ChapterID { get; set; }

        [Key]
        [StringLength(50)]
        public string UnutID { get; set; }

        [Required]
        [StringLength(200)]
        public string UnitName { get; set; }

        [Required]
        [StringLength(4000)]
        public string CourseURI { get; set; }

        [StringLength(4000)]
        public string PS { get; set; }
    }
}
