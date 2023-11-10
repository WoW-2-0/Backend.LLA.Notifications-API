using Sms.Infrastructure.Application.Common.Enums;
using Sms.Infrastructure.Application.Common.Notifications.Models;
using Sms.Infrastructure.Domain.Common.Exceptions;

namespace Sms.Infrastructure.Application.Common.Notifications.Services;

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