﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTRS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dashReportingEntities : DbContext
    {
        public dashReportingEntities()
            : base("name=dashReportingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CandidateTimeLine> CandidateTimeLines { get; set; }
        public virtual DbSet<CommentDetail> CommentDetails { get; set; }
        public virtual DbSet<JobPortalMaster> JobPortalMasters { get; set; }
        public virtual DbSet<LocationMaster> LocationMasters { get; set; }
        public virtual DbSet<RecurringMaster> RecurringMasters { get; set; }
        public virtual DbSet<RecurringType> RecurringTypes { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<SalesServiceMaster> SalesServiceMasters { get; set; }
        public virtual DbSet<TechnologyMaster> TechnologyMasters { get; set; }
        public virtual DbSet<UserAccountDetail> UserAccountDetails { get; set; }
        public virtual DbSet<VisaTitleMaster> VisaTitleMasters { get; set; }
        public virtual DbSet<CandidateMaster> CandidateMasters { get; set; }
        public virtual DbSet<TeamDetail> TeamDetails { get; set; }
        public virtual DbSet<CandidateMarketingDetail> CandidateMarketingDetails { get; set; }
        public virtual DbSet<CandidateAssign> CandidateAssigns { get; set; }
        public virtual DbSet<SubmissionDetail> SubmissionDetails { get; set; }
        public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }
    }
}
