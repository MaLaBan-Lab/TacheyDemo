namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course Chapter")]
    public partial class Course_Chapter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course_Chapter()
        {
            Course_HomeWork = new HashSet<Course_HomeWork>();
            Course_Unit = new HashSet<Course_Unit>();
        }

        public int CourseID { get; set; }

        [Key]
        [StringLength(50)]
        public string ChapterID { get; set; }

        [Required]
        [StringLength(200)]
        public string ChapterName { get; set; }

        public virtual Course Course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_HomeWork> Course_HomeWork { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Unit> Course_Unit { get; set; }
    }
}
