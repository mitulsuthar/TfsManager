using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.Win32;
namespace TFSManager.Cmdlets.Commands
{
    [Cmdlet("Get", "TfsDefaultServer")]
    public class GetTfsDefaultServerCommand : Cmdlet
    {   
        protected override void ProcessRecord()
        {
            using (var key = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                //Check if the TfsManagerModule Keys exists or not
                if (key.OpenSubKey("TfsManagerModule", true) == null)
                {
                    Console.WriteLine(
                        "No default Tfs server has been configured. Use Set-TfsDefaultServer cmdlet to set the Default Tfs Server");
                }
                else
                {
                    using (var k = key.OpenSubKey("TfsManagerModule", true))
                    {
                        var instance = k.OpenSubKey("DefaultTfsInstance", true);
                        var uriValue = instance.GetValue("Uri");
                        WriteObject(uriValue.ToString());
                        instance.Dispose();
                    }
                }
            }
            base.ProcessRecord();
        }
    }
}
