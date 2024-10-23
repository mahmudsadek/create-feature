using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace create_feature
{
    internal class FilesContent
    {
        public static string Request(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using namespace Roboost.Feature.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"public record {FeatureName}Request({FeatureName}RequestViewModel viewModel):IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}RequestHandler : RequestHandlerBase<{FeatureName}Request, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"   public {FeatureName}RequestHandler(RequestHandlerBaseParameters parameters) : base(parameters) ");
            request.AppendLine("    {}");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Request request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine($"        var validationResult = ValidateRequest();");
            request.AppendLine("        if (!validationResult.IsSuccess) { return validationResult; }");
            request.AppendLine($"        var result = await _mediator.Send(new  {FeatureName}Orchestrator());");
            request.AppendLine($"        return result;");
            request.AppendLine("    }");
            request.AppendLine($"        private RequestResult<{FeatureName}ResponseViewModel> ValidateRequest()");
            request.AppendLine("    {");
            request.AppendLine($"            return RequestResult<{FeatureName}ResponseViewModel>.Success(default, \"\");");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string Orchestrator(string ModelName, string FeatureName, bool isQuery)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using namespace Roboost.Feature.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"public record {FeatureName}Orchestrator():IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}OrchestratorHandler : RequestHandlerBase<{FeatureName}Request, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"   public {FeatureName}OrchestratorHandler(RequestHandlerBaseParameters parameters) : base(parameters) ");
            request.AppendLine("    {}");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Request request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine($"        var result = await _mediator.Send(new  {FeatureName}{(isQuery ? "Query" : "Command")});");
            request.AppendLine($"        return result;");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        static string MakePlural(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            if (word.EndsWith("y") && word.Length > 1 && IsConsonant(word[word.Length - 2]))
            {
                return word.Substring(0, word.Length - 1) + "ies";
            }
            else if (word.EndsWith("s") || word.EndsWith("x") || word.EndsWith("z") ||
                     word.EndsWith("ch") || word.EndsWith("sh"))
            {
                return word + "es";
            }
            else
            {
                return word + "s";
            }
        }
        static bool IsConsonant(char c)
        {
            return !"aeiou".Contains(char.ToLower(c));
        }
    }
}
