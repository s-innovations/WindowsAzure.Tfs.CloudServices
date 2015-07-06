using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow.Tracking;

namespace WindowsAzure.Tfs.CloudServices.Activities
{
    public sealed class UpdateRoleSizeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        [RequiredArgument]
        public InArgument<string> Size { get; set; }

        [RequiredArgument]
        public InArgument<string> FilePath { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Size);
            String filePath = Path.Combine(Path.GetDirectoryName(FilePath.Get(context)), "ServiceDefinition.csdef");
            context.TrackBuildWarning("File Path: " + filePath, BuildMessageImportance.High);
//            context.TrackBuildWarning(string.Join("\n", Directory.GetFiles(Path.GetDirectoryName(FilePath.Get(context)), "*.csdef", SearchOption.AllDirectories)), BuildMessageImportance.High);

            var doc = XDocument.Load(filePath);
            XNamespace ab = "http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition";
            foreach(var element in doc.Root.Elements(ab + "WebRole"))
                element.SetAttributeValue("vmsize", text);
            foreach (var element in doc.Root.Elements(ab + "WorkerRole"))
                element.SetAttributeValue("vmsize", text);
          //  doc.Root.Element(ab + "WebRole").SetAttributeValue("vmsize", text);
            
            doc.Save(filePath);

        }
    }
}
