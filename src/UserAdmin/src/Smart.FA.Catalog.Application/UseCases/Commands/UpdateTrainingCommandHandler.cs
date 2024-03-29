using MediatR;
using Microsoft.Extensions.Logging;
using Smart.FA.Catalog.Application.SeedWork;
using Smart.FA.Catalog.Core.Domain;
using Smart.FA.Catalog.Core.Domain.Dto;
using Smart.FA.Catalog.Core.Domain.Enumerations;
using Smart.FA.Catalog.Core.Domain.Interfaces;
using Smart.FA.Catalog.Core.Domain.ValueObjects;
using Smart.FA.Catalog.Core.Exceptions;
using Smart.FA.Catalog.Core.LogEvents;
using Smart.FA.Catalog.Core.SeedWork;
using Smart.FA.Catalog.Core.Services;
using Smart.FA.Catalog.Shared.Domain.Enumerations.Training;

namespace Smart.FA.Catalog.Application.UseCases.Commands;

public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingRequest, UpdateTrainingResponse>
{
    private readonly ILogger<UpdateTrainingCommandHandler> _logger;
    private readonly ITrainingRepository _trainingRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;

    public UpdateTrainingCommandHandler
    (
        ILogger<UpdateTrainingCommandHandler> logger
        , ITrainingRepository trainingRepository
        , ITrainerRepository trainerRepository
        , IUnitOfWork unitOfWork
        , IMailService mailService
    )
    {
        _logger = logger;
        _trainingRepository = trainingRepository;
        _trainerRepository = trainerRepository;
        _unitOfWork = unitOfWork;
        _mailService = mailService;
    }

    public async Task<UpdateTrainingResponse> Handle(UpdateTrainingRequest request, CancellationToken cancellationToken)
    {
        UpdateTrainingResponse resp = new();

        try
        {
            var training = await _trainingRepository.GetFullAsync(request.TrainingId, cancellationToken);

            if (training is null) throw new TrainingException(Errors.Training.NotFound(request.TrainingId));

            training.UpdateDetails(request.Detail.Title!, request.Detail.Goal!, request.Detail.Methodology!,
                Language.Create(request.Detail.Language).Value);
            training.SwitchTrainingTypes(request.Types);
            training.SwitchTargetAudience(request.TargetAudiences);
            training.SwitchSlotNumberType(request.SlotNumberTypes);
            training.SwitchTopics(request.Topics);
            var trainers = await _trainerRepository.GetListAsync(request.TrainingId, cancellationToken);
            training.AssignTrainers(trainers);

            if (request.IsDraft is not true)
            {
                training.Validate();
            }

            _unitOfWork.RegisterDirty(training);
            _unitOfWork.Commit();

            _logger.LogInformation(LogEventIds.TrainingUpdated, "Training with id {Id} has been updated", training.Id);

            resp.Training = training;
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

public class UpdateTrainingRequest : IRequest<UpdateTrainingResponse>
{
    public int TrainingId { get; init; }
    public TrainingDetailDto Detail { get; init; } = null!;
    public List<TrainingTargetAudience>? TargetAudiences { get; init; }
    public List<TrainingType> Types { get; init; } = null!;
    public List<TrainingSlotNumberType> SlotNumberTypes { get; init; } = null!;
    public List<TrainingTopic> Topics { get; init; } = null!;
    public List<int> TrainerIds { get; init; } = null!;
    public bool IsDraft { get; set; }
}

public class UpdateTrainingResponse : ResponseBase
{
    public Training Training { get; set; } = null!;
}
