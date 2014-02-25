using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.PowerShell.Commands;
using TFSManager.Cmdlets.Lib;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsBuildServer")]
    public class GetTfsBuildServerCommand : Cmdlet
    {

        private string _projectCollectionUri;
        [Parameter(ParameterSetName = "ParamByUri")]
        public string ProjectCollectionUri
        {
            get { return _projectCollectionUri; }
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    _projectCollectionUri = value;
                }
            }
        }
     
        private string _serverUri;

        protected override void ProcessRecord()
        {
            IBuildServer buildserver;
            try
            {
                if (_projectCollectionUri != null)
                {
                    var uri = new Uri(_projectCollectionUri);
                    var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
                    buildserver = tpc.GetService<IBuildServer>();
                }
                else
                {
                   var x = new SessionState();
                   var currentDirectory = x.Path.CurrentLocation.Path;
                   buildserver = Core.GetBuildServerByLocalPath(currentDirectory);
                }
                WriteObject(buildserver);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
            base.ProcessRecord();

        }
    }
}
