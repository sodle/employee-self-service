namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.skill")]
    public partial class skill
    {
        [Key]
        public int skill_line_id { get; set; }

        public int employee_key { get; set; }

        [Required]
        [StringLength(45)]
        public string skill_text { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
