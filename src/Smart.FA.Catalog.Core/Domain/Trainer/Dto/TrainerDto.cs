namespace Core.Domain.Dto;

public class TrainerDto
{
    public int Ids { get; set; }

    public  Name Name { get;  }

    public string Description { get; }

    public Language DefaultLanguage { get; }

}