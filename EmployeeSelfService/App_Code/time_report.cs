namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.time_report")]
    public partial class time_report
    {
        [Key]
        public int time_report_id { get; set; }

        public int employee_key { get; set; }

        [Column(TypeName = "date")]
        public DateTime time_report_date { get; set; }

        public decimal time_report_num_hours { get; set; }

        public bool time_report_billable { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
