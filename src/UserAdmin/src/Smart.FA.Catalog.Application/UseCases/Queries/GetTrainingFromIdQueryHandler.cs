using MediatR;
using Microsoft.Extensions.Logging;
using Smart.FA.Catalog.Application.SeedWork;
using Smart.FA.Catalog.Core.Domain;
using Smart.FA.Catalog.Infrastructure.Persistence;

namespace Smart.FA.Catalog.Application.UseCases.Queries;

public class GetTrainingFromIdQueryHandler: IRequestHandler<GetTrainingFromIdRequest, GetTrainingFromIdResponse>
{
    private readonly ILogger<GetTrainingFromIdQueryHandler> _logger;
    private readonly CatalogContext _catalogContext;

    public GetTrainingFromIdQueryHandler(ILogger<GetTrainingFromIdQueryHandler> logger, CatalogContext catalogContext )
    {
        _logger = logger;
        _catalogContext = catalogContext;
    }

    public async Task<GetTrainingFromIdResponse> Handle(GetTrainingFromIdRequest request, CancellationToken cancellationToken)
    {
        GetTrainingFromIdResponse resp = new();

        try
        {
            var training = await _catalogContext.Trainings.FindAsync(new object?[] { request.TrainingId }, cancellationToken: cancellationToken);
            resp.Training = training!;
            resp.SetSuccess();
        }
        catch (Exception e)
        {
             _logger.LogError("{Exception}", e.ToString());
            throw;
        }

        return resp;

    }
}

public class GetTrainingFromIdRequest: IRequest<GetTrainingFromIdResponse>
{
    public int TrainingId { get; set; }
}

public class GetTrainingFromIdResponse : ResponseBase
{
    public Training? Training { get; set; }
}
