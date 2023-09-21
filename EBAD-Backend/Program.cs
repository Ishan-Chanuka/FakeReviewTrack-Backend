using EBAD_Backend.DataAccess;
using EBAD_Backend.DataAccess.Concrete;
using EBAD_Backend.DataAccess.Interface;
using EBAD_Backend.Extension;
using EBAD_Backend.Services.Concrete;
using EBAD_Backend.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddCors(options => options.AddPolicy(name: "Origin", policy =>
{
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandleMiddleware();

app.UseCors("Origin");

app.MapControllers();

app.Run();
