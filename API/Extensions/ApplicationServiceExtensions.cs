using Application.Contracts.Infrastructure;
using Application.Interfaces;
using Application.Models;
using Infrastructure.Mail;
using Infrastructure.Photos;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddControllers(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsAdmin", policy =>
                {
                    policy.Requirements.Add(new IsAdminRequirement());
                });
            });

            services.AddScoped<IAuthorizationHandler, IsAdminRequirementHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                policy.AllowAnyMethod().
                AllowCredentials().
                AllowAnyHeader().WithOrigins("http://localhost:3000"));
            });

            return services;
        }
    }
}