namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public int AnswerID { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseID { get; set; }

        public int QuestionID { get; set; }

        [StringLength(128)]
        public string MemberID { get; set; }

        [StringLength(4000)]
        public string AnswerContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AnswerDate { get; set; }
    }
}
