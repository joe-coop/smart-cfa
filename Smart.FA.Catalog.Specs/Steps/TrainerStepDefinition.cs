using Core.Domain;
using FluentAssertions;
using Smart.FA.Catalog.Tests.Common;

namespace Smart.FA.Catalog.Specs.Steps;

[Binding]
public class TrainerSpecs
{
    private readonly TrainerFactory _trainerFactory = new();
    private Trainer _trainer = null!;
    private Action _action = null!;

    [Given(@"I have a valid trainer")]
    public void GivenIHaveAValidTrainer()
    {
        _trainer = _trainerFactory.CreateClean();
    }

    [When(@"I try to update his title with the invalid (.*)")]
    public void WhenITryToUpdateHisDescriptionWithTheInvalid(string title)
    {
        _action = () => _trainer.UpdateTitle(title);
    }

    [Then(@"the code should throw an error")]
    public void ThenTheCodeShouldThrowAnError()
    {
        _action.Should().Throw<Exception>();
    }


    [When(@"I try to update with the invalid title (.*)")]
    public void WhenITryToUpdateWithTheInvalidTitle(string title)
    {
        _action = () => _trainer.UpdateTitle(title);
    }
}




