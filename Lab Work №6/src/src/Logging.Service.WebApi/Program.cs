using Logging.Server.Service.StreamData.Configuration;
using Logging.Server.Service.StreamData.HttpServices;
using Logging.Server.Service.StreamData.Services.Implementation;
using Logging.Server.StreamData.Validator.Services;
using Logging.Server.StreamData.Validator.Services.Implementation;
using Logging.Service.WebApi.Services.Implementation;
using Logging.Service.WebApi.Services.Implementation.Decorator;
using Logging.Service.WebApi.Services.Implementation.Loggers;
using Logging.Service.WebApi.Services.Implementation.LoggerStates;
using Logging.Service.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.BasicDotNetMicroservice.GlobalExceptionFilters.Filters;
using Monq.Core.HttpClientExtensions.Exceptions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    }));
var constr = configuration.GetValue<string>(AppConstants.Configuration.ClickHouseConnectionString);
builder.Services.AddGlobalExceptionFilter()
                .AddExceptionHandler<ResponseException>(ex =>
                    new ObjectResult(JsonConvert.DeserializeObject(ex.ResponseData)) { StatusCode = (int)ex.StatusCode })
                .AddDefaultExceptionHandlers();

builder.Services.Configure<ClickHouseOptions>(options => options.ConnectionString = configuration.GetValue<string>(AppConstants.Configuration.ClickHouseConnectionString));

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IStreamDataSchemasRepository, StreamDataSchemasRepository>();
builder.Services.AddScoped<IStreamDataRepository, StreamDataRepositoryImpl>();
builder.Services.AddSingleton<IMqlQueryParserBuilder, MqlQueryParserBuilder>();
builder.Services.AddScoped<IStreamsHttpService, StreamsHttpServiceImpl>();

builder.Services
    .AddControllers(opt => opt.Filters.Add(typeof(GlobalExceptionFilter)))
    .AddNewtonsoftJson(opt => opt.UseCamelCasing(true));

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        LoggingFacade loggingFacade = new LoggingFacade();
        loggingFacade.Log($"Body: {context.Request.Method} {context.Request.Path}", LogLevel.Information);

        Logging.Service.WebApi.Services.Interfaces.ILogger logger = new Logger();
        var timestampLogger = new LogLevelFilterDecorator(logger, LogLevel.Error);
        var conditionalLogger = new ConditionalLoggerDecorator(logger, LogLevel.Debug);

        timestampLogger.Log($"Request: {context.Request.Method} {context.Request.Path}", LogLevel.Error);
        conditionalLogger.Log($"Request: {context.Request.Body} {context.Request.Path}", LogLevel.Debug);
    }
});

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Создаем экземпляр контекста логгера
        LoggerContext loggerContext = new LoggerContext();

        // Логируем сообщения в разных состояниях
        loggerContext.Log($"Request: {context.Request.Method} {context.Request.Path}");

        // Переключаем состояние на "выключено"
        loggerContext.TransitionTo(new LoggingDisabledState());
        loggerContext.Log($"Request: {context.Request.Method} {context.Request.Path}");
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.Run();
