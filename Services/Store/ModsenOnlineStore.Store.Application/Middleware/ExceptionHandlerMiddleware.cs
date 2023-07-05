using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Application.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync (HttpContext context) {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var exceptionThrowingСontext = ex.StackTrace.Remove(0, "   at ".Length);
                exceptionThrowingСontext = exceptionThrowingСontext.Substring(0, exceptionThrowingСontext.IndexOf("at") - 2);

                var exceptionMessage = ex.InnerException;

                Console.WriteLine(ex);
            }
        }
    }
}
