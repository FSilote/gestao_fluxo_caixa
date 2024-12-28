namespace CodeChallenger.Sdk.WebApi.AppStart.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyLoader
    {
        public static void LoadAssemblies()
        {
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,
                "*amx*.dll", new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive }).ToList();

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name!.Contains("amx", StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            var toLoad = referencedPaths
                .Where(r => !loadedAssemblies.Select(a => a.Location).Contains(r, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            toLoad.ForEach(path => AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
        }

        public static IList<Assembly> GetAssemblies()
        {
            LoadAssemblies();

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name!.Contains("amx", StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }
    }
}
