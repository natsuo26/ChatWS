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
            //var wsClient = new WebsocketClient(new Uri("ws://localhost:8181"));
            //wsClient.MessageReceived.Subscribe(msg =>
            //{
            //    Console.WriteLine("what we got from the server: "+msg.Text);
            //});
            //wsClient.Start();
            //var message = new ClientWantsToEchoServerDto()
            //{
            //    messageContent = "hey"
            //};
            //wsClient.Send(JsonSerializer.Serialize(message));
            //Task.Delay(5000).Wait();
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
