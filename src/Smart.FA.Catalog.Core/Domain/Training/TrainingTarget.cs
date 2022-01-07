namespace Core.Domain;

public class TrainingTarget
{
    #region Properties

    public int TrainingId { get; }
    public int TrainingTargetAudienceId { get; }
    public virtual Training Training { get; }
    public virtual TrainingTargetAudience TrainingTargetAudience { get; }

    #endregion

    #region Constructors

    protected TrainingTarget()
    {

    }

    public TrainingTarget(Training training, TrainingTargetAudience trainingTargetAudience)
    {
        Training = training;
        TrainingTargetAudience = trainingTargetAudience;
        TrainingId = training.Id;
        TrainingTargetAudienceId = trainingTargetAudience.Id;
    }

    #endregion
}