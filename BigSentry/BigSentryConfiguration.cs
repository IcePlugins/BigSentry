using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BigSentry
{
    public class BigSentryConfiguration : IRocketPluginConfiguration
    {
        public byte globalWidthOverride;
        public byte globalHeightOverride;

        [XmlArrayItem(ElementName = "Id")]
        public List<ushort> ignoreIds;

        public List<Sentry> overridesById;

        public void LoadDefaults()
        {
            globalWidthOverride = 8;
            globalHeightOverride = 8;

            ignoreIds = new List<ushort> { 1372 };
            overridesById = new List<Sentry> { new Sentry(1373, 6, 3) };
        }
    }
}
