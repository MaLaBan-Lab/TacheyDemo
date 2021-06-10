namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseCategory")]
    public partial class CourseCategory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string CategoryName { get; set; }
    }
}
