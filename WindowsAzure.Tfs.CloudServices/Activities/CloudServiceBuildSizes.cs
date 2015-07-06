using Microsoft.TeamFoundation.Build.Workflow.Activities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAzure.Tfs.CloudServices.Activities
{
    /// <summary>
    /// New WindowsAzure.Tfs.CloudServices.Activities.CloudServiceBuildSizes()
    /// </summary>
    [Serializable]
    public class CloudServiceBuildSizes
    {

        [Browsable(true), RefreshProperties(RefreshProperties.All)]
        public StringList ProjectsToBuild
        {
            get
            {
                if (this.m_projectsToBuild == null)
                {
                    this.m_projectsToBuild = new StringList();
                }
                return this.m_projectsToBuild;
            }
            set
            {
                this.m_projectsToBuild = value;
            }
        }

        private StringList m_projectsToBuild { get; set; }
    }
}
