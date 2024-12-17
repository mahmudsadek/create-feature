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
            var QueryNameOption = app.Option("-q|--query <FOLDERNAME>", "query name to create.", CommandOptionType.SingleValue);
            var CreateCommandNameOption = app.Option("-C|--Command <FOLDERNAME>", "query name to create.", CommandOptionType.SingleValue);
            var CreateQueryNameOption = app.Option("-Q|--Query <FOLDERNAME>", "query name to create.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                string dirName = dirInfo.Name;
                if (CommandNameOption.HasValue())
                {
                    try
                    {
                        CreateDir(CommandNameOption.Value(), currentDirectory);
                        CreateDir("Commands", $"{currentDirectory}/{CommandNameOption.Value()}/");
                        CreateFile(CommandNameOption.Value() + "Request.cs", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/", FilesContent.Request(dirName, CommandNameOption.Value()));
                        CreateFile(CommandNameOption.Value() + "Orchestrator.cs", $"{currentDirectory}/{CommandNameOption.Value()}/Commands/", FilesContent.Orchestrator(dirName, CommandNameOption.Value(), false));
                        CreateFile(CommandNameOption.Value() + "Command.cs", $"{currentDirectory}/{CommandNameOption.Value()}/Commands", FilesContent.Command(dirName, CommandNameOption.Value()));
                        CreateFile(CommandNameOption.Value() + "RequestViewModel.cs", $"{currentDirectory}/{CommandNameOption.Value()}/", FilesContent.RequestVM(dirName, CommandNameOption.Value()));
                        CreateFile(CommandNameOption.Value() + "ResponseViewModel.cs", $"{currentDirectory}/{CommandNameOption.Value()}/", FilesContent.ResponseVM(dirName, CommandNameOption.Value()));
                        CreateFile(CommandNameOption.Value() + "Endpoint.cs", $"{currentDirectory}/{CommandNameOption.Value()}/", FilesContent.EndPoint(dirName, CommandNameOption.Value()));
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }

                if (QueryNameOption.HasValue())
                {
                    try
                    {
                        CreateDir(QueryNameOption.Value(), currentDirectory);
                        CreateDir("Commands", $"{currentDirectory}/{QueryNameOption.Value()}/");
                        CreateDir("Queries", $"{currentDirectory}/{QueryNameOption.Value()}/");
                        CreateFile(QueryNameOption.Value() + "Request.cs", $"{currentDirectory}/{QueryNameOption.Value()}/Commands/", FilesContent.Request(dirName, QueryNameOption.Value()));
                        CreateFile(QueryNameOption.Value() + "Orchestrator.cs", $"{currentDirectory}/{QueryNameOption.Value()}/Commands/", FilesContent.Orchestrator(dirName, QueryNameOption.Value(), true));
                        CreateFile(QueryNameOption.Value() + "Query.cs", $"{currentDirectory}/{QueryNameOption.Value()}/Queries/", FilesContent.Query(dirName, QueryNameOption.Value()));
                        CreateFile(QueryNameOption.Value() + "ResponseViewModel.cs", $"{currentDirectory}/{QueryNameOption.Value()}/", FilesContent.ResponseVM(dirName, QueryNameOption.Value()));
                        CreateFile(QueryNameOption.Value() + "RequestViewModel.cs", $"{currentDirectory}/{QueryNameOption.Value()}/", FilesContent.RequestVM(dirName, QueryNameOption.Value()));
                        CreateFile(QueryNameOption.Value() + "Endpoint.cs", $"{currentDirectory}/{QueryNameOption.Value()}/", FilesContent.EndPoint(dirName, QueryNameOption.Value()));
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }

                if (CreateQueryNameOption.HasValue())
                {
                    try
                    {
                        if(!Directory.Exists($"{currentDirectory}/Queries"))
                            CreateDir("Queries", currentDirectory);
                        
                        CreateFile(CreateQueryNameOption.Value() + "Query.cs", $"{currentDirectory}/Queries/", FilesContent.Query(dirName, CreateQueryNameOption.Value()));
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                
                if (CreateCommandNameOption.HasValue())
                {
                    try
                    {
                        if(!Directory.Exists($"{currentDirectory}/Commands"))
                            CreateDir("Commands", currentDirectory);
                        
                        CreateFile(CreateCommandNameOption.Value() + "Command.cs", $"{currentDirectory}/Command/", FilesContent.Command(dirName, CreateCommandNameOption.Value()));

                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
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
