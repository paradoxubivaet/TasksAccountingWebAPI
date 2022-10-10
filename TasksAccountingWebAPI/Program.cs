using Microsoft.Extensions.Options;
using TasksAccountingWebAPI.DAL.Repository;
using TasksAccountingWebAPI.DAL.Settings;
using TasksAccountingWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<SqlSettings>(builder.Configuration.GetSection("SqlConnectionStrings"));

builder.Services.AddSingleton<ISqlSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<SqlSettings>>().Value);
builder.Services.AddHostedService<ObserverService>();

builder.Services.AddScoped(typeof(IRepository), typeof(Repository));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
