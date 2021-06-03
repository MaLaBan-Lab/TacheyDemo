namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course HomeWork")]
    public partial class Course_HomeWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course_HomeWork()
        {
            Owner = new HashSet<Owner>();
        }

        [Key]
        public int HomeWorkID { get; set; }

        [Required]
        [StringLength(50)]
        public string ChapterID { get; set; }

        public int MemberID { get; set; }

        [Required]
        [StringLength(50)]
        public string HWName { get; set; }

        [StringLength(4000)]
        public string HWDescription { get; set; }

        [StringLength(4000)]
        public string HWURL { get; set; }

        [StringLength(4000)]
        public string HWImgURL { get; set; }

        public virtual Course_Chapter Course_Chapter { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Owner> Owner { get; set; }
    }
}
