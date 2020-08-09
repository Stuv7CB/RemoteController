using Grpc.Core;
using Microsoft.Extensions.Logging;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using RemoteController.Rpc;
using RemoteController.Server.Managers.Xbox;
using System;
using System.Threading.Tasks;

namespace RemoteController.Server.Services
{
    public class ControllerFactoryService : ControllerFactory.ControllerFactoryBase
    {
        public ControllerFactoryService(
            ILogger<ControllerFactoryService> logger,
            XboxControllerManager xboxControllerManager)
        {
            Logger = logger;
            XboxControllerManager = xboxControllerManager;
        }

        private ILogger<ControllerFactoryService> Logger { get; }

        private XboxControllerManager XboxControllerManager { get; }

        public override Task<XboxControllerReply> CreateXboxController(XboxControllerRequest request, ServerCallContext context)
        {
            Logger.LogDebug("Receive request");
            return Task.FromResult(new XboxControllerReply
            {
                Id = XboxControllerManager.CreateController().ToString()
            });
        }

        public override Task<XboxControllerConnectReply> ConnectXboxController(XboxControllerConnectRequest request, ServerCallContext context)
        {
            Logger.LogDebug("Connect controller");
            XboxControllerManager.GetController(Guid.Parse(request.Id)).Connect();
            return Task.FromResult(new XboxControllerConnectReply());
        }
    }
}
