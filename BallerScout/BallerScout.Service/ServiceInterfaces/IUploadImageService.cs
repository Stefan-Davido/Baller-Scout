using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IUploadImageService
    {
        void UploadProfileImage(IFormFile file);
        void UploadPostImage(IFormFile file);
        bool IsImage(string url);
    }
}
