using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Web;
using AngularDoodle.Server.Helpers;


namespace AngularDoodle.Server
{
    public class ServiceConfigurator
    {
        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string serverName = DatabaseServerNameProvider.GetServerName();
            string? connectionString = configuration.GetConnectionString("AdvisoryShowcase_Database");
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                DataSource = serverName
            };
            string finalConnectionString = sqlConnectionStringBuilder.ConnectionString;

            services.AddDbContext<AdvisoryShowcase_Context>(options =>
            {
                options.UseSqlServer(finalConnectionString);
            }, ServiceLifetime.Scoped);
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));

            services.AddScoped<IAuthorizationHandler, UserExistsHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("requireAuth", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
