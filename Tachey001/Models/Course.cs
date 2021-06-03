namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Course_Chapter = new HashSet<Course_Chapter>();
            Course_Score = new HashSet<Course_Score>();
            Owner = new HashSet<Owner>();
            Questions = new HashSet<Questions>();
        }

        public int CourseID { get; set; }

        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(4000)]
        public string TitlePageImageURL { get; set; }

        [StringLength(4000)]
        public string MarketingImageURL { get; set; }

        [StringLength(4000)]
        public string Tool { get; set; }

        [StringLength(100)]
        public string CourseLevel { get; set; }

        [StringLength(4000)]
        public string Effect { get; set; }

        [StringLength(4000)]
        public string CoursePerson { get; set; }

        [Column(TypeName = "money")]
        public decimal OriginalPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal PreOrderPrice { get; set; }

        public int TotalMinTime { get; set; }

        [StringLength(4000)]
        public string Introduction { get; set; }

        [Required]
        [StringLength(4000)]
        public string CourseURL { get; set; }

        [Required]
        [StringLength(50)]
        public string LecturerIdentity { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryDetailsID { get; set; }

        public virtual Category_Details Category_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Chapter> Course_Chapter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Score> Course_Score { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Owner> Owner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
