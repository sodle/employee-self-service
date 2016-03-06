namespace EmployeeSelfService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ESSDatabase : DbContext
    {
        public ESSDatabase()
            : base("name=EmployeeSelfService")
        {
        }

        public virtual DbSet<certification> certifications { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<expense_report> expense_report { get; set; }
        public virtual DbSet<pto_request> pto_request { get; set; }
        public virtual DbSet<skill> skills { get; set; }
        public virtual DbSet<time_report> time_report { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<certification>()
                .Property(e => e.cert_text)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.address_street1)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.address_street2)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.address_state)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.address_city)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.address_zip)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.certifications)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.expense_report)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.pto_request)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.skills)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.time_report)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.users)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<expense_report>()
                .Property(e => e.expense_title)
                .IsUnicode(false);

            modelBuilder.Entity<expense_report>()
                .Property(e => e.expense_description)
                .IsUnicode(false);

            modelBuilder.Entity<pto_request>()
                .Property(e => e.request_reason)
                .IsUnicode(false);

            modelBuilder.Entity<skill>()
                .Property(e => e.skill_text)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password_hash)
                .IsUnicode(false);
        }
    }
}
