﻿namespace ChatWS.EventHandler.services
{

    public interface IEventHandlersService
    {
        HashSet<Type> EventHandlers { get; set; }
    }
    public class EventHandlersService : IEventHandlersService
    {
        public HashSet<Type> EventHandlers { get; set; }
    }
}
