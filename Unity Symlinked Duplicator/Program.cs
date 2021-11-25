using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

namespace UnitySymlinkedDuplicator;

internal static class Program
{
    static string? original, duplicated;

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Drop an Unity Project Folder to this app.");
            Console.ReadKey();
            return;
        }

        original = args[0];

        if (!Directory.Exists(Path.Combine(original, "Assets")) || !Directory.Exists(Path.Combine(original, "ProjectSettings")))
        {
            Console.WriteLine("This is not an Unity Project.");
            Console.ReadKey();
            return;
        }

        if (!IsAdmin) RunAsAdmin(args);

        duplicated = $"{original} Duplicated";
        Directory.CreateDirectory(duplicated);
        Link("Assets");
        Link("Library");
        Link("ProjectSettings");
        Link("Packages");
    }

    static void Link(string directory)
    {
        var path = Path.Combine(duplicated!, directory);
        var target = Path.Combine(original!, directory);
        if (!Directory.Exists(target)) return;
        Directory.CreateSymbolicLink(path, target);
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
            Console.WriteLine(e);
            throw;
        }
    }
}
