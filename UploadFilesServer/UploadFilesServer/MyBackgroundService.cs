using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UploadFilesServer.Context;
using Microsoft.Extensions.Hosting;

namespace UploadFilesServer
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        //private readonly DataContext _context;
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(ILogger<MyBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {

                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var images = await context.Images.Where(i => i.pullFlag == false).ToListAsync();
                    Console.WriteLine(images.Count);
              
                   if (images.Count > 0)
                   {
                        var connectionString = "Endpoint=sb://uploader.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yNo6ZI0QELFseDTIxMjpVosWPPoEYRG/6aqyh0Qj73c=";
                        
                        var sender = new ServiceBusClient(connectionString).CreateSender("add-image-data");
                        var body = JsonSerializer.Serialize(images);

                        await sender.SendMessageAsync(new ServiceBusMessage(body));

                   }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                        
                    foreach (var image in images)
                    {
                        image.pullFlag = true;
                    }
                    await context.SaveChangesAsync();

                }
            }

        }
    }
}


