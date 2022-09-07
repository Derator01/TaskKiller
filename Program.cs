using System.Diagnostics;

namespace TaskKiller;

internal static class Program
{
    private static string appName = "CMD.exe";
    private static string vitalPart = "/C ";

    private static void Main(string[] args)
    {
        for (int i = 0; i < int.Parse(args[0]); i++)
        {
            Process cmd = new();
            cmd.StartInfo.FileName = appName;
            cmd.StartInfo.Arguments = vitalPart + "tasklist";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.Start();

            Thread.Sleep(1000);
            
            string output = cmd.StandardOutput.ReadToEnd();
            cmd.Kill();

            if (output.Contains(args[1]))
            {
                Process.Start(appName, vitalPart + "taskkill /f /im " + args[1]);
            }

            Thread.Sleep(100);
        }
    }
}