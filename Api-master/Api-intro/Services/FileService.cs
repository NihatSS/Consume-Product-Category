using Api_intro.Helpers.Response;
using Api_intro.Services.Interfaces;

namespace Api_intro.Services
{
    public class FileService : IFileService
    {
        public void Delete(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public async Task<UploadResponse> UploadAsync(IFormFile file)
        {
            List<string> validExtentions = new() { ".jpg", ".png", ".gif", ".webp" };
            string fileExtention = Path.GetExtension(file.FileName);
            if (!validExtentions.Contains(fileExtention))
                return new UploadResponse
                {
                    HasError = true,
                    Response = $"File extention is wrong!(valid extentions : ({string.Join(",", validExtentions)}))"
                };


            string fileName = Guid.NewGuid().ToString() + fileExtention;

            if (!Directory.Exists("Uploads"))
            {
                Directory.CreateDirectory("Uploads");
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return new UploadResponse
            {
                HasError = false,
                Response = fileName
            };
        }
    }
}
