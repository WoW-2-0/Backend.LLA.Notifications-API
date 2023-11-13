using Notifications.Infrastructure.Application.Common.Enums;
using Notifications.Infrastructure.Domain.Common.Exceptions;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    );
}