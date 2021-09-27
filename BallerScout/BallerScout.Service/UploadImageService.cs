using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BallerScout.Service
{
    public class UploadImageService : IUploadImageService
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploadImageService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public bool IsImage(string url)
        {
            var extendion = Path.GetExtension(url);
            var boolean = false;

            //string[] parts = url.Split(".");
            //string mime = parts[parts.Length - 1];

            if (extendion == ".jpg" ||
                extendion == ".jpeg" ||
                extendion == ".pjpeg" ||
                extendion == ".gif" ||
                extendion == ".x-png" ||
                extendion == ".png")
            {
                boolean = true; 
            }
            else if(extendion == ".mp4")
            {
                boolean = false;
            }

            return boolean;
        }

        #region Upload Profile Image
        public async void UploadProfileImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = EnsureFileName(fileName);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(GetPathAndFileNameProfile(fileName)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }

        private string GetPathAndFileNameProfile(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\ProfilePhotos\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + fileName;
        }
        #endregion

        #region Upload Image On Post

        public async void UploadPostImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = EnsureFileName(fileName);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(GetPathAndFileNamePost(fileName)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }

        private string GetPathAndFileNamePost(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\PostsPhotos\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + fileName;
        }

        #endregion

        private string EnsureFileName(string fileName)
        {
            if (fileName.Contains("\\"))          
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
           
            return fileName;
        }
    }
}
