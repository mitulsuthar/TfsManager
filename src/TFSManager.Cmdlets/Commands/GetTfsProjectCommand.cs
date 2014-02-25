using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using TFSManager.Cmdlets.Lib;

namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsProject")]
    public class GetTfsProjectCommand : Cmdlet
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
        private string _projectCollectionName;
        [Parameter(ParameterSetName = "ParamByName", Mandatory = true)]
        public string ProjectCollectionName
        {
            get { return _projectCollectionName; }
            set { _projectCollectionName = value; }
        }

        [AllowNull]
        [Parameter(ParameterSetName = "ParamByName")]
        public string ServerUri
        {
            get
            {
                return _serverUri;
            }
            set { _serverUri = value; }
        }


        private string _serverUri;

        protected override void ProcessRecord()
        {
            try
            {
                if (_projectCollectionUri == null)
                {
                    if (_serverUri == null)
                    {

                        if (Core.GetDefaultServer() != "invalid url")
                        {
                            _serverUri = Core.GetDefaultServer();
                            _projectCollectionUri = String.Format("{0}/{1}", _serverUri, _projectCollectionName);
                        }
                        else
                        {
                            Console.WriteLine(
                                "ServerUri parameter can be null only if a default Tfs Server is set. See Get-TfsDefaultServer, Set-TfsDefaultServer to setup a default Tfs Server.");

                        }
                    }
                }
                var uri = new Uri(_projectCollectionUri);
                var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
                var workItemStore = tpc.GetService<WorkItemStore>();
                WriteObject(workItemStore.Projects, true);
                base.ProcessRecord();
            }
            catch (Exception ex)
            {
                WriteObject(ex, true);
            }
        }
    }
}
