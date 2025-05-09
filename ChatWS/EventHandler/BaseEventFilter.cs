﻿using ChatWS.EventHandler.DTOs;
using Fleck;


namespace ChatWS.EventHandler
{
    public interface IBaseEventFilter
    {
        Task Handle<T>(IWebSocketConnection socket, T dto) where T : BaseDto;

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class BaseEventFilter : Attribute, IBaseEventFilter
    {
        public abstract Task Handle<T>(IWebSocketConnection socket, T dto) where T : BaseDto;
    }
}
