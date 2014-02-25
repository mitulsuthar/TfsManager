using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.Win32;
using Microsoft.TeamFoundation.Client;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get","TfsProjectCollection")]
    public class GetTfsProjectCollectionCommand : Cmdlet
    {
       [Parameter(Position = 0, Mandatory = true)]
       public string ServerUri { get; set; }

       protected override void ProcessRecord()
       {
           var uri = new Uri(ServerUri);
           TfsConfigurationServer configurationServer =
                  TfsConfigurationServerFactory.GetConfigurationServer(uri);
           
           var tpcService = configurationServer.GetService<ITeamProjectCollectionService>();
           
           
           WriteObject(tpcService.GetCollections(), true);
           
           base.ProcessRecord();
        }
    }
    
}
