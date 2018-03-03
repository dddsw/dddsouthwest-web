using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace DDDSouthWest.Website.ImageHandlers
{
    public class ProfileImageHandler
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IList<string> _supportedTypes = new List<string>
        {
            "image/png",
            "image/jpg",
            "image/jpeg"
        };

        public ProfileImageHandler(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public (bool Success, List<ValidationFailure> Errors) SaveProfilePicture(IFormFile file, string userId)
        {
            if (file == null || file.Length == 0)
                return (false, new List<ValidationFailure>());

            var result = IsValidImage(file);
            if (result.Success)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/");

                var extension = string.Empty;
                switch (_supportedTypes.FirstOrDefault(x => x == file.ContentType))
                {
                    case "image/png":
                        extension = ".png";
                        break;
                    case "image/jpg":
                    case "image/jpeg":
                        extension = ".jpg";
                        break;
                }

                // Original
                var original = Path.Combine(uploads, "original_" + userId + extension);
                file.CopyTo(new FileStream(original, FileMode.Create));
            
                // Profile
                var profile = Path.Combine(uploads, "profile_" + userId + ".jpg");
                CreateProfilePic(original, profile);
            }

            return result;
        }

        private static void CreateProfilePic(string path, string profile)
        {
            using (var image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size(150, 150) 
                }));
                image.Save(profile);
            }
        }

        public (bool Exists, string Path) ResolveProfilePicture(int speakerId)
        {
            var image = "/uploads/profile_" + speakerId + ".jpg";
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/profile_" + speakerId + ".jpg");
            bool exists = File.Exists(fullPath);

            if (!exists)
                image = "/images/profile_default.png";

            return (exists, image);
        }

        private (bool Success, List<ValidationFailure> Errors) IsValidImage(IFormFile file)
        {            
            if (!_supportedTypes.Any(x => x.Contains(file.ContentType)))
                return (false, new List<ValidationFailure>
                {
                    new ValidationFailure("image", "Image must be either .jpg/jpeg or .png format")
                });

            return (true, new List<ValidationFailure>());
        }
    }
}