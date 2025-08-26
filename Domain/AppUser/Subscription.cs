namespace Domain.AppUser;

public record Subscription(SubscriptionPlan Plan, DateTime? ExpiryDate)
{
    public static Subscription Standard() => new(SubscriptionPlan.Standard, null);
    public static Subscription Premium(DateTime expiryDate) => new(SubscriptionPlan.Premium, expiryDate);
    
    public bool IsActive() => ExpiryDate == null || ExpiryDate > DateTime.UtcNow;
}