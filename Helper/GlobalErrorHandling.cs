using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.ViewModle.authVIewModel;

namespace FoodApp.Api.Helper
{
    public class GlobalErrorHandling
    {
      
            RequestDelegate next;

            public GlobalErrorHandling(RequestDelegate _next)
            {
                next = _next;
            }

            public async Task InvokeAsync(HttpContext context)
            {

                try
                {  
                    await next(context);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(@"F:\\log.txt", $"error{ex.Message}");

                    var Response = new FailerResView<bool>(Errorcode.None);

                    await context.Response.WriteAsJsonAsync(Response);
                }


            }


        }
    }
