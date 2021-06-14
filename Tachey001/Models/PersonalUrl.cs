namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalUrl")]
    public partial class PersonalUrl
    {
        [Key]
        public string MemberID { get; set; }

        [StringLength(80)]
        public string FbUrl { get; set; }

        [StringLength(80)]
        public string GoogleUrl { get; set; }

        [StringLength(80)]
        public string YouTubeUrl { get; set; }

        [StringLength(80)]
        public string BehanceUrl { get; set; }

        [StringLength(80)]
        public string PinterestUrl { get; set; }

        [StringLength(80)]
        public string BlogUrl { get; set; }
    }
}
