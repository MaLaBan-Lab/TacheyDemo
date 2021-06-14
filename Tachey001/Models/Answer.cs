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
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string MemberID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4000)]
        public string QuestionContent { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime QuestionDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Likes { get; set; }
    }
}
