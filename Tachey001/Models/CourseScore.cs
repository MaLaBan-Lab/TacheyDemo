namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseScore")]
    public partial class CourseScore
    {
        [Key]
        [Column(Order = 0)]
        public string CourseID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string MemberID { get; set; }

        public int? Score { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string ScoreContent { get; set; }

        [StringLength(4000)]
        public string ToTeacher { get; set; }

        [StringLength(4000)]
        public string ToTachey { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ScoreDate { get; set; }
    }
}
