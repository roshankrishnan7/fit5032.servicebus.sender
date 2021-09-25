using fit5032.order_sender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace fit5032.order_sender.Services
{
    public interface IServiceBusSenderService
    {
        Task SendMessageAsync(OrderMessage orderMessage, CancellationToken cancellationToken = default);
    }
}
