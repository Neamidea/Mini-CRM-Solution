using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace dea_customer_plugin
{
    public class BlockCreditLimitOnFrozenStatusPreImage : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                if (!context.InputParameters.Contains("Target"))
                {
                    return;
                }

                if (!context.PreEntityImages.Contains("PreImage"))
                {
                    throw new InvalidPluginExecutionException("PreImage not configured.");
                }

                if (context.Depth > 1)
                {
                    return;
                }

                Entity customer = context.InputParameters["Target"] as Entity;
                Entity preImage = context.PreEntityImages["PreImage"];

                if (customer == null)
                {
                    return;
                }

                int? status = preImage.GetAttributeValue<OptionSetValue>("dea_status")?.Value;

                decimal? oldLimit = preImage.GetAttributeValue<Money>("dea_creditlimit")?.Value;
                decimal? newLimit = customer.Contains("dea_creditlimit")
                    ? customer.GetAttributeValue<Money>("dea_creditlimit")?.Value : oldLimit;

                if (oldLimit != null && status == 0 && oldLimit != newLimit) // 0 = Frozen (based on config)
                {
                    throw new InvalidPluginExecutionException("This customer is frozen. You cannot change credit limit.");
                } 

                tracingService.Trace($"Status: {status}, Old: {oldLimit}, New: {newLimit}");

            }
            catch (InvalidPluginExecutionException ex)
                {
                    tracingService.Trace($"Error: {ex}");
                    throw new InvalidPluginExecutionException("An error occured in BlockCreditLimitOnFrozenStatusPreImage", ex);
                }
 
        }
    }
}
