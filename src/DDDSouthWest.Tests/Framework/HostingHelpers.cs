using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace DDDSouthWest.Tests.Framework
{
    public static class HostingHelpers
    {
        /// <summary>
        ///     Gets the full path to the target project path based on its startup class.
        /// </summary>
        /// <typeparam name="TStartup">The startup class.</typeparam>
        /// <returns>
        ///     The full path to the target project.
        /// </returns>
        /// <remarks>
        ///     Adapted from <c>https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing</c>.
        /// </remarks>
        public static string GetProjectPath<TStartup>()
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var websitePath = Path.Combine("src");

            return GetProjectPath(websitePath, startupAssembly);
        }

        /// <summary>
        ///     Gets the full path to the target project path that we wish to test.
        /// </summary>
        /// <param name="solutionRelativePath">
        ///     The parent directory of the target project.
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <returns>
        ///     The full path to the target project.
        /// </returns>
        /// <remarks>
        ///     Adapted from <c>https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing</c>.
        /// </remarks>
        public static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var solutionName = "api_consumercommpreference.sln";
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the directory containing the solution file
            var directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                var fileName = Path.Combine(directoryInfo.FullName, solutionName);

                var solutionFileInfo = new FileInfo(fileName);

                if (solutionFileInfo.Exists)
                {
                    var contentRoot = Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName);
                    return Path.GetFullPath(contentRoot);
                }

                directoryInfo = directoryInfo.Parent;
            } while (directoryInfo.Parent != null);

            throw new FileNotFoundException(
                $"Solution file could not be located using application root '{applicationBasePath}'.", solutionName);
        }
    }
}