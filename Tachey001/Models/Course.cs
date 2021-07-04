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
        public string CourseID { get; set; }

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
        public decimal? PreOrderPrice { get; set; }

        public int? TotalMinTime { get; set; }

        [StringLength(4000)]
        public string Introduction { get; set; }

        [Required]
        [StringLength(128)]
        public string MemberID { get; set; }

        [StringLength(50)]
        public string LecturerIdentity { get; set; }

        public int? CategoryID { get; set; }

        public int? CategoryDetailsID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public bool? CreateFinish { get; set; }

        public bool? CreateVerify { get; set; }

        [StringLength(4000)]
        public string PreviewVideo { get; set; }

        [StringLength(50)]
        public string CustomUrl { get; set; }

        public int? MainClick { get; set; }

        public int? CustomClick { get; set; }
    }
}
