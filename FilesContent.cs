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
            request.AppendLine($"using Roboost.Common.Views;");
            request.AppendLine($"using Roboost.Features.Common;");
            request.AppendLine($"");
            request.AppendLine($"namespace Roboost.Feature.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"");
            request.AppendLine($"public record {FeatureName}Request({FeatureName}RequestViewModel viewModel):IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}RequestHandler : RequestHandlerBase<{FeatureName}Request, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"    public {FeatureName}RequestHandler(RequestHandlerBaseParameters parameters) : base(parameters) ");
            request.AppendLine("    {}");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Request request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine($"        var validationResult = ValidateRequest();");
            request.AppendLine("        if (!validationResult.IsSuccess) { return validationResult; }");
            request.AppendLine($"");
            request.AppendLine($"        var result = await _mediator.Send(new  {FeatureName}Orchestrator(), cancellationToken).ConfigureAwait(false);");
            request.AppendLine($"");
            request.AppendLine($"        return result;");
            request.AppendLine("    }");
            request.AppendLine($"");
            request.AppendLine($"    private RequestResult<{FeatureName}ResponseViewModel> ValidateRequest()");
            request.AppendLine("    {");
            request.AppendLine($"        return RequestResult<{FeatureName}ResponseViewModel>.Success(default, \"\");");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string EndPoint(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using Microsoft.AspNetCore.Mvc;");
            request.AppendLine($"using Roboost.Common.Views;");
            request.AppendLine($"using Roboost.Features.Common;");
            request.AppendLine($"using Roboost.Features.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)};");
            request.AppendLine($"");
            request.AppendLine($"public class {FeatureName}Endpoint : EndpointBase<{FeatureName}RequestViewModel, {FeatureName}ResponseViewModel>");
            request.AppendLine("{");
            request.AppendLine($"    public {FeatureName}Endpoint(EndpointBaseParameters<{FeatureName}RequestViewModel> dependencyCollection) : base(dependencyCollection)");
            request.AppendLine("    {}");
            request.AppendLine($"    public async Task<EndPointResponse<{FeatureName}ResponseViewModel>> Handle({FeatureName}RequestViewModel request, CancellationToken cancellationToken)");
            request.AppendLine("    {");
            request.AppendLine($"        var validationResult = await ValidateRequestAsync(request).ConfigureAwait(false)");
            request.AppendLine("        if (!validationResult.IsSuccess) { return validationResult; }");
            request.AppendLine($"");
            request.AppendLine($"        var result = await _mediator.Send(new  {FeatureName}Request(request), cancellationToken).ConfigureAwait(false);");
            request.AppendLine($"");
            request.AppendLine($"        return result.IsSuccess");
            request.AppendLine($"            ? EndPointResponse<{FeatureName}ResponseViewModel>.Success(result.Data , result.Message);");
            request.AppendLine($"            : EndPointResponse<{FeatureName}ResponseViewModel>.Failure( result.ErrorCode );");

            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string Orchestrator(string ModelName, string FeatureName, bool isQuery)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using Roboost.Common.Views;");
            request.AppendLine($"using Roboost.Features.Common;");
            request.AppendLine($"");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"public record {FeatureName}Orchestrator():IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}OrchestratorHandler : RequestHandlerBase<{FeatureName}Orchestrator, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"    public {FeatureName}OrchestratorHandler(RequestHandlerBaseParameters parameters) : base(parameters) ");
            request.AppendLine("    {}");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Orchestrator request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine($"        var result = await _mediator.Send(new  {FeatureName}{(isQuery ? "Query()" : "Command()")}, cancellationToken).ConfigureAwait(false);");
            request.AppendLine($"        return result;");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }
        public static string Command(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using Roboost.Common.Views;");
            request.AppendLine($"using Roboost.Features.Common;");
            request.AppendLine($"using Roboost.Data;");
            request.AppendLine($"");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)}.Commands;");
            request.AppendLine($"");
            request.AppendLine($"public record {FeatureName}Command():IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}CommandHandler : RequestHandlerBase<{FeatureName}Command, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"    readonly Repository<YouModelHere> _repository; ");

            request.AppendLine($"    public {FeatureName}CommandHandler(RequestHandlerBaseParameters parameters, Repository<YouModelHere> repository) : base(parameters) ");
            request.AppendLine("    { _repository = repository; }");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Command request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string Query(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine($"using Roboost.Common.Views;");
            request.AppendLine($"using Roboost.Features.Common;");
            request.AppendLine($"using Roboost.Data;");
            request.AppendLine($"");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)}.Queries;");
            request.AppendLine($"public record {FeatureName}Query():IRequest<RequestResult<{FeatureName}ResponseViewModel>>;");
            request.AppendLine($"public class {FeatureName}QueryHandler : RequestHandlerBase<{FeatureName}Query, RequestResult<{FeatureName}ResponseViewModel>>");
            request.AppendLine("{");
            request.AppendLine($"    readonly Repository<YouModelHere> _repository; ");

            request.AppendLine($"    public {FeatureName}QueryHandler(RequestHandlerBaseParameters parameters, Repository<YouModelHere> repository) : base(parameters) ");
            request.AppendLine("    { _repository = repository; }");
            request.AppendLine($"    public override async Task<RequestResult<{FeatureName}ResponseViewModel>> Handle({FeatureName}Query request, CancellationToken cancellationToken) ");
            request.AppendLine("    {");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string RequestVM(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();

            request.AppendLine($"using namespace FluentValidation;");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)};");
            request.AppendLine($"public record {FeatureName}RequestViewModel();");
            request.AppendLine($"public class {FeatureName}RequestViewModelValidator : AbstractValidator <{FeatureName}RequestViewModel>");
            request.AppendLine("{");
            request.AppendLine($"    public {FeatureName}RequestViewModelValidator()");
            request.AppendLine("    {");
            request.AppendLine("    }");
            request.AppendLine("}");


            return request.ToString();
        }

        public static string ResponseVM(string ModelName, string FeatureName)
        {
            StringBuilder request = new StringBuilder();

            request.AppendLine($"using Mapster;");
            request.AppendLine($"namespace Roboost.Features.{MakePlural(ModelName)};");
            request.AppendLine($"public record {FeatureName}ResponseViewModel();");
            request.AppendLine($"public class {FeatureName}ResponseViewModelMappingConfig : IRegister");
            request.AppendLine("{");
            request.AppendLine($"    public void Register(TypeAdapterConfig config)");
            request.AppendLine("    {");
            request.AppendLine($"        config.NewConfig<YourModelHere, {FeatureName}ResponseViewModel>();");
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
