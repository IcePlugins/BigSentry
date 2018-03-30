using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Logger = Rocket.Core.Logging.Logger;

namespace BigSentry
{
    public class BigSentry : RocketPlugin<BigSentryConfiguration>
    {
        private List<Sentry> modified;

        protected override void Load()
        {
            modified = new List<Sentry>();

            ushort i = 0;
            while (true)
            {
                i++;

                if (Configuration.Instance.ignoreIds.Contains(i))
                    continue;

                Asset a = Assets.find(EAssetType.ITEM, i);

                if (a is ItemSentryAsset sentry)
                {
                    bool customExists = Configuration.Instance.overridesById.Any(x => x.Id == i);
                    byte width = customExists ? Configuration.Instance.overridesById.First(x => x.Id == i).Width : Configuration.Instance.globalWidthOverride;
                    byte height = customExists ? Configuration.Instance.overridesById.First(x => x.Id == i).Height : Configuration.Instance.globalHeightOverride;

                    modified.Add(new Sentry(i, sentry.storage_x, sentry.storage_y));

                    typeof(ItemSentryAsset).GetField("_storage_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sentry, width);
                    typeof(ItemSentryAsset).GetField("_storage_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sentry, height);

                    Logger.Log($"Modified '{sentry.itemName}'");
                }

                if (i == ushort.MaxValue)
                    break;
            }
        }

        protected override void Unload()
        {
            foreach (Sentry x in modified)
            {
                ItemSentryAsset sentry = (ItemSentryAsset)Assets.find(EAssetType.ITEM, x.Id);

                typeof(ItemSentryAsset).GetField("_storage_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sentry, x.Width);
                typeof(ItemSentryAsset).GetField("_storage_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sentry, x.Height);

                Logger.Log($"Reset '{sentry.itemName}'");
            }
        }
    }
}
