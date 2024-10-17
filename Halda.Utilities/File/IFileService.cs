using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Halda.Utilities.FileUpload
{
    public interface IFileService
    {
        string FileUploadProcessing(IFormFile uploadfile, string folder);
        void Filedeleteprocess(string file, string folder);
        string FileUploadProcessing(string base64File, string fileName, string folder);
        FileStream PrepareFileDownload(string filePath);
    }
}
