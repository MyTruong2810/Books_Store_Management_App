using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio;

namespace Books_Store_Management_App.Services
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;

        public MinioService(string endpoint, string accessKey, string secretKey)
        {
            _minioClient = (MinioClient)new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();
        }

        public async Task<bool> CheckBucketExists(string bucketName)
        {
            try
            {
                var bucketExistsArgs = new Minio.DataModel.Args.BucketExistsArgs();
                bucketExistsArgs.WithBucket(bucketName);

                return await _minioClient.BucketExistsAsync(bucketExistsArgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BucketExistsAsync] Error: {ex.Message}");
                return false;
            }
        }

        public async Task CreateBucketAsync(string bucketName)
        {
            try
            {
                bool found = await CheckBucketExists(bucketName);
                if (!found)
                {
                    var makeBucketArgs = new Minio.DataModel.Args.MakeBucketArgs()
                        .WithBucket(bucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateBucketAsync] Error: {ex.Message}");
            }
        }

        public async Task DeleteFileAsync(string bucketName, string objectName)
        {
            try
            {
                var removeObjectArgs = new Minio.DataModel.Args.RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteFileAsync] Error: {ex.Message}");
            }
        }

        public async Task UploadFileAsync(string bucketName, string objectName, string fileName, string contentType)
        {
            try
            {
                var putObjectArgs = new Minio.DataModel.Args.PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(fileName)
                    .WithContentType(contentType);

                await _minioClient.PutObjectAsync(putObjectArgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UploadFileAsync] Error: {ex.Message}");
            }
        }
    }
}
