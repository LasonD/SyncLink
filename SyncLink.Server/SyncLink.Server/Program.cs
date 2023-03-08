using SyncLink.Application.Contracts.Data;
using SyncLink.Application.UseCases.Register;
using SyncLink.Infrastructure.Data.Repositories;
using SyncLink.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredDbContext(builder.Configuration);
builder.Services.AddConfiguredIdentity(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
builder.Services.AddTransient<IAuthRepository, AuthRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();