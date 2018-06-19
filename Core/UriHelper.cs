using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal static class UriHelper
    {
        private static readonly Dictionary<string, Func<Uri, FileMode, FileAccess, Task<Stream>>> Resolvers = new Dictionary<string, Func<Uri, FileMode, FileAccess, Task<Stream>>>()
        {
            [Uri.UriSchemeFile] = (uri, mode, access) =>
            {

                string path = uri.AbsolutePath;
                path = Uri.UnescapeDataString(path);
                return Task.FromResult<Stream>(File.Open(path, mode, access));
            }

        };

        public static async Task<Stream> OpenAsync(Uri uri, FileMode mode, FileAccess fileAccess)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (!uri.IsAbsoluteUri)
            {
                var baseUri = new Uri(Environment.CurrentDirectory, UriKind.Absolute);
                uri = new Uri(baseUri, uri);
            }

            if(Resolvers.TryGetValue(uri.Scheme, out Func<Uri, FileMode, FileAccess, Task<Stream>> resolver))
            {
                return await resolver(uri, mode, fileAccess);
            }

            throw new InvalidOperationException($"Schema '{uri.Scheme}' has no attached handler");
        }
    }

}
