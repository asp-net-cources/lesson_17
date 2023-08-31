using Lesson17.JsonSettings.Converters;
using Lesson17.JsonSettings.Policies;
using System.Text.Json.Serialization;
using Lesson17.LifetimeExamples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = new UpperCaseNamingPolicy();
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new ProductJsonConverter());
    });

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SingletonClass>();
builder.Services.AddTransient<TransientClass>();
builder.Services.AddScoped<ScopedClass>();
builder.Services.AddScoped<Service1>();
builder.Services.AddScoped<Service2>();

var app = builder.Build();

Console.WriteLine("Singleton");
var singleton1 = app.Services.GetService<SingletonClass>();
Console.WriteLine(singleton1.Some);
var singleton2 = app.Services.GetService<SingletonClass>();
Console.WriteLine(singleton2.Some);

Console.WriteLine("Transient");
var transient1 = app.Services.GetService<TransientClass>();
Console.WriteLine(transient1.Some);
var transient2 = app.Services.GetService<TransientClass>();
Console.WriteLine(transient2.Some);

Console.WriteLine("Scoped");
using (var scope = app.Services.CreateScope())
{
    var scoped1 = scope.ServiceProvider.GetService<ScopedClass>();
    Console.WriteLine(scoped1.Some);
    var scoped2 = scope.ServiceProvider.GetService<ScopedClass>();
    Console.WriteLine(scoped2.Some);
}
using (var scope = app.Services.CreateScope())
{
    var scoped1 = scope.ServiceProvider.GetService<ScopedClass>();
    Console.WriteLine(scoped1.Some);
    var scoped2 = scope.ServiceProvider.GetService<ScopedClass>();
    Console.WriteLine(scoped2.Some);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Before handling");
//     await Task.CompletedTask;
//     if (false)
//         await next.Invoke();
//     context.Response.StatusCode = 302;
// });

app.Run();