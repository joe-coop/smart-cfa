using Smart.FA.Catalog.Core.Domain.Enumerations;
using Smart.FA.Catalog.Core.Domain.ValueObjects;
using Smart.FA.Catalog.Core.SeedWork;
using Smart.FA.Catalog.Shared.Domain.Enumerations.Trainer;

namespace Smart.FA.Catalog.Core.Domain;

public class Trainer : Entity, IAggregateRoot
{
    #region Private fields

    private readonly List<TrainerAssignment> _assignments = new();
    private readonly List<PersonalSocialNetwork> _personalSocialNetworks = new();

    #endregion

    #region Properties

    public virtual Name Name { get; private set; } = null!;

    public string Biography { get; private set; } = null!;

    public string Title { get; set; } = null!;

    public string? Email { get; private set; }

    public string? ProfileImagePath { get; set; } = null!;

    public Language DefaultLanguage { get; private set; } = null!;

    public TrainerIdentity Identity { get; } = null!;

    public virtual IReadOnlyCollection<TrainerAssignment> Assignments => _assignments.AsReadOnly();

    public virtual IReadOnlyCollection<PersonalSocialNetwork> PersonalSocialNetworks =>
        _personalSocialNetworks.AsReadOnly();

    #endregion

    #region Constructors

    public Trainer(Name name, TrainerIdentity identity, string title, string biography, Language defaultLanguage)
    {
        Identity = identity;
        ChangeDefaultLanguage(defaultLanguage);
        Rename(name);
        UpdateBiography(biography);
        UpdateTitle(title);
    }

    protected Trainer()
    {
    }

    #endregion

    #region Methods

    public void UpdateBiography(string newBiography)
    {
        Guard.AgainstNull(newBiography, nameof(newBiography));
        Guard.Requires(() => newBiography.Length <= 500, "Biography is too long");

        Biography = newBiography;
    }

    public void UpdateTitle(string newTitle)
    {
        Guard.AgainstNull(newTitle, nameof(newTitle));
        Guard.Requires(() => newTitle.Length <= 150
            , "Title is too long");
        Title = newTitle;
    }

    public void UpdateProfileImagePath(string profileImagePath)
    {
        Guard.AgainstNull(profileImagePath, nameof(profileImagePath));
        Guard.Requires(() => profileImagePath.Length <= 50, "URL for profile path is too long");
        ProfileImagePath = profileImagePath;
    }

    public void AssignTo(Training training)
    {
        Guard.Requires(() => !_assignments.Select(assignment => assignment.Training).Contains(training),
            "The trainer is already assigned to that training");
        _assignments.Add(new TrainerAssignment(training, this));
    }

    public void UnAssignFrom(Training training)
    {
        var trainingNumberRemoved = _assignments.RemoveAll(assignment => assignment.Training == training);
        Guard.Ensures(() => trainingNumberRemoved != 0, "The trainer was never assigned to that training");
    }

    public List<Training> GetTrainings()
        => Assignments.Any() ? Assignments.Select(assignment => assignment.Training).ToList() : new List<Training>();

    public void Rename(Name name) => Name = name;

    public void ChangeDefaultLanguage(Language language) => DefaultLanguage = language;

    /// <summary>
    /// Adds or updates an <see cref="PersonalSocialNetwork"/> to/of the underlying <see cref="Trainer"/>.
    /// </summary>
    /// <param name="socialNetwork">The type of social network to be updated.</param>
    /// <param name="urlToProfile">The URL to profile of the trainer <paramref name="socialNetwork"/>.</param>
    public void SetSocialNetwork(SocialNetwork socialNetwork, string? urlToProfile)
    {
        var existing = _personalSocialNetworks.FirstOrDefault(p => p.SocialNetwork == socialNetwork);

        if (existing is not null)
        {
            // There is no point to keep in the database a PersonalSocialNetwork if the url is empty.
            if (string.IsNullOrWhiteSpace(urlToProfile))
            {
                _personalSocialNetworks.Remove(existing);
                return;
            }

            existing.SetPersonalSocialNetworkInfo(Id, socialNetwork, urlToProfile);
        }
        else
        {
            _personalSocialNetworks.Add(new PersonalSocialNetwork(Id, socialNetwork, urlToProfile));
        }
    }

    /// <summary>
    /// Changes the <see cref="Trainer" />'s email address.
    /// </summary>
    /// <param name="email">The new email.</param>
    /// <exception cref="ArgumentNullException"><paramref name="email" /> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="email" />'s format is invalid.</exception>
    public void ChangeEmail(string? email)
    {
        Email = Guard.AgainstInvalidEmail(email, nameof(email));
    }

    #endregion
}
