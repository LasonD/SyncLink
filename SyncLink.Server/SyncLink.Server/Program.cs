using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using SyncLink.Data.Context;
using SyncLink.Data.Models.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SyncLinkDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SyncLinkDbContextConnection' not found.");

builder.Services.AddDbContext<SyncLinkDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SyncLinkDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/random", () =>
{
    return Enumerable.Range(0, 1000000)
        .Select(_ => new Random().Next(0, 1000))
        .GroupBy(x => x)
        .OrderByDescending(g => g.Count())
        .Select(JsonConvert.SerializeObject)
        .ToList();

}).WithOpenApi();

app.Run();