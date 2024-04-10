using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseProject.API.OpenApi;

public static class ConfigureSwaggerOptions
{
    public static void SetupAction(SwaggerGenOptions options)
    {
        //options.OperationFilter<CustomHeaderSwagger>();

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Base Project API",
            Description = "Web API for Base Project",
            Contact = new OpenApiContact
            {
                Name = "Quang",
                Email = "nguyenhnhatquang@gmail.com",
            },
            License = new OpenApiLicense
            {
                Name = "License By Base Project",
            }
        });
    }
}