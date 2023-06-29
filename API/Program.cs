using Persistence;
using Application;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain;
using Infrastructure;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices();
builder.Services.AddIdentityServices(builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = connection;

            command.CommandText = @"
                CREATE OR REPLACE FUNCTION calculate_distance(
                    lat1 double precision,
                    lon1 double precision,
                    lat2 double precision,
                    lon2 double precision
                )
                RETURNS double precision AS $$
                DECLARE
                    x double precision = 69.1 * (lat2 - lat1);
                    y double precision = 69.1 * (lon2 - lon1) * cos(lat1 / 57.3);
                    distance double precision = sqrt(x * x + y * y);
                BEGIN
                    RETURN distance;
                END;
                $$ LANGUAGE plpgsql;
            ";

            command.ExecuteNonQuery();
        }
    }
}

try
{
    var context = services.GetRequiredService<HakimHubDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    await context.Database.MigrateAsync();
    // await Seed.SeedData(context, );
    await Seed.SeedData(context, userManager, builder.Configuration);

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An erorr occured during migration");
}

app.Run();