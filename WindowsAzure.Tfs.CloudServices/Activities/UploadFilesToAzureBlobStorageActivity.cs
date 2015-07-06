using Microsoft.WindowsAzure.Storage;
using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow.Tracking;

namespace WindowsAzure.Tfs.CloudServices.Activities
{
    public sealed class UploadFilesToAzureBlobStorageActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> FilePaths { get; set; }

        public InArgument<string> StorageAccountName { get; set; }
        public InArgument<string> StorageAccountKey { get; set; }
        public InArgument<string> BasePath { get; set; }
        public InArgument<string> DropLocation { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            var filepaths = context.GetValue(this.FilePaths);
            var name = context.GetValue(this.StorageAccountName);
            var key = context.GetValue(this.StorageAccountKey);
            var basepath = context.GetValue(this.BasePath);
            var dropLocation = context.GetValue(this.DropLocation);

         
            var account = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(name, key), true);
            var access = account.CreateCloudBlobClient();


            var container = access.GetContainerReference(basepath);
            container.CreateIfNotExists();
            var dirname = Path.GetFileName(Path.GetDirectoryName(filepaths));
            foreach (var file in Directory.GetFiles(filepaths,"*",SearchOption.AllDirectories))
            {
                
                var path = string.IsNullOrEmpty(dropLocation) ? string.Format("{0}/{1}", dirname, Path.GetFileName(file))
                : file.Replace(dropLocation, "").Replace("\\","/");
                var blob = container.GetBlockBlobReference(path);
                   // 

             //   var blob = container.GetBlockBlobReference(String.Join("/",
             //       filepaths.Replace("\\", "/").Trim('/').Split('/').ToArray()
             //       .Concat(new[] { Path.GetFileName(file) }).ToArray()));

                blob.UploadFromFile(file, FileMode.Open);

            }



        }
    }
}
