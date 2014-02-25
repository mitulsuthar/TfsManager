using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using TFSManager.Cmdlets.Lib;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsBuildDefinition")]
    public class GetTfsBuildDefinitionCommand : Cmdlet
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
        
        [Parameter(ParameterSetName = "ParamByUri")]
        public string ProjectName { get; set; }


        protected override void ProcessRecord()
        {

            IBuildDefinition[] builddefinitions;
            try
            {
                if (_projectCollectionUri != null)
                {
                    var uri = new Uri(_projectCollectionUri);
                    var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
                    var buildserver = tpc.GetService<IBuildServer>();
                    builddefinitions = buildserver.QueryBuildDefinitions(ProjectName);
                }
                else
                {
                    var x = new SessionState();
                    var currentDirectory = x.Path.CurrentLocation.Path;
                    builddefinitions = Core.GetBuildDefinitionsByLocalPath(currentDirectory);
                    if (builddefinitions == null)
                    {
                        throw new Exception("No mapped workspace could be found at this path");
                    }
                }
                WriteObject(builddefinitions, true);
            }
            catch (Exception ex)
            {
                WriteObject(ex, true);
            }
            base.ProcessRecord();
        }
    }
}