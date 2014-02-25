using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using Microsoft.TeamFoundation.VersionControl.Client;
using TFSManager.Cmdlets.Commands;
namespace TFSManager.Cmdlets.Lib
{
    public static class Core
    {
        public static string GetDefaultServer()
        {
            var getTfsDefaultServer = new GetTfsDefaultServerCommand();
            var output = getTfsDefaultServer.Invoke<string>();
            string serverUri = "";
            foreach (var x in output)
            {
                Console.WriteLine("default server - {0}", x);
                if (Uri.IsWellFormedUriString(x, UriKind.Absolute))
                    serverUri = x;
                else
                    serverUri = "invalid url";
            }
            return serverUri;
        }

        public static TfsTeamProjectCollection GetTeamProjectCollectionByLocalPath(string localPath)
        {
            var wkInfo = Workstation.Current.GetLocalWorkspaceInfo(localPath);
            var serverUri = wkInfo.ServerUri;
            var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(serverUri);
            return tpc;
        }

        public static Workspace GetWorkspaceByLocalPath(string localPath)
        {
            var wkInfo = Workstation.Current.GetLocalWorkspaceInfo(localPath);
            var serverUri = wkInfo.ServerUri;
            var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(serverUri);
            var wksp = wkInfo.GetWorkspace(tpc);
            return wksp;
        }
        public static TeamProject GetTeamProjectByLocalPath(string localPath)
        {
            var wksp = GetWorkspaceByLocalPath(localPath);
            var project = wksp.GetTeamProjectForLocalPath(localPath);
            return project;
        }
        public static IBuildServer GetBuildServerByLocalPath(string localPath)
        {
            var wkInfo = Workstation.Current.GetLocalWorkspaceInfo(localPath);
            var serverUri = wkInfo.ServerUri;
            var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(serverUri);
            var buildserver = tpc.GetService<IBuildServer>();
            return buildserver;
        }
        public static IBuildDefinition[] GetBuildDefinitionsByLocalPath(string localPath)
        {
            var wkInfo = Workstation.Current.GetLocalWorkspaceInfo(localPath);
            var serverUri = wkInfo.ServerUri;
            var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(serverUri);
            var buildserver = tpc.GetService<IBuildServer>();
            var wksp = wkInfo.GetWorkspace(tpc);
            var project = wksp.GetTeamProjectForLocalPath(localPath);
            var buildfinitions = buildserver.QueryBuildDefinitions(project.Name);
            return buildfinitions;
        }

        public static IBuildDetail[] GetBuildsByLocalPath(string localPath)
        {
            var wkInfo = Workstation.Current.GetLocalWorkspaceInfo(localPath);
            var serverUri = wkInfo.ServerUri;
            var tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(serverUri);
            var wksp = wkInfo.GetWorkspace(tpc);
            var project = wksp.GetTeamProjectForLocalPath(localPath);
            var buildserver = tpc.GetService<IBuildServer>();
            var builds = buildserver.QueryBuilds(project.Name);
            return builds;
        }
    }

}
