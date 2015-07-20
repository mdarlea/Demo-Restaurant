using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Restaurant.Host.Configuration
{
    public class SocialSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public SocialProviderCollection SocialProviders
        {
            get { return ((SocialProviderCollection)(base["providers"])); }
            set { base["providers"] = value; }
        }
    }
}