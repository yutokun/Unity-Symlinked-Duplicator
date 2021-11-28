using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace UnitySymlinkedDuplicator;

internal static class Program
{
    static string? original, symlinked;

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

        original = args.Aggregate((result, next) => $"{result} {next}");

        if (!Directory.Exists(Path.Combine(original, "Assets")) || !Directory.Exists(Path.Combine(original, "ProjectSettings")))
        {
            Console.WriteLine("This is not an Unity Project.");
            Console.ReadKey();
            return;
        }

        if (!IsAdmin)
        {
            RunAsAdmin(args);
            return;
        }

        symlinked = $"{original} Symlinked";
        Directory.CreateDirectory(symlinked);
        Link("Assets");
        Link("Library");
        Link("ProjectSettings");
        Link("Packages");

        Console.WriteLine("Symlinked project has been created.");
        Console.ReadKey();
    }

    static void Link(string directory)
    {
        var path = Path.Combine(symlinked!, directory);
        var target = Path.Combine(original!, directory);
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

    static void RunAsAdmin(string[] args)
    {
        var exe = Environment.GetCommandLineArgs()[0].Replace(".dll", ".exe");
        var startInfo = new ProcessStartInfo(exe, args[0])
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
