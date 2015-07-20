using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Swaksoft.Core.External;

namespace Restaurant.Host.Configuration
{
    public class SocialProvider : ConfigurationElement
    {
        [ConfigurationProperty("type", DefaultValue = ExternalProvider.Twitter, IsKey = true, IsRequired = true)]
        public ExternalProvider Type
        {
            get
            {
                return (ExternalProvider)this["type"];
            }

            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("consumerKey", IsRequired = true)]
        public string ConsumerKey
        {
            get
            {
                return (string)this["consumerKey"];
            }

            set
            {
                this["consumerKey"] = value;
            }
        }

        [ConfigurationProperty("consumerSecret", IsRequired = true)]
        public string ConsumerSecret
        {
            get
            {
                return (string)this["consumerSecret"];
            }

            set
            {
                this["consumerSecret"] = value;
            }
        }

        [ConfigurationProperty("oauthCallbackUrl", IsRequired = false)]
        public Uri OAuthCallbackUrl
        {
            get { return this["oauthCallbackUrl"] as Uri; }
            set { this["oauthCallbackUrl"] = value; }
        }
    }
}