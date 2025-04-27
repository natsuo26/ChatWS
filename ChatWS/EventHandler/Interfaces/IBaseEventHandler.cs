using ChatWS.EventHandler.DTOs;
using Fleck;

namespace ChatWS.EventHandler.Interfaces
{
    public interface IBaseEventHandler<T> where T : BaseDto
    {
        string eventType { get; }

        Task InvokeHandle(string message, IWebSocketConnection socket);

        Task Handle(T dto, IWebSocketConnection socket);
    }
}
