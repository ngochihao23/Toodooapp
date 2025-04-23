using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace THUC_HANH_3.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);


         if ((context.Response.StatusCode) == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = "Chưa Đăng Nhập";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            }
        else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden) 
            {
                context.Response.ContentType= "application/json";
                var response = "Tài khoản không có quyền";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

        

        }
    }
}
