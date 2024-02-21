using Buffer.Service.Handlers;
using MassTransit;
using Monq.Core.ClickHouseBuffer.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCHBuffer(builder.Configuration.GetSection("BufferEngineOptions"), "Host=clickhouse;Port=8123;Username=someuser;Password=strongPasw;Database=logging_service;");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LogsConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("logs", e =>
        {
            e.ConfigureConsumer<LogsConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
