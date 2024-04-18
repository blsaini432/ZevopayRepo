using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zevopay.Contracts;
using Zevopay.Data;
using Zevopay.Data.Entity;
using Zevopay.Data.PrimeBuilderAdmin.Context;
using Zevopay.Services;

namespace Zevopay.App_Start
{
    public static class ServiesClass
    {
        public static void RegisterServices(this IServiceCollection services,WebApplicationBuilder builder)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ZevopayDb")));
            builder.Services.AddScoped<IDapperDbContext, DapperDbContext>();
            //services.AddHttpContextAccessor();
            services.AddScoped<ICommonService,CommonService>();
            services.AddScoped<IApiService,ApiService>();
            services.AddScoped<IAccountService,AccountService>();
            services.AddScoped<ISubAdminService,SubAdminService>();
            services.AddScoped<IAdminService,AdminService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ITwoFactorAuthService,TwoFactorAuthService>();

            services.AddIdentity<IPayoutsService, PayoutsService>()
            services.AddIdentity<ApplicationUser, ApplicationRole>()
             .AddEntityFrameworkStores<DataContext>()
             .AddDefaultTokenProviders();
            services.AddMvc();
        }
    }
}
