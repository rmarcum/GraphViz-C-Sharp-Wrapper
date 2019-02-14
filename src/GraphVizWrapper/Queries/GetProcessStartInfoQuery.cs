// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetProcessStartInfoQuery.cs" company="Jamie Dixon Ltd">
//   Jamie Dixon
// </copyright>
// <summary>
//   Defines the GetProcessStartInfoQuery type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace GraphVizWrapper.Queries
{
    public class GetProcessStartInfoQuery : IGetProcessStartInfoQuery
    {
        public System.Diagnostics.ProcessStartInfo Invoke(IProcessStartInfoWrapper processStartInfo)
        {
            return new System.Diagnostics.ProcessStartInfo
                       {
                           WorkingDirectory = Path.GetDirectoryName(processStartInfo.FileName),
                           FileName = '"' + processStartInfo.FileName + '"',
                           Arguments = processStartInfo.Arguments,
                           RedirectStandardInput = processStartInfo.RedirectStandardInput,
                           RedirectStandardOutput = processStartInfo.RedirectStandardOutput,
                           RedirectStandardError = processStartInfo.RedirectStandardError,
                           UseShellExecute = processStartInfo.UseShellExecute,
                           CreateNoWindow = processStartInfo.CreateNoWindow
                       };
        }
    }
}
