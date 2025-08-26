using Domain.AppUser;
using Domain.Common;
using Domain.Documents;

namespace Domain.Practice_Test;

public sealed class PracticeTest : BaseEntity, IAggregate
{
    private readonly List<Question> _questions = new();
    public User Owner { get; private set; }
    public Document ReferenceDocument { get; private set; }
    public int? Score { get; private set; }
    public bool IsCompleted { get; private set; }
    
    private PracticeTest(){}

    public PracticeTest(User user, Document document, IEnumerable<Question> questions)
    {
        if(questions is null) throw new DomainException("Practice Test must contain questions.");
        
        Owner = user;
        ReferenceDocument = document;
        _questions.AddRange(questions);
        IsCompleted = false;
    }
    

    public void ValidateAnswers()
    {
        if (IsCompleted) throw new DomainException("Practice Test is already completed.");
        
        int correctAnswers = _questions.Count(q => q.IsCorrect());
        
        Score = (int)(((double)correctAnswers / _questions.Count) * 100);
        IsCompleted = true;
    }
}

