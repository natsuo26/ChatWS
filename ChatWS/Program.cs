using ChatWS.EventHandler;
using ChatWS.EventHandler.services;
using Fleck;
using System.Reflection;


public static class Startup
{
    public static void Main(string[] args)
    {

        StartUp(args);
        Console.ReadLine();
    }

    public static void StartUp(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());


        var app = builder.Build();

        var server = new WebSocketServer("ws://0.0.0.0:8181");

        var wsConnections = new List<IWebSocketConnection>();

        server.Start(ws =>
        {
            ws.OnOpen = () =>
            {
                StateService.AddConnections(ws);
            };

            ws.OnMessage = async message =>
            {
                try
                {
                    await app.InvokeClientEventHandler(clientEventHandlers, ws, message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException);
                    Console.WriteLine(e.StackTrace);

                }
            };
        });
    }
}