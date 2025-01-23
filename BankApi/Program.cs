using BankApi;
using BankApi.Service;
using BankApi.Service.IService;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
	.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
	.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3} {SourceContext} {Message:lj}{NewLine}{Exception}", restrictedToMinimumLevel: LogEventLevel.Information)
	.Enrich.FromLogContext()
	.ReadFrom.Configuration(ctx.Configuration)
);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigins", policy =>
	{
		policy.WithOrigins("http://localhost:5117")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IWithdrawService, WithdrawService>();

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
