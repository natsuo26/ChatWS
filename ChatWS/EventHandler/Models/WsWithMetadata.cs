using Fleck;

namespace ChatWS.EventHandler.Models
{
    public class WsWithMetadata(IWebSocketConnection connection)
    {
        public IWebSocketConnection Connection { get; set; } = connection;
        public string UserName { get; set; }
    }
}
