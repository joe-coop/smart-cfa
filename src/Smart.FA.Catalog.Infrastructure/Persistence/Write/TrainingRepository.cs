using System.Linq.Expressions;
using Core.Domain;
using Core.Domain.Interfaces;
using Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write;

public class TrainingRepository : ITrainingRepository
{
    private readonly Context _context;

    public TrainingRepository(Context context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Training>> GetListAsync(int trainerId, Specification<Training> specification, CancellationToken cancellationToken)
        => await _context
            .Trainings
            .Include(training => training.TrainerEnrollments)
            .Where(specification.ToExpression())
            .Where(training =>
                        training
                            .TrainerEnrollments
                            .Select(enrollment => enrollment.TrainerId)
                            .Contains(trainerId))
            .ToListAsync(cancellationToken);

    public async Task<Training?> GetFullAsync(int trainingId, CancellationToken cancellationToken)
        => await _context.Trainings.Include(training => training.TrainerEnrollments)
            .Include(training => training.Details)
            .Include(training => training.Identities)
            .Include(training => training.Slots)
            .Include(training => training.Targets)
            .FirstOrDefaultAsync(training => training.Id == trainingId, cancellationToken);

    public async Task<Training> FindAsync(int trainingId, CancellationToken cancellationToken)
        => await _context.Trainings.SingleAsync( training => training.Id == trainingId, cancellationToken);
}
