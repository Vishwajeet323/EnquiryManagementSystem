using Enquiry.Domain.Entities.EnquiryManagment;
using Enquiry.Domain.Entities.Login;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Context
{
    public class EnquiryDbContext:DbContext
    {
        public EnquiryDbContext(DbContextOptions<EnquiryDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEnquiry>()
            .HasOne(e => e.User_CreatedBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
            .HasOne(e => e.StudentEnquiry_EnquiryId)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdmissionInstallment>()
            .HasOne(e => e.User_Receiver)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowUp>()
            .HasOne(e => e.User_FollowUpBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                    .HasOne(e => e.User_AdmissionBy)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            var Roles = new List<RoleMaster>()
            {
                new RoleMaster
                {
                    RoleId= 1,
                    Role="Admin",
                    CreatedDate=DateTime.UtcNow,
                    IsDeleted=false

                }
            };
            modelBuilder.Entity<RoleMaster>().HasData(Roles);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<AdmissionInstallment> AdmissionInstallments { get; set; }
        public DbSet<CourseMaster> CourseMasters { get; set; }
        public DbSet<StudentEnquiry> StudentEnquiries { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<ReferenceMaster> ReferenceMasters { get; set; }

        
    }
}
