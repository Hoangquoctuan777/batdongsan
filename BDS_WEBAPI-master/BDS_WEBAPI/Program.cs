using BDS_WEBAPI.Respository;
using BDS_WEBAPI.IRespository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRespository, UserRespository>();
builder.Services.AddScoped<INewsRespository, NewsRespository>();
builder.Services.AddScoped<IPropertiesRespository, PropertiesRespository>();
builder.Services.AddScoped<ISessionRespository, SessionsRespository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
