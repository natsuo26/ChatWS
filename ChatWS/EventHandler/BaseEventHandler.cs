using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Interfaces;
using Fleck;
using System.Reflection;
using System.Text.Json;

namespace ChatWS.EventHandler
{
    public abstract class BaseEventHandler<T> : IBaseEventHandler<T> where T : BaseDto
    {
        public string eventType => GetType().Name;

        public async Task InvokeHandle(string message, IWebSocketConnection socket) //todo cancellationtoken
        {
            var dto = JsonSerializer.Deserialize<T>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new ArgumentException("Could not deserialize into " + typeof(T).Name + "from string: " + message);
            foreach (var baseEventFilterAttribute in GetType().GetCustomAttributes().OfType<BaseEventFilter>())
                await baseEventFilterAttribute.Handle(socket, dto);
            await Handle(dto, socket);
        }

        public abstract Task Handle(T dto, IWebSocketConnection socket);
    }
}
