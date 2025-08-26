using Domain.Common;

namespace Domain.Practice_Test;

public sealed class Question : BaseEntity
{
    public PracticeTest PracticeTest { get; private set; }
    public string Text { get; private set; }
    public IReadOnlyCollection<string> Options { get; private set; }
    private int CorrectOptionIndex { get; set; }
    public int? SelectedOptionIndex { get; private set; }
    
    private Question(){}

    public Question(string text, IReadOnlyCollection<string> options, int correctOptionIndex)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new DomainException("Question text cannot be empty");
        if (options is null || options.Count < 4) throw new DomainException("Question must have at least 4 options");
        if (correctOptionIndex < 0) throw new DomainException("Correct option index cannot be negative");
        
        Text = text;
        Options = options;
        CorrectOptionIndex = correctOptionIndex;
    }

    public bool IsCorrect() => SelectedOptionIndex == CorrectOptionIndex;
    
}