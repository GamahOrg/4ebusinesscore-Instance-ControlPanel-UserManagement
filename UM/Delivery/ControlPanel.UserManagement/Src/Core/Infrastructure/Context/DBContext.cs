using _4EPlatform_Domain.Models.Lookup;
using _4EPlatform_Domain.Models.User;
using _4EPlatform_Domain.Models.WorkspaceEntity;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


namespace Infrastructure.GenericRepo.Context
{
    public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<FunctionalData> FunctionalData { get; set; }
		public DbSet<ScientificDataAndExperiences> ScientificDataAndExperiences { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RoleClaim> RoleClaims { get; set; }
		public DbSet<UserClaim> UserClaims { get; set; }
		public DbSet<UserToken> UserTokens { get; set; }
		public DbSet<UserHistory> UserLoginsHistory { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermissions> RolePermissions { get; set; }
		public DbSet<Domains> Domains { get; set; }
		public DbSet<QuickAccess> QuickAccess { get; set; }
		public DbSet<UserDelegation> UserDelegation { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<HomePage> HomePages { get; set; }
		public DbSet<UserSetting> UserSettings { get; set; }
		public DbSet<FavoriteWorkspace> FavoriteSubSystemSettings { get; set; }
		public DbSet<GroupLog> GroupLogs { get; set; }
		public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<LanguageForm> LanguageForm { get; set; }
        public DbSet<HomeSetting> HomeSettings { get; set; }
		public DbSet<HiddenWorkspace> HiddenSubSystemSettings { get; set; }
        public DbSet<InformationJob> InformationJob { get; set; }
        public DbSet<LK_JobDegree> LK_JobDegree { get; set; }
        public DbSet<LK_HomeSetting> LK_HomeSetting { get; set; }
        public DbSet<LK_LanguageForm> LK_LanguageForm { get; set; }
        public DbSet<LK_JobName> LK_JobName { get; set; }
        public DbSet<LK_JobRole> LK_JobRole { get; set; }
		public DbSet<LK_RoleCategory> LK_RoleCategory { get; set; }
		public DbSet<RolePermissionsGrouping> RolePermissionsGrouping { get; set; }
		public DbSet<UserTransaction> UserTransaction { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserRole>().HasKey(p => new { p.UserId, p.RoleId });

            /*            modelBuilder.Entity<GroupRole>().HasKey(p => new { p.GroupId, p.RoleId });
						modelBuilder.Entity<UserGroup>().HasKey(p => new { p.UserId, p.GroupId });
						modelBuilder.Entity<RolePermissions>().HasKey(p => new { p.RoleId, p.PermissionId });
						modelBuilder.Entity<RoleWorkSpace>().HasKey(p => new { p.RoleId, p.WorkspaceId });*/
            modelBuilder.Entity<User>().Property(p=>p.JobNameId)
                .IsRequired(false); 
			modelBuilder.Entity<User>().HasQueryFilter(p => p.AllowExternalRequest == true);
            modelBuilder.Entity<InformationJob>().Property(p=>p.JobDegreeId).IsRequired(false);
            modelBuilder.Entity<InformationJob>().Property(p=>p.JobRoleId).IsRequired(false);
            modelBuilder.Entity<Group>().Property(p=>p.ManagerId).IsRequired(false);
            modelBuilder.Entity<Role>().Property(p => p.RoleCategoryId).IsRequired(false);
            modelBuilder.Entity<Role>().Property(p => p.Code).IsRequired(false);
            modelBuilder.Entity<QuickAccess>()
            .HasIndex(qa => new { qa.WorkSpaceCardId, qa.UserId })
            .IsUnique();
            modelBuilder.Entity<User>().Property(u=>u.ManagerNameAD).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u=>u.CommonNameAD).HasMaxLength(100);

        }

        public bool AllMigrationsApplied()
		{
			var applied = this.GetService<IHistoryRepository>()
				.GetAppliedMigrations()
				.Select(m => m.MigrationId);

			var total = this.GetService<IMigrationsAssembly>()
				.Migrations
				.Select(m => m.Key);

			return !total.Except(applied).Any();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
		public void Migrate()
		{
			this.Database.Migrate();
		}

	}
}