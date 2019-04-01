using Content.Client.Interfaces;
using Content.Shared.GameObjects.Components.Markers;
using SS14.Client.Interfaces.Console;
using SS14.Client.Interfaces.GameObjects.Components;
using SS14.Shared.GameObjects;
using SS14.Shared.Interfaces.GameObjects;
using SS14.Shared.IoC;

namespace Content.Client.Commands
{
    internal sealed class ShowMarkersCommand : IConsoleCommand
    {
        // ReSharper disable once StringLiteralTypo
        public string Command => "togglemarkers";
        public string Description => "Toggles visibility of markers such as spawn points.";
        public string Help => "";

        public bool Execute(IDebugConsole console, params string[] args)
        {
            bool? whichToSet = null;
            foreach (var entity in IoCManager.Resolve<IEntityManager>()
                .GetEntities(new TypeEntityQuery(typeof(SharedSpawnPointComponent))))
            {
                if (!entity.TryGetComponent(out ISpriteComponent sprite))
                {
                    continue;
                }

                if (!whichToSet.HasValue)
                {
                    whichToSet = !sprite.Visible;
                }

                sprite.Visible = whichToSet.Value;
            }

            return false;
        }
    }

    internal sealed class NotifyCommand : IConsoleCommand
    {
        public string Command => "notify";
        public string Description => "Send a notify client side.";
        public string Help => "notify <message>";

        public bool Execute(IDebugConsole console, params string[] args)
        {
            var message = args[0];

            var notifyManager = IoCManager.Resolve<IClientNotifyManager>();
            notifyManager.PopupMessage(message);

            return false;
        }
    }
}
