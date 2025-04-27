
using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Models;
using Fleck;
using System.Text.Json;

namespace ChatWS.EventHandler.Events
{
    public class ClientWantsToEchoServer : BaseEventHandler<ClientWantsToEchoServerDto>
    {
        public override Task Handle(ClientWantsToEchoServerDto dto, IWebSocketConnection socket)
        {
            var echo = new ServerEchosClient()
            {
                echoValue = "echo: " + dto.messageContent
            };
            var messageToClient = JsonSerializer.Serialize(echo);
            socket.Send(messageToClient);

            return Task.CompletedTask;
        }
    }
}
