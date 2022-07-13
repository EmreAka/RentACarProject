using System;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.CloudinaryAdapter
{
    public class CloudinaryAdapter
    {
        private readonly IConfiguration _configuration;
        public CloudinaryAdapter()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public string UploadPhoto(IFormFile file)
        {
            string picture;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                picture = Convert.ToBase64String(fileBytes);
            }
            Account account = new Account(_configuration["Cloudinary:Cloud"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($@"data:image/png;base64,{picture}"),
                Transformation = new Transformation().Width(1280).Height(720).Crop("fill")
            };
            var result = cloudinary.Upload(uploadParams);
            return result.Url.ToString();
        }
    }
}
