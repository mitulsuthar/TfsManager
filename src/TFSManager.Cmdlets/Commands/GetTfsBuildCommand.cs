using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using TFSManager.Cmdlets.Lib;

namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsBuild")]
    public class GetTfsBuildCommand : Cmdlet
    {
        private string _projectCollectionUri;

        [AllowNull]
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
        [Parameter(ParameterSetName = "ParamByUri")]
        public string ProjectName { get; set; }

        [Parameter()]
        public string Status { get; set; }

        protected override void ProcessRecord()
        {
            IBuildDetail[] builds;
            try
            {
                if (_projectCollectionUri != null)
                {
                    var uri = new Uri(_projectCollectionUri);
                    var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
                    var buildserver = tpc.GetService<IBuildServer>();
                    builds = buildserver.QueryBuilds(ProjectName);
                }
                else
                {
                    var x = new SessionState();
                    var currentDirectory = x.Path.CurrentLocation.Path;
                    builds = Core.GetBuildsByLocalPath(currentDirectory);
                }
                if (builds == null)
                {
                    throw new Exception("No mapped workspace could be found at this path");
                }
                else
                {
                    if (this.Status != null)
                    {
                        var filteredBuilds = builds.Where(x => x.Status.ToString() == Status).ToList();
                        WriteObject(filteredBuilds,true);
                    }
                    else
                    {
                        WriteObject(builds, true);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteObject(ex, true);
            }
            base.ProcessRecord();
        }
    }
}
