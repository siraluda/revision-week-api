using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Documents;
using Domain.Practice_Test;

namespace Domain.AppUser;

public class User : BaseEntity, IAggregate
{
    public string Email { get; private set; }
    public string DisplayName { get; private set; }
    public Subscription Subscription { get; private set; }
    
    private readonly List<Document> _documentIds = new ();
    public IReadOnlyList<Document> Documents => _documentIds.AsReadOnly();
    
    private readonly List<PracticeTest> _practiceTestIds = new ();
    public IReadOnlyList<PracticeTest> PracticeTests => _practiceTestIds.AsReadOnly();
    
    private User(){ }

    public User(string userKeycloakId, string email, string displayName)
    {
        if (string.IsNullOrWhiteSpace(userKeycloakId)) throw new DomainException(nameof(userKeycloakId));
        if (string.IsNullOrWhiteSpace(email)) throw new DomainException(nameof(email));
        if (string.IsNullOrWhiteSpace(displayName)) throw new DomainException(nameof(displayName));
        
        Id = Guid.Parse(userKeycloakId);
        Email = email;
        DisplayName = displayName;
        Subscription = Subscription.Standard();
    }

    public void UpgradeToPremium(DateTime premiumExpiryDate)
    {
        if (premiumExpiryDate < DateTime.UtcNow) throw new DomainException(nameof(premiumExpiryDate));
        Subscription = Subscription.Premium(premiumExpiryDate) ?? throw new DomainException(nameof(premiumExpiryDate));
    }

    public void DowngradeToStandard()
    {
        Subscription = Subscription.Standard();
    }
}