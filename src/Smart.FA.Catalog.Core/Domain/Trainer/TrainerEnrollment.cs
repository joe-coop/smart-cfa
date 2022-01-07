namespace Core.Domain;

public class TrainerEnrollment
{

    #region Properties

    public int TrainingId { get; private set; }
    public int TrainerId { get; private set; }

    public virtual Training Training { get; init; }
    public virtual Trainer Trainer { get; init; }

    #endregion

    #region Constructors

    protected TrainerEnrollment()
    {

    }

    public TrainerEnrollment(Training training, Trainer trainer)
    {
        Training = training;
        Trainer = trainer;
        TrainingId = training.Id;
        TrainerId = trainer.Id;
    }

    #endregion

}