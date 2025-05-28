using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Infra.Behaviors;
using Rommanel.Test.API.Infra.EntityFramework;
using Rommanel.Test.API.Infra.Middlewares;
using Rommanel.Test.API.Queries.Customer;
using Rommanel.Test.API.Queries.Interfaces;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("db");

builder.Services.AddDbContext<RommanelContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICustomerQuery, CustomerQuery>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//rate limit
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    })
    .AddSlidingWindowLimiter("sliding", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.SegmentsPerWindow = 5;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{

    options.AddPolicy("Policy",
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("Policy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.MapHealthChecks("/api/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseErrorMiddleware();

app.Run();
