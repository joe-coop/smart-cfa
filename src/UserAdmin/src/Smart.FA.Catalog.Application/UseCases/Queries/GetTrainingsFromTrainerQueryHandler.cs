using MediatR;
using Microsoft.Extensions.Logging;
using Smart.FA.Catalog.Application.SeedWork;
using Smart.FA.Catalog.Core.Domain.Dto;
using Smart.FA.Catalog.Core.Domain.Interfaces;
using Smart.FA.Catalog.Core.Domain.ValueObjects;

namespace Smart.FA.Catalog.Application.UseCases.Queries;

public class
    GetTrainingsFromTrainerQueryHandler : IRequestHandler<GetTrainingsFromTrainerRequest,
        GetTrainingsFromTrainerResponse>
{
    private readonly ILogger<GetTrainingsFromTrainerQueryHandler> _logger;
    private readonly ITrainingQueries _trainingQueries;

    public GetTrainingsFromTrainerQueryHandler(ILogger<GetTrainingsFromTrainerQueryHandler> logger,
        ITrainingQueries trainingQueries )
    {
        _logger = logger;
        _trainingQueries = trainingQueries;
    }

    public async Task<GetTrainingsFromTrainerResponse> Handle(GetTrainingsFromTrainerRequest request,
        CancellationToken cancellationToken)
    {
        GetTrainingsFromTrainerResponse resp = new();

        try
        {
            resp.Trainings = await _trainingQueries.GetListAsync(request.TrainerId, request.Language.Value, cancellationToken);
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

public class GetTrainingsFromTrainerRequest : IRequest<GetTrainingsFromTrainerResponse>
{
    public int TrainerId { get; init; }
    public Language Language { get; init; } = null!;
}

public class GetTrainingsFromTrainerResponse : ResponseBase
{
    public IEnumerable<TrainingDto>? Trainings { get; set; }
}
