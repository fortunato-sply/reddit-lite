using backend.Model;
using backend.Controllers;
using backend.Repositories;
using backend.Services;
using Security.Jwt;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddScoped<RedditliteContext>(); // Shared Context
builder.Services.AddTransient<DataUserController>(); // Create class every req
builder.Services.AddTransient<IForumRepository, ForumRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddTransient<IPasswordProvider, PasswordProvider>(p => {
    return new PasswordProvider("minhasenha");
});
builder.Services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();


app.UseCors();
app.UseSwagger(); // Swagger for debug
app.UseSwaggerUI(); // Swagger for debug

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

public static class ExtensionMethods
{
    public static IEnumerable<T> AddMany<T>(this List<T> l, IEnumerable<T> elements)
    {
        foreach (var item in elements)
        {
            l.Add(item);
        }
        return l;
    }
}