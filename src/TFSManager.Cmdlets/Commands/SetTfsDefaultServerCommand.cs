using System.Collections.Generic;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.Win32;
using System;
using System.Management.Automation;

namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Set", "TfsDefaultServer")]
    public class SetTfsDefaultServerCommand : Cmdlet
    {
        private string _serverUri;
        [Parameter(Position = 0, Mandatory = false)]
        public string ServerUri
        {
            get { return _serverUri; }
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    _serverUri = value;
                }
            }
        }
        protected override void ProcessRecord()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey("Software", true))
                {
                    //Check if the TfsManagerModule Keys exists or not
                    if (key.OpenSubKey("TfsManagerModule", true) == null)
                    {
                        Console.WriteLine("No default TFS server set.");
                        using (var tfsSettingKey = key.CreateSubKey("TfsManagerModule",
                                RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {
                            var defaultTfsInstance = tfsSettingKey.CreateSubKey("DefaultTfsInstance",
                                RegistryKeyPermissionCheck.ReadWriteSubTree);
                            Console.WriteLine("Creating new tfs default");
                            defaultTfsInstance.SetValue("Uri", _serverUri);
                            Console.WriteLine("New Default Value - {0}", _serverUri);
                            defaultTfsInstance.Dispose();
                        }
                    }
                    else
                    {   
                        using (var k = key.OpenSubKey("TfsManagerModule", true))
                        {
                            var instance = k.OpenSubKey("DefaultTfsInstance", true);
                            var uriValue = instance.GetValue("Uri");
                            Console.WriteLine("Current default value - {0}",uriValue.ToString());
                            Console.WriteLine("Changing current tfs default");
                            instance.SetValue("Uri",_serverUri);
                            Console.WriteLine("New default value - {0}", _serverUri);
                            instance.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteObject(ex);
            }
            //WriteObject(workItemStore.Projects, true);
            base.ProcessRecord();
        }
    }
}
