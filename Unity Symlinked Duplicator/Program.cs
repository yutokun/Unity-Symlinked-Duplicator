using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace UnitySymlinkedDuplicator;

internal static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Unity Symlinked Duplicator v{Assembly.GetEntryAssembly().GetName().Version.ToString(3)}");
        Console.WriteLine("");

        if (args.Length == 0)
        {
            Console.WriteLine("Drop an Unity Project Folder to this app.");
            Console.ReadKey();
            return;
        }

        var combinedArgument = args.Aggregate((result, next) => $"{result} {next}");
        var paths = ExtractPaths(combinedArgument);

        foreach (var path in paths)
        {
            if (!Directory.Exists(Path.Combine(path, "Assets")) || !Directory.Exists(Path.Combine(path, "ProjectSettings")))
            {
                Console.WriteLine($"Not an Unity Project: {path}");
                continue;
            }

            if (!IsAdmin)
            {
                RunAsAdmin(combinedArgument);
                return;
            }

            var symlinked = $"{path} Symlinked";
            if (Directory.Exists(symlinked))
            {
                var directories = Directory.GetDirectories(Directory.GetParent(symlinked).FullName);
                var maxNumber = directories
                                .Where(d => d.Contains(symlinked))
                                .Select(d =>
                                {
                                    var number = Regex.Match(d, @"[0-9]+").Groups[0];
                                    return int.Parse(number.Success ? number.Value : "0");
                                })
                                .Max();
                symlinked = $"{symlinked} {maxNumber + 1}";
            }

            Directory.CreateDirectory(symlinked);
            Link(path, symlinked, "Assets");
            Link(path, symlinked, "Library");
            Link(path, symlinked, "ProjectSettings");
            Link(path, symlinked, "Packages");

            Console.WriteLine($"Symlinked: {symlinked}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to finish...");
        Console.ReadKey();
    }

    static List<string> ExtractPaths(string original)
    {
        var pathStarts = new List<int>();
        while (true)
        {
            var searchStartAt = pathStarts.Count == 0 ? 0 : pathStarts.Last() + 2;
            var startsAt = original.IndexOf(":", searchStartAt) - 1;
            if (startsAt == -2)
            {
                pathStarts.Add(original.Length + 1);
                break;
            }

            pathStarts.Add(startsAt);
        }

        var paths = new List<string>();
        for (var i = 0; i < pathStarts.Count - 1; i++)
        {
            var length = pathStarts[i + 1] - pathStarts[i] - 1;
            paths.Add(original.Substring(pathStarts[i], length));
        }

        return paths;
    }

    static void Link(string original, string symlinked, string directory)
    {
        var path = Path.Combine(symlinked, directory);
        var target = Path.Combine(original, directory);
        if (!Directory.Exists(target)) return;
        try
        {
            Directory.CreateSymbolicLink(path, target);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
            throw;
        }
    }

    static bool IsAdmin
    {
        get
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    static void RunAsAdmin(string argument)
    {
        var exe = Environment.GetCommandLineArgs()[0].Replace(".dll", ".exe");
        var startInfo = new ProcessStartInfo(exe, argument)
        {
            UseShellExecute = true,
            Verb = "runas"
        };

        try
        {
            Process.Start(startInfo);
        }
        catch (Win32Exception e)
        {
            Console.WriteLine("Could not get administrator privileges.");
            Console.ReadKey();
        }
    }
}
