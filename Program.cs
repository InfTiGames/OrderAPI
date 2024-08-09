using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Repositories;
using OrderAPI.Services;
using OrderAPI.Models;
using OrderAPI.Utilities;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OrdersDb"));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var paymentChannel = Channel.CreateUnbounded<PaymentInfo>();
builder.Services.AddSingleton(paymentChannel);

builder.Services.AddHostedService<PaymentProcessingService>();

builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(config => {
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

IApplicationBuilder applicationBuilder = app.UseEndpoints(endpoints => {
    ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = endpoints.MapControllers();
});

app.Run();