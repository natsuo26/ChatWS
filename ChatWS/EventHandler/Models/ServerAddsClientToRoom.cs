using ChatWS.EventHandler.DTOs;

namespace ChatWS.EventHandler.Models
{
    public class ServerAddsClientToRoom:BaseDto
    {
        public string message { set; get; }
    }
}
