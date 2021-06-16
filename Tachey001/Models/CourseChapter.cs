namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseChapter")]
    public partial class CourseChapter
    {
        [Key]
        public string CourseID { get; set; }

        [StringLength(50)]
        public string ChapterID { get; set; }

        [StringLength(200)]
        public string ChapterName { get; set; }
    }
}
