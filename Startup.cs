

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebScoket
{
    public class Startup
    {
        public int pi = 0;
        public List<Object> lista = new List<object>();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();
            
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webScoket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("Web Connect");

                    await ReceiveMassage(webScoket, async (result, buffer) =>
                    {
                       if(result.MessageType == WebSocketMessageType.Text)
                       {
                            await context.Response.WriteAsync("ok");
                            Console.WriteLine("Recebido");
                       }
                        else if(result.MessageType == WebSocketMessageType.Close)
                       {   
                           Console.WriteLine("Close Message");
                           return;
                       }
                   });
                }
                else
                {
                    await next();
                }
            });
            app.Run(async context => {
                pi++;
                lista.Add(pi);
                Console.WriteLine(pi);
                await context.Response.WriteAsync("ok");
            });            
        }

        private async Task ReceiveMassage(WebSocket socket, Action<WebSocketReceiveResult,byte[]> handleMessage) {
            var buffer = new byte[1024 * 4];
            while(socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);
                Console.WriteLine(result);
                handleMessage(result, buffer);
            }
        }
    }
}
