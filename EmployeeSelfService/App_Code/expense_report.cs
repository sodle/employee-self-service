namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.expense_report")]
    public partial class expense_report
    {
        [Key]
        public int expense_key { get; set; }

        public int employee_key { get; set; }

        [Required]
        [StringLength(45)]
        public string expense_title { get; set; }

        [Required]
        [StringLength(45)]
        public string expense_description { get; set; }

        public int expense_cost { get; set; }

        public bool expense_approved { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
