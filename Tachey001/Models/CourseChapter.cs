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
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ChapterID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string ChapterName { get; set; }
    }
}
