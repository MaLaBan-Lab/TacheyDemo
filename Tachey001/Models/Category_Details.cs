namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category Details")]
    public partial class Category_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category_Details()
        {
            Course = new HashSet<Course>();
        }

        [Key]
        [StringLength(50)]
        public string CategoryDetailsID { get; set; }

        [Required]
        [StringLength(50)]
        public string DetailsName { get; set; }

        public int CourseCategoryID { get; set; }

        public virtual Course_Category Course_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Course { get; set; }
    }
}
