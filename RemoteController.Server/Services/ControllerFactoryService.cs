using AutoMapper;
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
            XboxControllerManager xboxControllerManager,
            IMapper mapper)
        {
            Logger = logger;
            XboxControllerManager = xboxControllerManager;
            Mapper = mapper;
        }

        private ILogger<ControllerFactoryService> Logger { get; }

        private XboxControllerManager XboxControllerManager { get; }

        private IMapper Mapper { get; }

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

        public override async Task StartController(IAsyncStreamReader<XboxMessageRequest> requestStream, IServerStreamWriter<XboxMessageReply> responseStream, ServerCallContext context)
        {
            var controller = XboxControllerManager.GetController(Guid.NewGuid());
            controller.FeedbackReceived += async (s, e) => await responseStream.WriteAsync(new XboxMessageReply());
            await foreach (var request in requestStream.ReadAllAsync())
            {
                switch (request.TypeCase)
                {
                    case XboxMessageRequest.TypeOneofCase.ButtonPressed:
                        controller.SetButtonState(
                            Mapper.Map<Rpc.XboxMessageRequest.Types.Buttons, Xbox360Button>(request.ButtonPressed.Button),
                            pressed: true);
                        continue;
                    case XboxMessageRequest.TypeOneofCase.ButtonReleased:
                        controller.SetButtonState(
                            Mapper.Map<Rpc.XboxMessageRequest.Types.Buttons, Xbox360Button>(request.ButtonPressed.Button),
                            pressed: false);
                        continue;
                    case XboxMessageRequest.TypeOneofCase.None:
                        continue;
                }
            }
        }
    }
}
