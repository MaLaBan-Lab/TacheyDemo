namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Point")]
    public partial class Point
    {
        public int PointID { get; set; }

        [StringLength(128)]
        public string MemberID { get; set; }

        [Required]
        [StringLength(30)]
        public string PointName { get; set; }

        public int PointNum { get; set; }

        public int ValidDate { get; set; }

        public bool Status { get; set; }

        [StringLength(30)]
        public string GetTime { get; set; }

        [StringLength(30)]
        public string Deadline { get; set; }
    }
}
