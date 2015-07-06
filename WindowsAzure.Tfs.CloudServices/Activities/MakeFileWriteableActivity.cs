using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAzure.Tfs.CloudServices.Activities
{
   

    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class MakeFileWriteableActivity : CodeActivity
    {

        [RequiredArgument]
        public InArgument<string> FilePath
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            String filePath = Path.Combine(Path.GetDirectoryName(FilePath.Get(context)), "ServiceDefinition.csdef");
     
            //add exception handling 

            FileAttributes fileAttributes = File.GetAttributes(filePath);
            File.SetAttributes(filePath, fileAttributes & ~FileAttributes.ReadOnly);
            
        }

    }
}
