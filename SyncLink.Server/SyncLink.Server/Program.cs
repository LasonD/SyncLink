using SyncLink.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredDbContext(builder.Configuration);
builder.Services.AddConfiguredIdentity(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();