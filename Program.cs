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
                    CreateFolder(CommandNameOption.Value(), currentDirectory);
                    CreateFolder("Commands", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Request", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Orchestrator", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Command", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "RequestViewModel", currentDirectory);
                    CreateFile(CommandNameOption.Value() + "ResponseViewModel", currentDirectory);
                    CreateFile(CommandNameOption.Value() + "Endpoint", currentDirectory);

                }

                if (QueryNameOption.HasValue())
                {
                    CreateFolder(QueryNameOption.Value(), currentDirectory);
                    CreateFolder("Commands", $"{currentDirectory}/{QueryNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Request", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Orchestrator", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFolder("Queries", $"{currentDirectory}/{QueryNameOption.Value()}/");
                    CreateFile(CommandNameOption.Value() + "Query", $"{currentDirectory}/{CommandNameOption.Value()}/");
                    CreateFile(QueryNameOption.Value() + "ResponseViewModel", currentDirectory);
                    CreateFile(QueryNameOption.Value() + "RequestViewModel", currentDirectory);
                    CreateFile(QueryNameOption.Value() + "Endpoint", currentDirectory);

                }
            });

            return app.Execute(args);
        }

        static void CreateFolder(string folderName, string currentDirectory)
        {
            string folderPath = Path.Combine(currentDirectory, folderName);
            Directory.CreateDirectory(folderPath);
        }

        static async void CreateFile(string fileName, string currentDirectory, string content = "")
        {
            string FilePath = Path.Combine(currentDirectory, fileName);
            await File.WriteAllTextAsync(FilePath , content);
        }
    }
}
