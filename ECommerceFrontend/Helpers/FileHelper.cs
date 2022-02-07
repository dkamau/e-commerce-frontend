using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ECommerceFrontend.Helpers
{
    public static class FileHelper
    {
        public static KeyValuePair<bool, string> FileIsValid(IBrowserFile file)
        {
            string fileExtension = Path.GetExtension(file.Name).ToLower();

            if (!fileExtension.Equals(".png") &&
                !fileExtension.Equals(".gif") &&
                !fileExtension.Equals(".jpg") &&
                !fileExtension.Equals(".jpeg"))
            {
                return new KeyValuePair<bool, string>(false, "Please select a valid image file (.png, .gif, .jpg or .jpeg).");
            }

            if (file.Size > 1000000)
            {
                return new KeyValuePair<bool, string>(false, "Please select a file that is less than 1Mb in size.");
            }

            return new KeyValuePair<bool, string>(true, "");
        }

        public async static Task<string> ConvertToBase64(this IBrowserFile file)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();

                return Convert.ToBase64String(bytes);
            }
        }
    }
}
