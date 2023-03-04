using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

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