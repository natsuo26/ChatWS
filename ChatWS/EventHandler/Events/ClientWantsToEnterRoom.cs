using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Models;
using ChatWS.EventHandler.services;
using Fleck;
using System.Text.Json;

namespace ChatWS.EventHandler.Events
{
    public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
    {
        public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
        {
            StateService.AddToRoom(socket,dto.roomId);
            socket.Send(
            JsonSerializer.Serialize(
                new ServerAddsClientToRoom()
                    {
                        message = "You were successfully added to room with ID: " + dto.roomId
                    }
                )
            );
            return Task.CompletedTask;
        }
    }
}
