namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.certification")]
    public partial class certification
    {
        [Key]
        public int cert_line_id { get; set; }

        public int employee_key { get; set; }

        [Required]
        [StringLength(45)]
        public string cert_text { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
