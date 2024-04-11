using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using BaseProject.API.Middleware;
using BaseProject.API.Utils.Database;
using BaseProject.API.Utils.OpenApi;
using BaseProject.Domain.Shares;
using BaseProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(ConfigureSwaggerOptions.SetupAction);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services.AddProblemDetails();

var app = builder.Build();

// Init Database Record
using (var scope = app.Services.CreateScope())
{
    var servicesDb = scope.ServiceProvider;
    try
    {
        var context = servicesDb.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch
    {
        // ignored
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();