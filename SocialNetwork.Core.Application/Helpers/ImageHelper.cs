using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Core.Application.Helpers
{
    public class ImageHelper
    {
        public string UploadImage(IFormFile file, string basePath, string oldImage = null, bool editMode = false)
        {
            if (editMode && file == null)
            {
                return oldImage;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fullFilePath = Path.Combine(path, filename);
            string relativeFilePath = Path.Combine(basePath, filename);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (editMode)
            {
                string[] textSplitted = oldImage.Split("\\");
                oldImage = textSplitted[^1];

                string oldImagePath = Path.Combine(path, oldImage);

                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            return relativeFilePath;
        }
    }
}