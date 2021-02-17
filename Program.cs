using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace PuttyWrapper
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (!Directory.Exists(Constants.DataPath))
            {
                Directory.CreateDirectory(Constants.DataPath);
            }

            var parserResult = Parser.Default.ParseArguments<Options>(args);
            parserResult.WithParsed(o =>
            {
                if (o.Connect || !o.Delete && !o.New && !o.Print && !string.IsNullOrWhiteSpace(o.Name))
                {
                    var dataFilePath = Path.Join(Constants.DataPath, o.Name);

                    if (!File.Exists(dataFilePath))
                    {
                        Console.WriteLine($"No file found for {o.Name}, expected it at {dataFilePath}");
                        return;
                    }

                    var startProcessInfo = File.ReadAllText(dataFilePath).Split(' ', 2);

                    Console.WriteLine("Connecting...");

                    Process.Start(startProcessInfo[0], startProcessInfo[1]);
                }
                else if (o.Delete)
                {
                    var dataFilePath = Path.Join(Constants.DataPath, o.Name);

                    if (!File.Exists(dataFilePath))
                    {
                        Console.WriteLine($"No file found for {o.Name}, expected it at {dataFilePath}");
                        return;
                    }

                    File.Delete(dataFilePath);

                    Console.WriteLine("Deleted");
                }
                else if (o.New)
                {
                    var dataFilePath = Path.Join(Constants.DataPath, o.Name);

                    if (File.Exists(dataFilePath))
                    {
                        Console.WriteLine($"File exists found for {o.Name}, at {dataFilePath} please use another name");
                        return;
                    }

                    var file = File.Create(dataFilePath);
                    using var binaryWriter = new BinaryWriter(file);
                    binaryWriter.Write($"putty.exe -ssh {o.User}@{o.IpAddress} -pw {o.Password}");
                    Console.WriteLine($"Saved as {o.Name}");
                }
                else if (o.Print)
                {
                    var dataFilePath = Path.Join(Constants.DataPath, o.Name);
                    Console.WriteLine("Saved configs are located at: " + dataFilePath);
                    var configurations = Directory.GetFiles(dataFilePath).Select(x => x.Replace(dataFilePath, ""));

                    foreach (var configuration in configurations)
                    {
                        Console.WriteLine(" - " + configuration);
                    }
                }
                else
                {
                    var helpText = HelpText.AutoBuild(parserResult, h => h, e => e);
                    Console.WriteLine(helpText);
                }
            });
        }
    }
}
