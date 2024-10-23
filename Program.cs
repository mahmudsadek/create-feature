using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace create_feature
{
    internal class Program
    {

        static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "foldercreator",
                Description = "Creates a folder or file based on options."
            };

            app.HelpOption("-?|-h|--help");

            var CommandNameOption = app.Option("-c|--command <FOLDERNAME>", "command name to create.", CommandOptionType.SingleValue);
            var QueryNameOption = app.Option("-q|--query <FILENAME>", "query name to create.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string currentDirectory = Directory.GetCurrentDirectory();

                if (CommandNameOption.HasValue())
                {
                    CreateDir(CommandNameOption.Value(), currentDirectory);
                    CreateDir("Commands", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Request", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/");
                    CreateFile(CommandNameOption.Value() + "Orchestrator", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/");
                    CreateFile(CommandNameOption.Value() + "Command", $"{currentDirectory}/{CommandNameOption.Value()}/Commands");
                    CreateFile(CommandNameOption.Value() + "RequestViewModel", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "ResponseViewModel", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Endpoint", $"{currentDirectory}/{CommandNameOption.Value()}/");

                }

                if (QueryNameOption.HasValue())
                {
                    CreateDir(QueryNameOption.Value(), currentDirectory);
                    CreateDir("Commands", $"{currentDirectory}/{QueryNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Request", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/");
                    CreateFile(CommandNameOption.Value() + "Orchestrator", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/");
                    CreateDir("Queries", $"{currentDirectory}/{QueryNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Query", $"{currentDirectory}/{CommandNameOption.Value()}/Queries/");
                    CreateFile(QueryNameOption.Value() + "ResponseViewModel", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(QueryNameOption.Value() + "RequestViewModel", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(QueryNameOption.Value() + "Endpoint", $"{currentDirectory}/{CommandNameOption.Value()}/");

                }
            });

            return app.Execute(args);
        }

        static void CreateDir(string folderName, string currentDirectory)
        {
            string folderPath = Path.Combine(currentDirectory, folderName);
            Directory.CreateDirectory(folderPath);
        }

        static async void CreateFile(string fileName, string currentDirectory, string content = "")
        {
            string FilePath = Path.Combine(currentDirectory, fileName);
            await File.WriteAllTextAsync(FilePath, content);
        }
    }
}
