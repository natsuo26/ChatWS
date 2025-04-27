using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Models;
using ChatWS.EventHandler.services;
using Fleck;
using System.Text.Json;

namespace ChatWS.EventHandler.Events
{
    public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
    {
        public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
        {
            StateService.Connections[socket.ConnectionInfo.Id].UserName = dto.UserName;
            socket.Send(JsonSerializer.Serialize(new ServerWelcomesUser()));
            return Task.CompletedTask;
        }
    }
}
