using _4EPlatform_Domain.Models.Lookup;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.GenericRepo.Context
{
    public class ContextSeed
    {
        private static DBContext _dbContext;
        private static IServiceProvider _serviceProvider;
        private readonly UserManager<User> _userManager;

        //private static KafkaProducer _producer = new KafkaProducer("kafka:9200");

        public ContextSeed(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public static void Seed(DBContext appDbContext, IServiceProvider serviceProvider)
        {
            _dbContext = appDbContext;
            _dbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;

            //// call functions
            //SeedLookups();
            SeedLK_LanguageForm();
            SeedRoleCategory();
            SeedRole();
            SeedUser();
            SeedLK_HomeSetting();
            SeedDomain();
            SeedJobRole();
            SeedJobDegree();
            SeedJobName();
            // SeedWorkSpace();
            //SeedHomePage();
            //SeedQuickAccess();
            //SeedRole();
            SeedPermission();
        }
        //private static void SeedOrgHierarchy()
        //{
        //	if (!_dbContext.OrgHierarchies.Any())
        //	{
        //		string OrgHierarchiesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "OrgHierarchy.json");
        //		var OrgHierarchies = JsonConvert.DeserializeObject<List<OrgHierarchy>>(OrgHierarchiesAsJson);

        //		if (OrgHierarchies != null)
        //		{
        //			foreach (var OrgHierarchy in OrgHierarchies)
        //			{
        //				_dbContext.OrgHierarchies.Add(OrgHierarchy);
        //				_dbContext.SaveChanges();

        //				//if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        //				//{
        //				//	var producerModel = new KafkaModel()
        //				//	{
        //				//		KafkaEvent = KafkaEvent.Post,
        //				//		Entity = OrgHierarchy
        //				//	};
        //				//	_producer.StartSendingMessages("OrgHierarchyTopic", System.Text.Json.JsonSerializer.Serialize(producerModel));
        //				//}
        //			}
        //		}
        //	}
        //}
        //	private static void SeedWorkSpace()
        //	{
        //		if (!_dbContext.Workspaces.Any())
        //		{

        //			string ToolsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "workspaceSeed.json");
        //			List<Workspace> tools = JsonConvert.DeserializeObject<List<Workspace>>(ToolsAsJson);

        //			_dbContext.Database.OpenConnection();
        //			try
        //			{
        //				//	_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Workspaces ON");
        //				_dbContext.Workspaces.AddRange(tools);
        //				_dbContext.SaveChanges();
        //				//	_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Workspaces OFF");

        //			}

        //			catch (Exception ex) when (
        //// https://github.com/dotnet/runtime/issues/60600
        //ex.GetType().Name is not "StopTheHostException"
        //// HostAbortedException was added in .NET 7, but since we target .NET 6 we
        //// need to do it this way until we target .NET 8
        //&& ex.GetType().Name is not "HostAbortedException")
        //			{
        //				Log.Fatal(ex, "Unhandled exception");
        //			}

        //		}
        //	}
        private static void SeedQuickAccess()
        {
            if (!_dbContext.QuickAccess.Any())
            {

                string QuickAccessAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "QuickAccessSeed.json");
                List<QuickAccess> quickAccesses = JsonConvert.DeserializeObject<List<QuickAccess>>(QuickAccessAsJson);

                _dbContext.Database.OpenConnection();
                try
                {
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.QuickAccess ON");
                    _dbContext.QuickAccess.AddRange(quickAccesses);
                    _dbContext.SaveChanges();
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.QuickAccess OFF");
                }
                catch (Exception ex) when (
    // https://github.com/dotnet/runtime/issues/60600
    ex.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && ex.GetType().Name is not "HostAbortedException")
                {
                    Log.Fatal(ex, "Unhandled exception");
                }


            }
        }

        private static void SeedRole()
        {
            if (!_dbContext.Roles.Any())
            {

                string rolesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "RoleSeed.json");
                List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(rolesAsJson);
                try
                {
                    foreach (var role in roles)
                    {
                        role.CreatedOn = DateTime.Now;

                        _dbContext.Database.OpenConnection();

                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");
                        _dbContext.Roles.Add(role);
                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");
                    }
                    _dbContext.SaveChanges();
                }
                catch (Exception ex) when (
                    // https://github.com/dotnet/runtime/issues/60600
                    ex.GetType().Name is not "StopTheHostException"
                    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
                    // need to do it this way until we target .NET 8
                    && ex.GetType().Name is not "HostAbortedException")
                {
                    Log.Fatal(ex, "Unhandled exception");
                }

            }
            else
            {
                string rolesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "RoleSeed.json");
                List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(rolesAsJson);
                var permessions = _dbContext.RolePermissionsGrouping.ToList();

                var ids = _dbContext.Roles.Select(x => x.Id).ToList();
                var listRoles = roles.Where(n => !ids.Contains(n.Id)).ToList();
                _dbContext.Roles.AddRange(listRoles);
                roles.ForEach(x =>
                {
                    if (x.RolePermissionsGroupings != null)
                    {
                        x.RolePermissionsGroupings.ToList().ForEach(n =>
                        {
                            var per = permessions.FirstOrDefault(p => p.RoleId == x.Id && p.WorkspaceKey == n.WorkspaceKey);
                            if (per != null)
                            {
                                per.VerbType = per.VerbType == Domain.Enums.VerbPermissionEnum.NoPermession ? Domain.Enums.VerbPermissionEnum.Write : per.VerbType;
                                _dbContext.RolePermissionsGrouping.Update(per);
                            }
                            else
                            {
                                n.RoleId = x.Id;
                                _dbContext.RolePermissionsGrouping.Add(n);
                            }
                        });
                    }
                });
                _dbContext.SaveChanges();
            }
            //else
            //{
            //    string rolesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "RoleSeed.json");
            //    List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(rolesAsJson);

            //    roles.ForEach(n =>
            //    {
            //        n.RolePermissions?.ToList().ForEach(p =>
            //        {
            //            if (_dbContext.Permissions.Any(d => d.WorkspaceKey == p.Permission.WorkspaceKey && d.RolePermissions.Any(g=>g.RoleId == n.Id)))
            //            {

            //                n.RolePermissions.Remove(p);
            //            }
            //        });
            //        n.RolePermissionsGroupings?.ToList().ForEach(p =>
            //        {
            //            if (_dbContext.RolePermissionsGrouping.Any(d => d.WorkspaceKey == p.WorkspaceKey && d.RoleId == n.Id))
            //            {
            //                n.RolePermissionsGroupings.Remove(p);
            //            }
            //        });
            //        if(n.RolePermissions?.Count == 0)
            //        {
            //            n.RolePermissions = null;
            //        }
            //        if (n.RolePermissionsGroupings?.Count == 0)
            //        {
            //            n.RolePermissionsGroupings = null;
            //        }
            //        n.UpdatedOn = DateTime.Now;
            //        //_dbContext.Database.OpenConnection();
            //        n.RowVersion = _dbContext.Roles.FirstOrDefault(r => r.Id == n.Id).RowVersion;
            //    });

            //    _dbContext.Roles.UpdateRange(roles);
            //    _dbContext.SaveChanges();
            //}
        }


        private static void SeedUser()
        {
            if (!_dbContext.Users.Any())
            {
                string usersAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "UserSeed.json");
                var users = JsonConvert.DeserializeObject<List<User>>(usersAsJson);

                if (users != null)
                {

                    _dbContext.Database.OpenConnection();
                    foreach (var user in users)
                    {
                        // user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("123");
                        //        if(user.UserName == "SystemAdmin")
                        //        {
                        //            user.UserRoles.ToList().ForEach(n =>
                        //            {
                        //            var per = n.Role.RolePermissions.ToList();
                        //            n.Role = null;
                        //            per.ForEach(p =>
                        //            {
                        //                p.RoleId = n.RoleId;
                        //            });
                        //            _dbContext.RolePermissions.AddRange(per);
                        //            });
                        //}
                        user.AllowExternalRequest = true;
                        _dbContext.Users.Add(user);
                        //if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
                        //{
                        //	var producerModel = new KafkaModel()
                        //	{
                        //		KafkaEvent = KafkaEvent.Post,
                        //		Entity = user
                        //	};
                        //	_producer.StartSendingMessages("UserTopic", System.Text.Json.JsonSerializer.Serialize(producerModel));
                        //}
                    }
                    _dbContext.SaveChanges();
                    _dbContext.Database.CloseConnection();
                }
            }
            //else
            //{
            //    string usersAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "UserSeed.json");
            //    var users = JsonConvert.DeserializeObject<List<User>>(usersAsJson);
            //    var listUsers = new List<User>();
            //    var data = _dbContext.Users.ToList();

            //    var seedData = data.Where(n => n.FromSeed == true).ToList();
            //    seedData.ForEach(n =>
            //    {
            //        if(!users.Any(x=>x.Id == n.Id))
            //        {
            //            n.State = Domain.Enums.State.Deleted;

            //            _dbContext.Users.Update(n);
            //        }
            //    });

            //    users.ForEach(n =>
            //    {
            //        if (data.Any(s => s.Id == n.Id))
            //        {
            //            n.AllowExternalRequest = true;
            //            n.ThirdName = n.ThirdName == null || n.ThirdName == "null" ? "" : n.ThirdName;
            //            n.ForthName = n.ForthName == null || n.ForthName == "null" ? "" : n.ForthName;
            //            n.LanguageForm = null;
            //            n.UserRoles = null;
            //            n.RowVersion = data.FirstOrDefault(s => s.Id == n.Id).RowVersion;
            //            n.FromSeed = true;
            //        }
            //        else
            //        {
            //            n.AllowExternalRequest = true;
            //            listUsers.Add(n);
            //        }
            //    });

            //    users.RemoveAll(x => listUsers.Any(c => c.Id == x.Id));

            //    _dbContext.Users.AddRange(listUsers);
            //    _dbContext.Users.UpdateRange(users);
            //    _dbContext.SaveChanges();

            //}
           
        }
        private static void SeedLK_HomeSetting()
        {
            if (!_dbContext.LK_HomeSetting.Any())
            {
                string LK_HomeSettingAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LK_HomeSettingSeed.json");
                var LK_HomeSettings = JsonConvert.DeserializeObject<List<LK_HomeSetting>>(LK_HomeSettingAsJson);

                if (LK_HomeSettings != null)
                {

                    _dbContext.Database.OpenConnection();
                    var list = new List<LK_HomeSetting>();
                    foreach (var LK_HomeSetting in LK_HomeSettings)
                    {
                        LK_HomeSetting.CreatedOn = DateTime.Now;
                        list.Add(LK_HomeSetting);


                    }
                    _dbContext.LK_HomeSetting.AddRange(list);
                    _dbContext.SaveChanges();
                    _dbContext.Database.CloseConnection();
                }
            }

        }
        private static void SeedLK_LanguageForm()
        {
            if (!_dbContext.LK_LanguageForm.Any())
            {
                string LK_HomeSettingAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LK_LanguageFormSeed.json");
                var LK_LanguageForms = JsonConvert.DeserializeObject<List<LK_LanguageForm>>(LK_HomeSettingAsJson);

                if (LK_LanguageForms != null)
                {

                    _dbContext.Database.OpenConnection();
                    var list = new List<LK_LanguageForm>();
                    foreach (var LK_LanguageForm in LK_LanguageForms)
                    {
                        LK_LanguageForm.CreatedOn = DateTime.Now;
                        list.Add(LK_LanguageForm);

                    }
                    _dbContext.LK_LanguageForm.AddRange(list);
                    _dbContext.SaveChanges();
                    _dbContext.Database.CloseConnection();
                }
            }

        }
        private static void SeedHomePage()
        {
            if (!_dbContext.HomePages.Any())
            {
                string homePagesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "HomePageSeed.json");
                List<HomePage> homePages = JsonConvert.DeserializeObject<List<HomePage>>(homePagesAsJson);
                _dbContext.Database.OpenConnection();
                try
                {
                    foreach (var homePage in homePages)
                    {
                        _dbContext.HomePages.Add(homePage);
                    }
                    _dbContext.SaveChanges();
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }
        }

        private static void SeedPermission()
        {
            if (!_dbContext.Permissions.Any())
            {
                string permissionJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "PermissionsSeed.json");
                List<Permission> permissions = JsonConvert.DeserializeObject<List<Permission>>(permissionJson);
                _dbContext.Database.OpenConnection();
                try
                {
                    foreach (var permission in permissions)
                    {
                        _dbContext.Permissions.Add(permission);
                    }
                    _dbContext.SaveChanges();
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }
            else
            {
                string permissionJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "PermissionsSeed.json");
                List<Permission> permissions = JsonConvert.DeserializeObject<List<Permission>>(permissionJson);
                var perm = _dbContext.Permissions.ToList();
                var per = permissions.Where(n => !perm.Any(x => x.Id == n.Id)).ToList();
                if (per.Any())
                {
                    _dbContext.Permissions.AddRange(per);
                    _dbContext.SaveChanges();
                }

            }
        }
        private static void SeedRoleCategory()
        {
            if (!_dbContext.LK_RoleCategory.Any())
            {

                string rolesCategoryAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "RoleCategorySeed.json");
                List<LK_RoleCategory> rolesCategory = JsonConvert.DeserializeObject<List<LK_RoleCategory>>(rolesCategoryAsJson);
                try
                {
                    _dbContext.Database.OpenConnection();
                    foreach (var role in rolesCategory)
                    {
                        role.CreatedOn = DateTime.Now;

                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");
                        _dbContext.LK_RoleCategory.Add(role);
                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");
                    }
                    _dbContext.SaveChanges();
                    _dbContext.Database.CloseConnection();
                }
                catch (Exception ex) when (
                    // https://github.com/dotnet/runtime/issues/60600
                    ex.GetType().Name is not "StopTheHostException"
                    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
                    // need to do it this way until we target .NET 8
                    && ex.GetType().Name is not "HostAbortedException")
                {
                    Log.Fatal(ex, "Unhandled exception");
                }

            }
        }


        private static void SeedDomain()
        {
            if (!_dbContext.Domains.Any())
            {

                string domainsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "DomainSeed.json");
                List<Domain.Models.Domains> domains = JsonConvert.DeserializeObject<List<Domain.Models.Domains>>(domainsAsJson);
                try
                {
                    _dbContext.Database.OpenConnection();
                    foreach (var domain in domains)
                    {
                        domain.CreatedOn = DateTime.Now;

                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");
                        _dbContext.Domains.Add(domain);
                        //_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");
                    }
                    _dbContext.SaveChanges();
                }
                catch (Exception ex) when (
                    // https://github.com/dotnet/runtime/issues/60600
                    ex.GetType().Name is not "StopTheHostException"
                    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
                    // need to do it this way until we target .NET 8
                    && ex.GetType().Name is not "HostAbortedException")
                {
                    Log.Fatal(ex, "Unhandled exception");
                }

            }
        }

        private static void SeedJobDegree()
        {
            if (!_dbContext.LK_JobDegree.Any())
            {
                string json = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LookupSeed.json");
                var lookup = JsonConvert.DeserializeObject<List<LK_JobDegree>>(json);

                if (lookup != null)
                {

                    _dbContext.LK_JobDegree.AddRange(lookup);
                    _dbContext.SaveChanges();
                }
            }

        }
        private static void SeedJobRole()
        {
            if (!_dbContext.LK_JobRole.Any())
            {
                string json = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LookupJobRoleSeed.json");
                var lookup = JsonConvert.DeserializeObject<List<LK_JobRole>>(json);

                if (lookup != null)
                {

                    _dbContext.LK_JobRole.AddRange(lookup);
                    _dbContext.SaveChanges();
                }
            }

        }
        private static void SeedJobName()
        {
            if (!_dbContext.LK_JobName.Any())
            {
                string json = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LookupJobNameSeed.json");
                var lookup = JsonConvert.DeserializeObject<List<LK_JobName>>(json);

                if (lookup != null)
                {

                    _dbContext.LK_JobName.AddRange(lookup);
                    _dbContext.SaveChanges();
                }
            }

        }
        public class RoleComparer : IEqualityComparer<Role>
        {
            public bool Equals(Role x, Role y)
            {

                return x == y;
            }

            public int GetHashCode(Role obj)
            {

                return HashCode.Combine(obj);
            }
        }
    }

}