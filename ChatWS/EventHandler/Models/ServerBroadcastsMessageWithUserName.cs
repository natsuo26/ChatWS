using ChatWS.EventHandler.DTOs;

namespace ChatWS.EventHandler.Models
{
    public class ServerBroadcastsMessageWithUserName:BaseDto
    {
        public string message {  get; set; }
        public string UserName {  get; set; }
    }
}
