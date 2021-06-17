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
        public int TestChapterID { get; set; }

        [StringLength(200)]
        public string ChapterName { get; set; }

        [StringLength(200)]
        public string CourseID { get; set; }

        public int? ChapterID { get; set; }
    }
}
