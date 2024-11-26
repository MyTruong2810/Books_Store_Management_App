using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services
{
    public interface IMinioService
    {
        Task<bool> CheckBucketExists(string bucketName);
        Task CreateBucketAsync(string bucketName);
        Task DeleteFileAsync(string bucketName, string objectName);

        Task UploadFileAsync(string bucketName, string objectName, string fileName, string contentType);
    }
}
