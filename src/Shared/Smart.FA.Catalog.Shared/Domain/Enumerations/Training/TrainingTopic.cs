namespace Smart.FA.Catalog.Shared.Domain.Enumerations.Training;

public class TrainingTopic: Enumeration
{
    public static readonly TrainingTopic LanguageCourse        = new(1, nameof(LanguageCourse));
    public static readonly TrainingTopic InformationTechnology = new(2, nameof(InformationTechnology));
    public static readonly TrainingTopic SocialScience         = new(3, nameof(SocialScience));
    public static readonly TrainingTopic School                = new(4, nameof(School));
    public static readonly TrainingTopic HealthCare            = new(5, nameof(HealthCare));
    public static readonly TrainingTopic EconomyMarketing      = new(8, nameof(EconomyMarketing));
    public static readonly TrainingTopic Communication         = new(6, nameof(Communication));
    public static readonly TrainingTopic Culture               = new(7, nameof(Culture));
    public static readonly TrainingTopic Sport                 = new(9, nameof(Sport));

    protected TrainingTopic(int id, string name) : base(id, name)
    {
    }
}
