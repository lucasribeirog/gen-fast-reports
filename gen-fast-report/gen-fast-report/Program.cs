using gen_fast_report.Data;
using gen_fast_report.Services;
using gen_fast_report.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StandardReportDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
sqlOptions => sqlOptions.EnableRetryOnFailure(
    maxRetryCount:3,
    maxRetryDelay: TimeSpan.FromSeconds(10),
    errorNumbersToAdd: null)
    )
);
builder.Services.AddScoped<IUploadReportHandler, UploadReportHandler>();
builder.Services.AddScoped<IFileValidationService, FileValidationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
