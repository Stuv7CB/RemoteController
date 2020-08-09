using Microsoft.Extensions.Logging;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using System;
using System.Collections.Concurrent;

namespace RemoteController.Server.Managers.Xbox
{
    public class XboxControllerManager
    {
        public XboxControllerManager(ViGEmClient client, ILogger<XboxControllerManager> logger)
        {
            ViGEmClient = client;
            Controllers = new ConcurrentDictionary<Guid, IXbox360Controller>();
            Logger = logger;
        }

        private ILogger<XboxControllerManager> Logger { get; }

        private ViGEmClient ViGEmClient { get; }

        private ConcurrentDictionary<Guid, IXbox360Controller> Controllers { get; }

        public IXbox360Controller GetController(Guid id)
        {
            Logger.LogDebug("Return controller with {Id}", id);
            return Controllers[id];
        }

        public Guid CreateController()
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug("Create controller with id {Id}", guid);
            Controllers.TryAdd(guid, ViGEmClient.CreateXbox360Controller());
            return guid;
        }
    }
}
