using ChatWS.EventHandler.DTOs;
using ChatWS.EventHandler.Events;
using ChatWS.EventHandler.Models;
using System.Text.Json;
using Websocket.Client;

namespace ChatWsTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Startup.StartUp(null);
        }

        [Test]
        public async Task Test1()
        {
            var ws = await new WebSocketTestClient().ConnectAsync();
            var ws2 = await new WebSocketTestClient().ConnectAsync();

            await ws.DoAndAssert(new ClientWantsToSignInDto()
            {
                UserName = "bob"
            }, res => res.Count(dto => dto.eventType == nameof(ServerWelcomesUser)) == 1);
            await ws2.DoAndAssert(new ClientWantsToSignInDto()
            {
                UserName = "steve"
            }, res => res.Count(dto => dto.eventType == nameof(ServerWelcomesUser)) == 1);

            await ws.DoAndAssert(new ClientWantsToEnterRoomDto()
            {
                roomId=1
            },res => res.Count(dto => dto.eventType == nameof(ServerAddsClientToRoom)) == 1);
            await ws2.DoAndAssert(new ClientWantsToEnterRoomDto()
            {
                roomId = 1
            }, res => res.Count(dto => dto.eventType == nameof(ServerAddsClientToRoom)) == 1);


            await ws.DoAndAssert(new ClientWantsToBroadcastToRoomDto()
            {
                roomId = 1,
                message = "hey im bob"
            }, res => res.Count(dto => dto.eventType == nameof(ServerBroadcastsMessageWithUserName)) == 1);
            await ws2.DoAndAssert(new ClientWantsToBroadcastToRoomDto()
            {
                roomId = 1,
                message = "hey im steve"
            },res => res.Count(dto => dto.eventType == nameof(ServerBroadcastsMessageWithUserName)) == 1);
        }
    }
}
