using System.Text.Json.Serialization;
using Lesson17.LifetimeExamples;
using Microsoft.AspNetCore.Mvc;

namespace Lesson17.Controllers;

[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly Service1 _service1;
    private readonly Service2 _service2;
    private const string _login = "user";
    private const string _pwd = "qwerty";
    private const string _token = "123";

    public TestController(Service1 service1, Service2 service2)
    {
        _service1 = service1;
        _service2 = service2;
    }
    
    [HttpGet("test")]
    public ActionResult GetTest()
    {
        HttpContext.Response.Cookies.Append("key", "value", new CookieOptions()
        {
            Expires = DateTimeOffset.Now + TimeSpan.FromDays(1),
            Domain = HttpContext.Request.Host.Host,
            Path = "/"
        });
        Console.WriteLine(string.Join(Environment.NewLine, HttpContext.Request.Cookies));
        return Ok("You are cool");
    }
    
    [HttpGet("test2")]
    public string GetTest2()
    {
        var ans = string.Join(Environment.NewLine, HttpContext.Request.Cookies);
        return ans;
    }
    
    [HttpGet("/private")]
    public ActionResult GetPrivateTest()
    {
        if (HttpContext.Request.Cookies.TryGetValue("token", out var token)
            && token == _token)
        {
            return Ok($"You are {_login}");
        }

        return NotFound();
    }
    
    [HttpGet("/auth")]
    public ActionResult Authorize(string login, string password)
    {
        if (login == _login && password == _pwd)
        {
            HttpContext.Response.Cookies.Append("token", _token, new CookieOptions()
            {
                Expires = DateTimeOffset.Now + TimeSpan.FromDays(1),
                Domain = HttpContext.Request.Host.Host,
                Path = "/",
                Secure = true
            });
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}