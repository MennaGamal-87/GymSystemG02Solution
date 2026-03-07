using GymSystemG02BLL;
using GymSystemG02BLL.Services.AttachmentService;
using GymSystemG02BLL.Services.Classes;
using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02DAL.Data.Contexts;
using GymSystemG02DAL.Data.DataSeed;
using GymSystemG02DAL.Entities;
using GymSystemG02DAL.Repositroies.Classes;
using GymSystemG02DAL.Repositroies.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymSystemG02PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Dependency injection
            builder.Services.AddDbContext<GymSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped(typeof(IMembershipRepository), typeof(MembershipRepository));
            builder.Services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));

            builder.Services.AddAutoMapper(X=>X.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IPlanService,PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config =>
            {
                Config.Password.RequiredLength = 6;
                Config.Password.RequireUppercase = true;
                Config.Password.RequireLowercase = true;
                Config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymSystemDbContext>();
                
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            var app = builder.Build();

            #region Data Seed
            var Scope = app.Services.CreateScope();
            var dbContext = Scope.ServiceProvider.GetRequiredService<GymSystemDbContext>();
            //Must Check all migration is applied
            var PendingMigration = dbContext.Database.GetPendingMigrations();
            //if have pending migration apply if not return false
            if (PendingMigration?.Any() ?? false)
            {
                dbContext.Database.Migrate();
            }
            GymDbContextSeeding.SeedData(dbContext);

            //SeedDate
            var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            IdentitydbContextSeeding.SeedData(RoleManger, UserManger);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
