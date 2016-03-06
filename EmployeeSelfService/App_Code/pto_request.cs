namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.pto_request")]
    public partial class pto_request
    {
        [Key]
        public int request_key { get; set; }

        public int employee_key { get; set; }

        [Required]
        [StringLength(45)]
        public string request_reason { get; set; }

        [Column(TypeName = "date")]
        public DateTime request_date_start { get; set; }

        [Column(TypeName = "date")]
        public DateTime request_date_end { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
