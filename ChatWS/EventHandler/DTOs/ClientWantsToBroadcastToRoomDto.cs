namespace ChatWS.EventHandler.DTOs
{
    public class ClientWantsToBroadcastToRoomDto : BaseDto
    {
        public int roomId { get; set; }
        public string message { get; set; }
    }
}
