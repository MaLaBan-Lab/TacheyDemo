namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course Score")]
    public partial class Course_Score
    {
        [Key]
        public int ScoreID { get; set; }

        public int MemberID { get; set; }

        public int CourseID { get; set; }

        public int Score { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Content { get; set; }

        [StringLength(4000)]
        public string ToTeacher { get; set; }

        [StringLength(4000)]
        public string ToHahow { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScoreDate { get; set; }

        public virtual Course Course { get; set; }

        public virtual Member Member { get; set; }
    }
}
