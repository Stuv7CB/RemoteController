using Grpc.Core;
using Microsoft.Extensions.Logging;
using RemoteController.Rpc;
using System;
using System.Threading.Tasks;

namespace RemoteController.Server.Services
{
    public class ControllerFactoryService : ControllerFactory.ControllerFactoryBase
    {
        private readonly ILogger<ControllerFactoryService> _logger;

        public ControllerFactoryService(ILogger<ControllerFactoryService> logger)
        {
            _logger = logger;
        }

        public override Task<XboxControllerReply> CreateXboxController(XboxControllerRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Receive request");
            return Task.FromResult(new XboxControllerReply()
            {
                Id = Guid.NewGuid().ToString(),
            });
        }
    }
}
