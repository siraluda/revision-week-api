using Domain.AppUser;
using Domain.Common;
using Domain.Documents;

namespace RevisionWeek.Tests.DomainTests;

public class AppUserDomainTests
{
    private readonly string _email;
    private readonly string _displayName;
    private readonly string _keycloakId;
    private readonly User _user;
    
    public AppUserDomainTests()
    {
        _email = "test@email.com";
        _displayName = "TestUser";
        _keycloakId = Guid.NewGuid().ToString();
        
        _user = new User(_keycloakId, _email, _displayName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void creating_a_user_without_valid_keycloakId_should_throw_exception(string keycloakId)
    {
        Assert.Throws<DomainException>(() => new User(keycloakId, _email, _displayName));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void creating_a_user_without_valid_email_should_throw_exception(string email)
    {
        Assert.Throws<DomainException>(() => new User(_keycloakId, email, _displayName));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void creating_a_user_without_valid_displayName_should_throw_exception(string displayName)
    {
        Assert.Throws<DomainException>(() => new User(_keycloakId, _email, displayName));
    }

    [Fact]    
    public void upgrading_user_subcription_plan_with_date_in_the_past_should_throw_exception()
    {
        Assert.Throws<DomainException>(() => _user.UpgradeToPremium(new DateTime(2020, 01, 01)));
    }
    
    [Fact]    
    public void upgrading_user_subcription_plan_with_date_in_the_future_should_succeed()
    {
        _user.UpgradeToPremium(new DateTime(2050, 01, 01));
        Assert.Equal(SubscriptionPlan.Premium, _user.Subscription.Plan);
    }
    
    [Fact]
    public void default_user_plan_should_be_standard()
    {
        Assert.Equal(SubscriptionPlan.Standard, _user.Subscription.Plan);
    }

    [Fact]
    public void downgrade_user_to_standard_should_succeed()
    {
        // verify default plan
        Assert.Equal(SubscriptionPlan.Standard, _user.Subscription.Plan);
        
        // upgrade user to premium
        _user.UpgradeToPremium(new DateTime(2050, 01, 01));
        Assert.Equal(SubscriptionPlan.Premium, _user.Subscription.Plan);
        
        // downgrade user back to standard
        _user.DowngradeToStandard();
        Assert.Equal(SubscriptionPlan.Standard, _user.Subscription.Plan);
    }
}