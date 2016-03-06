namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.user")]
    public partial class user
    {
        [Key]
        public int user_key { get; set; }

        [Required]
        [StringLength(45)]
        public string user_name { get; set; }

        [Required]
        [StringLength(45)]
        public string password_hash { get; set; }

        public int employee_key { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        public virtual employee employee { get; set; }
    }
}
