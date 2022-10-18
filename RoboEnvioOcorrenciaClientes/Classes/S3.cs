using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RoboEnvioOcorrenciaClientes.S3
{
    public class S3
    {

        public string _bucketName { get; set; }// = "*** provide bucket name ***";
        public string _keyName { get; set; }//= "*** provide a name for the uploaded object ***";
        public string _filePath { get; set; }// = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
        private IAmazonS3 s3Client;

        public S3(string bucketName, string keyName, string filePath)
        {
            _bucketName = bucketName;
            _keyName = keyName;
            _filePath = filePath;
        }

        
        public const string accessKey = "AKIA4D2WBDWQ6D2YWPNX";
        public const string secretKey = "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";


        public async Task<string> UploadFileSystemAsync(byte[] blob)
        {

            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "https://s3.amazonaws.com";


            try
            {
                using (IAmazonS3 client = new AmazonS3Client(accessKey, secretKey, config))
                {
                    var tu = new Amazon.S3.Transfer.TransferUtility(client);                                  

                    var ms = new System.IO.MemoryStream();
                    ms.Write(blob, 0, blob.Length);
                    ms.Position = 0;
                    var req = new Amazon.S3.Transfer.TransferUtilityUploadRequest();
                    req.BucketName = _bucketName;
                    req.Key = _keyName;
                    req.InputStream = ms;
                    tu.Upload(req);
                    try
                    {
                        await client.PutACLAsync(new PutACLRequest() { BucketName = _bucketName, CannedACL = S3CannedACL.PublicRead, Key = _keyName });

                    }
                    catch (Exception xx)
                    {
                        string rr = xx.Message;
                    }
                    return "https://s3.amazonaws.com/" + _bucketName + "/" + _keyName;
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return "https://s3.amazonaws.com/" + _bucketName + "/" + _keyName;
        }

        public void CreateFolder(string bucketName, string folderName)
        {
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "https://s3.amazonaws.com";

            IAmazonS3 s3Client = new AmazonS3Client(accessKey, secretKey, config);

            var folderKey = folderName;
            var request = new PutObjectRequest();
            request.BucketName = bucketName;
            request.StorageClass = S3StorageClass.Standard;
            request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.None;
            request.Key = folderKey;
            var response = s3Client.PutObjectAsync(request).Result;



        }

        public async Task<string> BaixarArquivo(string bucketName, string keyName)
        {
            //string keyName = "clientes/FACILITY00002/74.395.542/CTe/Xml/35200474395542000147570010011418221000074466-CTe.XML";
            string[] keySplit = keyName.Split('/');
            string fileName = keySplit[keySplit.Length - 1];

            //string dest = Path.Combine("c:\\temp\\", fileName);

            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "https://s3.amazonaws.com";

            using (IAmazonS3 client = new AmazonS3Client(accessKey, secretKey, config))
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = bucketName;// "facilityeletronicos";
                request.Key = keyName;

                using (GetObjectResponse response = await client.GetObjectAsync(request))
                {
                    using (var reader = new StreamReader(response.ResponseStream))
                    {
                        return reader.ReadToEnd();
                    }


                    //      response.WriteResponseStreamToFile(dest, false);
                }


                // System.IO.File.Delete(dest);
            }
        }

    }
}