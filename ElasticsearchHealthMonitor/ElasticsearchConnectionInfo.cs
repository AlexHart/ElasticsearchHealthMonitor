using System;

namespace ElasticsearchHealthMonitor
{
    public class ElasticsearchConnectionInfo
    {
        public Uri Uri { get; }

        public string UserName { get; }

        public string Password { get; }

        public ElasticsearchConnectionInfo(Uri uri)
        {
            Uri = uri;
        }

        public ElasticsearchConnectionInfo(Uri uri, string userName, string password)
        {
            Uri = uri;
            UserName = userName;
            Password = password;
        }       

    }
}
