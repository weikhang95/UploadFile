using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using UploadFilesServer.Context;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
//using UploadFilesServer.HubConfig;
using UploadFilesServer.Models;

namespace UploadFilesServer
{
    public class MyBackgroundService2 : BackgroundService
    {
        public MyBackgroundService2()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                var connectionString = "Endpoint=sb://uploader.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yNo6ZI0QELFseDTIxMjpVosWPPoEYRG/6aqyh0Qj73c=";

                await using var receiver = new ServiceBusClient(connectionString).CreateReceiver("add-image-data");
                var message = await receiver.ReceiveMessageAsync();
                string body = message.Body.ToString();
                
                //Acknowledge the message by marking it as complete
                await receiver.CompleteMessageAsync(message);

            }

        }

     
    }
}

