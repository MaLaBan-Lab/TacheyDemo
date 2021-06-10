namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Homework
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HwkID { get; set; }

        [Required]
        [StringLength(80)]
        public string MemberID { get; set; }

        [Required]
        [StringLength(80)]
        public string Hwkchapter { get; set; }

        [StringLength(80)]
        public string Hwkname { get; set; }

        [StringLength(80)]
        public string Hwkdescription { get; set; }

        [StringLength(80)]
        public string HwkPhoto { get; set; }

        public int? HwkLike { get; set; }

        [StringLength(80)]
        public string HwkUrl { get; set; }
    }
}
