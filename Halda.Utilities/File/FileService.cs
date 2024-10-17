using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Halda.Utilities.FileUpload
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment  )
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public string FileUploadProcessing(IFormFile uploadfile, string folder)
        {

            string fileName = uploadfile.FileName;
            string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();


            string newfilename = uploadfile.FileName.Replace(uploadfile.FileName, "Product") + "_" + Guid.NewGuid().ToString() + "." + FileExtension;
            string uploadedfilepath = Path.Combine(hostingEnvironment.ContentRootPath, folder, newfilename);
            uploadfile.CopyTo(new FileStream(uploadedfilepath, FileMode.Create));


            return uploadedfilepath;


            

        }

        public string FileUploadProcessing(string base64File, string fileName, string folder)
        {
            // Define the base path for saving the applicant files
            string basePath = Path.Combine(hostingEnvironment.ContentRootPath, folder);

            // Ensure the directory exists
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // Check if the base64 string contains metadata
            string base64Data = base64File.Contains(",") ? base64File.Split(',')[1] : base64File;
            // Decode the Base64 string
            byte[] fileBytes = Convert.FromBase64String(base64Data);
            // Construct the full file path
            string filePath = Path.Combine(basePath, fileName);
            // Save the file
            File.WriteAllBytes(filePath, fileBytes);
            return filePath; // Return the file path to store in the database
        }

        public void Filedeleteprocess(string file, string folder)
        {
            if (File.Exists(Path.Combine(folder, file)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(folder, file));

            }

        }

        public FileStream PrepareFileDownload(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path is required.", nameof(filePath));
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            // Open the file stream for reading
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return fileStream;
        }


    }

}
