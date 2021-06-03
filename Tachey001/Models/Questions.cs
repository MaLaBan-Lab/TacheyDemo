namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questions()
        {
            Answer = new HashSet<Answer>();
        }

        [Key]
        public int QuestionID { get; set; }

        public int MemberID { get; set; }

        public int CourseID { get; set; }

        [Column(TypeName = "date")]
        public DateTime QDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string QContent { get; set; }

        public int Likes { get; set; }

        public bool AfterBefore { get; set; }

        public bool Done { get; set; }

        [StringLength(50)]
        public string ChapterID { get; set; }

        [StringLength(50)]
        public string UnitID { get; set; }

        public virtual Course Course { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
