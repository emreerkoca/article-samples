using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySalesCLIApplication
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            var fileOption = new Option<FileInfo?>(
                name: "--file",
                description: "The file to read and display on the console.");

            var rootCommand = new RootCommand("Sample app for System.CommandLine");
            rootCommand.AddOption(fileOption);

            rootCommand.SetHandler((file) =>
            {
                ReadFile(file!);
            },
                fileOption);

            return await rootCommand.InvokeAsync(args);
        }

        static void ReadFile(FileInfo file)
        {
            File.ReadLines(file.FullName).ToList()
                .ForEach(line => Console.WriteLine(line));
        }
    }
}
