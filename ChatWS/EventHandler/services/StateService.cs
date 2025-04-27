using ChatWS.EventHandler.Models;
using Fleck;

namespace ChatWS.EventHandler.services
{
    public static class StateService
    {
        /// <summary>
        /// stores the Guid mapped to its respective webSocketConnection
        /// </summary>
        public static Dictionary<Guid, WsWithMetadata> Connections = new();

        ///<summary>
        /// stores the rooms and all the connected webSocketConnections to those rooms
        /// </summary>
        public static Dictionary<int , HashSet<Guid>> Rooms = new();



        /// <summary>
        /// adding the connections with the premade Guid to the dict. so we have a map of guids and its
        /// respective webSocketConnection object reference.
        /// </summary>
        public static bool AddConnections(IWebSocketConnection ws)
        {
           return Connections.TryAdd(ws.ConnectionInfo.Id, new WsWithMetadata(ws));
        }


        /// <summary>
        /// Adds the provided webSocketConnection into the provided room
        /// </summary>
        public static bool AddToRoom(IWebSocketConnection ws, int room)
        {
            if (!Rooms.ContainsKey(room))
            {
                Rooms.Add(room, new HashSet<Guid>());
            }
            return Rooms[room].Add(ws.ConnectionInfo.Id);
        }


        /// <summary>
        /// sends the message provided to all the users that belongs to the provided room.
        /// </summary>
        public static void BroadcastToRoom(int room, string message)
        {
            if(Rooms.TryGetValue(room,out var guids))
                foreach(var guid in guids)
                    if(Connections.TryGetValue(guid, out var webSocketConn))
                        webSocketConn.Connection.Send(message);
        }

    }
}
