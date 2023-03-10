using SyncLink.Server.Helpers;
using SyncLink.Server.Middleware;

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
}

app.UseMiddleware<ErrorHandler>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();