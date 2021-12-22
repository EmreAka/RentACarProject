﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.CloudinaryAdapter
{
    public class CloudinaryAdapter
    {
        public static string UploadPhoto(IFormFile file)
        {
            string picture;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                picture = Convert.ToBase64String(fileBytes);
            }
            Account account = new Account("emreaka", "946292829472262", "AOtqB7yeA2UVCUpT-0fuStKnIYU");
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
