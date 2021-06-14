namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryDetail")]
    public partial class CategoryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DetailID { get; set; }

        [Required]
        [StringLength(40)]
        public string DetailName { get; set; }

        public int CategoryID { get; set; }
    }
}
