#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.FA.Catalog.Shared.Domain.Enumerations.Training;
using Smart.FA.Catalog.Showcase.Domain.Models;

namespace Smart.FA.Catalog.Showcase.Web.Pages.Training;

public class TrainingListModel : PageModel
{
    private readonly Infrastructure.Data.CatalogShowcaseContext _context;

    public TrainingListModel(Infrastructure.Data.CatalogShowcaseContext context)
    {
        _context = context;
    }

    public IList<TrainingList> TrainingList { get; set; }
    public List<Training> Trainings { get; set; } = new List<Training>();

    public async Task OnGetAsync()
    {
        TrainingList = await _context.TrainingList.ToListAsync();
        TrainingList = TrainingList.OrderBy(o => o.TrainingId).ToList();

        foreach (var training in TrainingList)
        {
            if (training.TrainingStatus != TrainingStatus.Validated.Id)
            {
                continue;
            }

            if (Trainings.Any(t => t.TrainingId == training.TrainingId))
            {
                int index = Trainings.FindIndex(a => a.TrainingId == training.TrainingId);

                if (Trainings[index].Topics.All(t => t.Id != training.TrainingTopic))
                {
                    Trainings[index].Topics
                        .Add(TrainingTopic.FromValue<TrainingTopic>(training.TrainingTopic));
                }

                if (!Trainings[index].TrainingLanguages.Contains(training.TrainingLanguage))
                {
                    Trainings[index].TrainingLanguages.Add(training.TrainingLanguage);
                }
            }
            else
            {
                Training newTraining = new()
                {
                    TrainingId = training.TrainingId,
                    TrainingTitle = training.TrainingTitle,
                    TrainerFirstName = training.TrainerFirstName,
                    TrainerLastName = training.TrainerLastName,
                    TrainingStatus = TrainingStatus.FromValue<TrainingStatus>(training.TrainingStatus)
                    //.GetValues((int)training.TrainingStatus)
                    //)Enum.Parse(typeof(TrainingStatus), training.TrainingStatus)
                    //TrainingStatus(training.TrainingStatus)
                };
                newTraining.Topics.Add(TrainingTopic.FromValue<TrainingTopic>(training.TrainingTopic));
                newTraining.TrainingLanguages.Add(training.TrainingLanguage);

                Trainings.Add(newTraining);
            }
        }
    }
}
