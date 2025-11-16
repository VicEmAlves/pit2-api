using Microsoft.Extensions.DependencyInjection;
using Pit2Api.Infra;
using Pit2Api.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddInfraDependencyInjection()
    .AddRepositories();

builder.Services.AddOptions();
builder.Services.Configure<Config>(builder.Configuration.GetSection(nameof(Config)));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Policy",
                      policy =>
                      {
                          policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("Policy");

app.UseAuthorization();

app.MapControllers();

app.Run();
