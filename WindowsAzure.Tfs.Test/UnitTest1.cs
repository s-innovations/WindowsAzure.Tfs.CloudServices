using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace WindowsAzure.Tfs.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = "ExtraSmall";
            var doc = XDocument.Load("testFile.xml");
            XNamespace ab = "http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceDefinition";
            foreach (var element in doc.Root.Elements(ab + "WebRole"))
            {
                element.SetAttributeValue("vmsize", text);
            }
            foreach (var element in doc.Root.Elements(ab + "WorkerRole"))
            {
                FixSize(ab, element, "AscendLocalStorage", "1020");
            }
         
            //  doc.Root.Element(ab + "WebRole").SetAttributeValue("vmsize", text);

           // doc.Save(filePath);
        }
        public void FixSize(XNamespace ab, XElement role, string name, string size)
        {
            foreach (var local in role.Elements(ab + "LocalResources"))
                foreach (var storage in local.Elements(ab + "LocalStorage"))
                    if (storage.Attribute("name").Value == name)
                        storage.SetAttributeValue("sizeInMB", size);
        }
    }
}
