using _4EPlatform.Application.Services;
using _4EPlatform.Domain.Models;
using _4EPlatform.PermissionsGroupingChecker;
using _4EPlatform.PremessionRegister;
using _4EPlatForm.Application;
using _4EPlatform_Application.Services;
using _4EPlatform_Controlpanel_ORGHierararchy;
using _4EPlatform_Controlpanel_ORGHierararchy.Services;
using _4EPlatform_Controlpanel_UserManagement.GrpClientsConfig;
using _4EPlatform_StrategicTelecom_Thanks.Extension;
using AdminPanel.UserManagement.Feature.Account.Commands.LoginV2;
using AdminPanel.UserManagement.Mappings;
using Application.Helpers.LdapManager;
using Application.Services;
using ControlPanel.UserManagement.MiddleWares;
using Domain.Models;
using Dtos;
using Duende.IdentityServer;
using Infrastructure.GenericRepo;
using Infrastructure.GenericRepo.Context;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Net;
using System.Reflection;
using UserManagement.Helpers.Encryption;
using UserManagement.Helpers.IdentityConfiguration;
using UserManagement.Helpers.SendEmail;
using UserManagement.Resources;

var builder = WebApplication.CreateBuilder(args);
try
{
    //builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
    var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, 7152, o => o.Protocols = HttpProtocols.Http2);

        // ADDED THIS LINE to fix the problem
        options.Listen(IPAddress.Any, 5111, o => o.Protocols = HttpProtocols.Http1);

    });
    builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);
    builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
    builder.Services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();
    //builder.Services.AddHostedService<AddPermissionsMiddleware>();
    ////builder.Services.AddSingleton<PostPermessions>();
    builder.Services.AddSingleton<ITokenService, TokenService>();
    //builder.Services.AddHostedService<AddPermissionsMiddleware>();

    // Add services to the container.
    // serilog 
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    // solve the problem of cycle

    builder.Services.AddControllersWithViews()
     .AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );

    builder.Services.AddRatesLimiting(builder.Configuration);
    //builder.Services.AddMediatR(typeof(Program));

    //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    //builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("4EPlatform.Application"));
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(CoreModule).Assembly);
        //cfg.RegisterServicesFromAssemblies(typeof(InfrastructureModule).Assembly);
    });

    //builder.Services.AddAutoMapper(typeof(Program), typeof(CoreModule), typeof(InfrastructureModule));
    builder.Services.AddAutoMapper(typeof(Program), typeof(CoreModule));
    builder.Services.AddControllers().AddOData(options =>
                options.Select().Filter().OrderBy().Expand());
    builder.Services.AddHttpContextAccessor();    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddTransient<IAESConfiguration, AESConfiguration>();
    builder.Services.AddSingleton<IAESConfiguration>(builder.Configuration.GetSection("EncryptionConfiguration").Get<AESConfiguration>());
    builder.Services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
    builder.Services.AddScoped(typeof(ILdapManager), typeof(LdapManager));
    builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    builder.Services.AddSingleton<IEmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
    builder.Services.AddScoped<IEmailService, EmailService>();
    #region Localization
    builder.Services.AddSingleton<LocService>();
    #endregion
    builder.Services.AddIdentity<User, Role>()
    .AddUserStore<UserStore<User, Role, DBContext, Guid, UserClaim, UserRole, UserHistory, UserToken, RoleClaim>>()
    .AddRoleStore<RoleStore<Role, DBContext, Guid, UserRole, RoleClaim>>()
    .AddTokenProvider(TokenOptions.DefaultProvider, typeof(DataProtectorTokenProvider<User>));

    //Identity Server
    builder.Services.AddIdentityServer(options =>
    {
        options.IssuerUri = builder.Configuration.GetValue<string>("IdentityUrl");
    })
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    // use identity user in database 
    .AddAspNetIdentity<User>()
    .AddDeveloperSigningCredential().AddExtensionGrantValidator<UsernameOnlyGrantValidator>();

    builder.Services.AddApiVersioning(config =>
    {
        // Specify the default API Version as 1.0
        config.DefaultApiVersion = new ApiVersion(1, 0);
        // If the client hasn't specified the API version in the request, use the default API version number 
        config.AssumeDefaultVersionWhenUnspecified = true;
        // Advertise the API versions supported for the particular endpoint
        config.ReportApiVersions = true;
    });
    #region Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "User Management APIs Reference",
        });

        c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
            );
        c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
        }
            );
    });
    #endregion
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(10);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
    });


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            });
    });

    //builder.Services.AddAuthentication(
    //    options =>
    //{
    //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //})
    //.AddJwtBearer(options =>
    //{
    //    options.RequireHttpsMetadata = false;
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:key"])),
    //        ValidateIssuer = true,
    //        ValidIssuer = builder.Configuration["Token:Issuer"],
    //        ValidateAudience = true,
    //        ValidAudience = builder.Configuration["Token:Audience"],
    //        ValidateLifetime = true,
    //        ClockSkew = TimeSpan.Zero

    //    };
    //    options.Events = new JwtBearerEvents
    //    {
    //        OnMessageReceived = context =>
    //        {
    //            var accessToken = context.Request.Headers["Authorization"];

    //            var path = context.HttpContext.Request.Path;

    //            return Task.CompletedTask;
    //        }
    //    };
    //}
    //);
    //builder.Services.AddAuthentication().AddLocalApi();
    builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        // the name of your api resources   
        options.Audience = "UserManagementServer";

        options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
        options.RequireHttpsMetadata = false; // Set to true in production for HTTPS
        // Other options...
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "4EMicroServices");
        });

        options.AddPolicy("ClientCredentialsPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");

            //policy.AddAuthenticationSchemes("ClientCredentials"); // Use your custom scheme name here
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "CPAggregatorAPI");
        });
    });

    builder.Services.AddDbContext<DBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

        options.EnableSensitiveDataLogging(true);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    //builder.Services.AddScoped<AuthorizedPermissions>();
    //builder.Services.AddAuthorization();

    //    builder.Services.AddDbContext<DBContext>(options =>
    //    {
    //        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
    //        {
    //            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings"));
    //        }
    //        else
    //        {
    //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //        }
    //        options.EnableSensitiveDataLogging(true);
    //        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //    });

    builder.Services.AddApiVersioning(config =>
    {
        // Specify the default API Version as 1.0
        config.DefaultApiVersion = new ApiVersion(1, 0);
        // If the client hasn't specified the API version in the request, use the default API version number
        config.AssumeDefaultVersionWhenUnspecified = true;
        // Advertise the API versions supported for the particular endpoint
        config.ReportApiVersions = true;
    });
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    builder.Services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        // .AddDataAnnotationsLocalization();
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                return factory.Create("SharedResource", assemblyName.Name);
            };
        });

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("es")
                };

        options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "ar");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    //     builder.Services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //    }).AddJwtBearer(options =>
    //    {
    //        options.Authority = identityUrl;
    //        options.RequireHttpsMetadata = false;
    //        options.Audience = "basicdata";
    //    });
    //     builder.Services.AddAuthorization(options =>
    //     {
    //         options.AddPolicy("ApiScope", policy =>
    //         {
    //             policy.RequireAuthenticatedUser();
    //             policy.RequireClaim("scope", "basicdata");
    //         });
    //     });
    // Log.Logger = new LoggerConfiguration()
    //     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    //     {
    //         AutoRegisterTemplate = true,
    //         AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
    //     }).CreateLogger();
    //builder.Host.UseSerilog();





    var app = builder.Build();


    app.MapGrpcService<UserServices>();
    app.MapGrpcService<OrgUserServices>();
    app.MapGrpcService<QuickAccessServices>();
    app.MapGrpcService<PlatformSettingsService>();
    app.MapGrpcService<RoleWorkspaceServices>();
    app.MapGrpcService<OrgSettingService>();
    app.MapGrpcService<UserPermissions>();
    app.MapGrpcService<WorkspacePermissionServices>();
    app.MapGrpcService<GroupService>();
    app.MapGrpcService<WorkspacePermissionS>();
    app.MapGrpcService<UserDelegationsService>();
    app.MapGrpcService<UserPermissionGrouping>();
    app.MapGrpcService<DelegationUserServices>();
    app.MapGrpcService<SSOSecuritySettingService>();
    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
    IdentityModelEventSource.ShowPII = true;

    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
        var serviceProvider = serviceScope.ServiceProvider;
        if (!serviceScope.ServiceProvider.GetService<DBContext>().AllMigrationsApplied())
        {
            serviceScope.ServiceProvider.GetService<DBContext>().Migrate();
        }
        ContextSeed.Seed(dbContext, serviceProvider);
    }

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
    });
    
    app.UseCors("AllowCors");
    //app.UseCors();
    
    app.UseApiVersioning();
    app.MapControllers();
    //app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles();
    //string uploadFilesPath = builder.Configuration.GetSection("UploadFile").Value;
    //var provider = new FileExtensionContentTypeProvider();
    //provider.Mappings[".vtt"] = "text/vtt";

    //app.UseStaticFiles(new StaticFileOptions()
    //{
    //    FileProvider = new PhysicalFileProvider(uploadFilesPath),
    //    RequestPath = new PathString("/rega-files"),
    //    ContentTypeProvider = provider
    //});

    app.UseRouting();
    
    app.UseIdentityServer();
    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandler>();
    app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
    
    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    app.MapGrpcReflectionService();
    app.UseRateLimiter();
    //app.UseSerilogRequestLogging(options =>
    //{
    //    options.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
    //    {
    //        diagnosticContext.Set("ClientContext", "");
    //    };

    //});
    app.Run();
}
catch (Exception ex) when (

// https://github.com/dotnet/runtime/issues/60600
ex.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && ex.GetType().Name is not "HostAbortedException")
{
    Console.WriteLine(ex.Message);
    Log.Fatal(ex, "Unhandled exception");
}
