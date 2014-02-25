using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using TFSManager.Cmdlets.Lib;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsPendingChanges")]
    public class GetTfsPendingChangesCommand : Cmdlet
    {
        //private string _projectCollectionUri;

        //[AllowNull]
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
        protected override void ProcessRecord()
        {
            PendingChange[] pc;
            try
            {
                //if (_projectCollectionUri != null)
                //{
                //var uri = new Uri(_projectCollectionUri);
                //var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
                //var vcs = tpc.GetService<VersionControlServer>();
                //tpc.ConfigurationServer.Connect(ConnectOptions.IncludeServices);
                //tpc.ConfigurationServer.Authenticate();
                
                ///builds = buildserver.QueryBuilds(ProjectName);
                //}

                var x = new SessionState();
                var currentDirectory = x.Path.CurrentLocation.Path;
                var wk = Core.GetWorkspaceByLocalPath(currentDirectory);
                pc = wk.GetPendingChanges();
                if (pc == null)
                {
                    throw new Exception("No mapped workspace could be found at this path");
                }
                WriteObject(pc, true);
            }
            catch (Exception ex)
            {
                WriteObject(ex, true);
            }
            base.ProcessRecord();
        }
    }
}
