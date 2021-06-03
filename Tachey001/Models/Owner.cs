namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Owner")]
    public partial class Owner
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OwnerTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; set; }

        public int? CourseID { get; set; }

        public int? HomeWorkID { get; set; }

        public virtual Course Course { get; set; }

        public virtual Course_HomeWork Course_HomeWork { get; set; }

        public virtual Member Member { get; set; }

        public virtual Owner_Type Owner_Type { get; set; }
    }
}
