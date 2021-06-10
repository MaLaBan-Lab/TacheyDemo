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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PointID { get; set; }

        public int MemberID { get; set; }

        [Required]
        [StringLength(30)]
        public string PointName { get; set; }

        public int PointNum { get; set; }

        public DateTime GetTime { get; set; }

        public DateTime Deadline { get; set; }

        public bool Status { get; set; }
    }
}
