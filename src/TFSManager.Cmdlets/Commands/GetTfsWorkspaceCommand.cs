using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using TFSManager.Cmdlets.Lib;

namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsWorkspace")]
    public class GetTfsWorkspaceCommand : Cmdlet
    {
        //private string _projectCollectionUri;
        //[Parameter(ParameterSetName = "ParamByUri")]
        //public string ProjectCollectionUri
        //{
        //    get { return _projectCollectionUri; }
        //    set
        //    {
        //        if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
        //        {
        //            _projectCollectionUri = value;
        //        }
        //    }
        //}
        //[Parameter(ParameterSetName = "ParamByUri")]
        //public string ProjectName { get; set; }

        protected override void ProcessRecord()
        {
            Workspace workspace;
            try
            {
                var x = new SessionState();
                var currentDirectory = x.Path.CurrentLocation.Path;
                workspace = Core.GetWorkspaceByLocalPath(currentDirectory);
                if (workspace == null)
                {
                    throw new Exception("No mapped workspace could be found at this path. Navigate to a mapped path to get a workspace.");
                }
                WriteObject(workspace, true);
            }
            catch (Exception ex)
            {
                WriteObject(ex, true);
            }
            base.ProcessRecord();
        }
    }
}
