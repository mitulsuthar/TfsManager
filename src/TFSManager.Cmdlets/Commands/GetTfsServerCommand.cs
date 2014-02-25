using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.Win32;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsServer")]
    public class GetTfsServerCommand : Cmdlet
    {
        public List<string> GetTfsServerList()
        { 
            var tfsCol = new List<string>();
            //Remove hard coded version number form this project.
            using (var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\VisualStudio\\12.0\\TeamFoundation\\Instances"))
            {
                foreach (var v in key.GetSubKeyNames())
                {
                    var instance = key.OpenSubKey(v);
                    if (instance != null)
                    {
                        var serverUri = Convert.ToString(instance.GetValue("Uri"));
                       
                        tfsCol.Add(serverUri);
                    }
                    instance.Dispose();
                }
            }
            return tfsCol;
        }
        protected override void ProcessRecord()
        {
            WriteObject(GetTfsServerList(), true);
            base.ProcessRecord();
        }
    }
}
