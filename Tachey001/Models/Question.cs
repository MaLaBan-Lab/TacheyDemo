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
        public int QuestionID { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseID { get; set; }

        [Required]
        [StringLength(128)]
        public string MemberID { get; set; }

        [StringLength(4000)]
        public string QuestionContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? QuestionDate { get; set; }
    }
}
