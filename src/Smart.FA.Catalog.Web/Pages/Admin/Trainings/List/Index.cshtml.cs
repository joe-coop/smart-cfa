using Api.Options;
using Application.UseCases.Queries;
using Core.Domain;
using Core.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Pages.Admin.Trainings.List;

public class ListModel : AdminPage
{
    private readonly AdminOptions _adminOptions;

    // [BindProperty(SupportsGet = true)]
    // public int CurrentPage { get; set; } = 1;

    public List<TrainingListingViewModel>? Trainings { get; set; } = new();
    // public int NumberOfTrainingPerPage { get; set; }

    public ListModel(IMediator mediator, IOptions<AdminOptions> adminOptions) : base(mediator)
    {
        _adminOptions = adminOptions.Value ?? throw new ArgumentNullException(nameof(adminOptions));
        // NumberOfTrainingPerPage = _adminOptions.Training?.NumberOfTrainingsListed ?? throw new NullReferenceException();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        SetSideMenuItem();
        // if (CurrentPage < 0)
        // {
        //     return RedirectToCurrentPageOne();
        // }

        var response = await Mediator.Send(new GetTrainingsFromTrainerRequest {TrainerId = 1});
        Trainings = response.Trainings.Select(training => new TrainingListingViewModel
        {
            Status = Enumeration.FromValue<TrainingStatus>(training.StatusId), Tags = new List<string>(), Title = training.Details.FirstOrDefault()?.Title
        }).ToList();

        // If CurrentPage returns no result, redirect to the index 1.
        // return Trainings?.Count == 0 ? RedirectToCurrentPageOne() : Page();
        return Page();
    }

    private IActionResult RedirectToCurrentPageOne()
    {
        return RedirectToPage(new {CurrentPage = 1});
    }

    protected override SideMenuItem GetSideMenuItem() => SideMenuItem.MyTrainings;
}