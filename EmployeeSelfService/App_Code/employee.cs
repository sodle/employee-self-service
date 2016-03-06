namespace EmployeeSelfService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("capstone-db.employee")]
    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            certifications = new HashSet<certification>();
            expense_report = new HashSet<expense_report>();
            pto_request = new HashSet<pto_request>();
            skills = new HashSet<skill>();
            time_report = new HashSet<time_report>();
            users = new HashSet<user>();
        }

        [Key]
        public int employee_key { get; set; }

        [Required]
        [StringLength(45)]
        public string first_name { get; set; }

        [Required]
        [StringLength(45)]
        public string last_name { get; set; }

        [Required]
        [StringLength(45)]
        public string address_street1 { get; set; }

        [StringLength(45)]
        public string address_street2 { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string address_state { get; set; }

        [Required]
        [StringLength(45)]
        public string address_city { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(5)]
        public string address_zip { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(10)]
        public string phone { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime create_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<certification> certifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expense_report> expense_report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pto_request> pto_request { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skill> skills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<time_report> time_report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> users { get; set; }
    }
}
