using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Models;
using ChatWS.EventHandler.services;
using Fleck;
using System.Text.Json;

namespace ChatWS.EventHandler.Events
{
    public class ClientWantsToBroadcastToRoom : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
    {
        public override Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
        {
            var message = new ServerBroadcastsMessageWithUserName()
            {
                UserName = StateService.Connections[socket.ConnectionInfo.Id].UserName,
                message = dto.message,
            };
            StateService.BroadcastToRoom(dto.roomId, JsonSerializer.Serialize(message));
            return Task.CompletedTask;
        }
    }
}
