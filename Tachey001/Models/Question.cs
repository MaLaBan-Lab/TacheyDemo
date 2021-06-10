namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public bool? BeforeAfter { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        public int MemberID { get; set; }

        public int CourseID { get; set; }

        [StringLength(50)]
        public string ChapterID { get; set; }

        [StringLength(50)]
        public string UnitID { get; set; }

        public bool Done { get; set; }

        public int Likes { get; set; }

        [Required]
        [StringLength(4000)]
        public string QuestionContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime QuestionDate { get; set; }
    }
}
