using Dropbox.Api;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IDropboxService
    {
        Task<string> Upload(string folder, string file, string userId, byte[] content);
        Task Delete(string url);
    }
}
