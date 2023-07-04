using backend.Model;
using backend.Controllers;
using backend.Repositories;
using backend.Services;
using Securitas.JWT;
using Securitas;
using System.Security.Cryptography;
using System.Text;

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

#region base services

builder.Services.AddScoped<RedditliteContext>(); // Shared Context
builder.Services.AddTransient<DataUserController>(); // Create class every req
builder.Services.AddTransient<IForumRepository, ForumRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSingleton(
    p => new EnvironmentFile(".env")
);

builder.Services.AddScoped<HashAlgorithm>(
    p => SHA256.Create()
);

builder.Services.AddSingleton(
    p => Encoding.UTF8
);

#endregion

#region Security services

builder.Services.AddTransient<IPasswordProvider>(
    p => new ConstPasswordProvider(
        p.GetService<EnvironmentFile>()
            .Get("SECRET_PASSWORD")
    )
);

builder.Services.AddTransient<ISecurityService>(
    p => new SecurityService()
);


builder.Services.AddTransient<IJWTService>(
    p => new JWTService(
        p.GetService<IPasswordProvider>()!,
        p.GetService<Encoding>()!,
        HashAlgorithmType.HS256
    )
);

#endregion

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