using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.Service.Interfaces;
using VDemyanov.MathWars.Service.Utils;

namespace VDemyanov.MathWars.Service.Implementation
{
    public class DropboxService : IDropboxService
    {
        public string AccessToken { get; }
        public string MyProperty { get; set; }

        public DropboxService(IConfiguration configuration)
        {
            AccessToken = configuration["Dropbox:AccessToken"];
        }

        public async Task<string> Upload(string folder, string file, string userId, byte[] content)
        { 
            string url = "";
            string remotePath = folder + "/" + userId + file;
            using (var dbx = new DropboxClient(AccessToken))
            {
                using (var mem = new MemoryStream(content))
                {
                    ListSharedLinksResult link = null;

                    try
                    {
                        link = await dbx.Sharing.ListSharedLinksAsync(remotePath);
                    }
                    catch
                    {
                        await dbx.Files.UploadAsync(remotePath, WriteMode.Overwrite.Instance, body: mem);
                        link = await dbx.Sharing.ListSharedLinksAsync(remotePath);
                    }
                    
                    if (link.Links.Count == 0)
                    {
                        var res = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(remotePath);
                        url = res.Url.Split("?")[0];
                        url += "?raw=1";

                        //var result = await dbx.Files.GetTemporaryLinkAsync(remotePath);
                        //url = result.Link;
                    }
                    else
                    {
                        //var res = await dbx.Sharing.GetSharedLinkFileAsync(null, remotePath);
                        
                        url = link.Links[0].Url.Split("?")[0];
                        url += "?raw=1";

                        //var result = await dbx.Files.GetTemporaryLinkAsync(remotePath);
                        //url = result.Link;
                    }
                        
                }
            }
            return url;
        }

        public async Task Delete(string url)
        {
            using (var dbx = new DropboxClient(AccessToken))
            {
                var md = await dbx.Sharing.GetSharedLinkMetadataAsync(url);
                await dbx.Files.DeleteV2Async(md.PathLower);
            }
        }
    }
}
