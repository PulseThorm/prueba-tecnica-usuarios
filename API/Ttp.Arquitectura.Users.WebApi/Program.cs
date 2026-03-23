using Microsoft.EntityFrameworkCore;
using Ttp.Arquitectura.Users.Repository;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

//DB
builder.Services.AddDbContext<UsersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("users-db")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Comands
builder.Services.AddScoped<AddUserHandler>();
builder.Services.AddScoped<DeleteUserHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddHttpClient();

//Queries
builder.Services.AddScoped<GetUsersHandler>();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//CORS para que la api pueda funcionar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFront", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAngularFront");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
