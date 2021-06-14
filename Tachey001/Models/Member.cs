namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        public string MemberID { get; set; }

        [StringLength(30)]
        public string Account { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public bool? EmailStatus { get; set; }

        public DateTime? JoinTime { get; set; }

        [StringLength(6)]
        public string Sex { get; set; }

        [StringLength(10)]
        public string CountryRegion { get; set; }

        [StringLength(10)]
        public string City { get; set; }

        [StringLength(80)]
        public string Address { get; set; }

        public int? PostalCode { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(80)]
        public string Interest { get; set; }

        [StringLength(80)]
        public string Like { get; set; }

        [StringLength(80)]
        public string Expertise { get; set; }

        [StringLength(80)]
        public string About { get; set; }

        [StringLength(80)]
        public string InterestContent { get; set; }

        [StringLength(10)]
        public string Language { get; set; }

        [StringLength(4000)]
        public string Photo { get; set; }

        [StringLength(200)]
        public string Introduction { get; set; }

        [StringLength(80)]
        public string Theme { get; set; }

        [StringLength(30)]
        public string Profession { get; set; }

        public int? Point { get; set; }

        [StringLength(80)]
        public string Facebook { get; set; }

        [StringLength(80)]
        public string Line { get; set; }

        [StringLength(80)]
        public string Google { get; set; }
    }
}
