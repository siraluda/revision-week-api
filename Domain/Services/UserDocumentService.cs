using Domain.AppUser;
using Domain.Common;
using Domain.Documents;

namespace Domain.Services;

public class UserDocumentService(IUserRepository userRepository)
{
    private const long BytesScale = 1024 * 1024;
    private const long StandardUserMaximumFileSize = 100 * BytesScale; // 100 MB
    
    // Check if user can upload the document based on their subscription plan before generating upload presigned-url
    public async Task<bool> CanUploadDocument(long fileSize, string userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if(user == null) throw new DomainException($"User with {userId} not found");

        if (user.Subscription.Plan == SubscriptionPlan.Standard && fileSize > StandardUserMaximumFileSize)
        {
            throw new DomainException(
                $"Standard users cannot upload files larger than {(int)(StandardUserMaximumFileSize / BytesScale)} MB");
        }
        return true;
    }
}