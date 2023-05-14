using SyncLink.Server.Common;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;
using SyncLink.Server.Middleware;
using SyncLink.Server.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredDbContext(builder.Configuration);
builder.Services.AddConfiguredIdentity(builder.Configuration);
builder.Services.AddApiWithSwagger();
builder.Services.AddInfrastructure();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await DbSeeder.SeedAsync(app.Services);
}

app.UseRouting();

app.UseMiddleware<ErrorHandler>();

app.UseCors(Constants.AllowAllCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<SyncLinkHub>("hubs/general");

app.Run();