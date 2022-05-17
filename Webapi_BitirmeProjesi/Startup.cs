using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.JWT;
using Webapi_BitirmeProjesi.Middlewares;
using Webapi_BitirmeProjesi.Services;

namespace Webapi_BitirmeProjesi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

     
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddXmlSerializerFormatters();

            services.AddDbContext<EventSystemDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EventSystemDb"));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<ITokenHelper,JwtHelper>();

            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            services.AddSingleton<ILoggerService, FileLogger>();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOption>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = tokenOptions.Audience,
                    ValidateAudience = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Webapi_BitirmeProjesi", Version = "v1" });
            });
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Webapi_BitirmeProjesi v1"));
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
