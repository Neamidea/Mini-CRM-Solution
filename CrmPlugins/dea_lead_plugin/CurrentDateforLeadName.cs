using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace dea_lead_plugin
{
    public class dea_lead_CreateAndUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Get services
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            try
            {
                // Ensure Target exists
                if (!context.InputParameters.Contains("Target"))
                {
                    return;
                }

                // Get the target entity
                Entity lead = context.InputParameters["Target"] as Entity;

                if (lead == null)
                {
                    return;
                }

                if (context.Depth > 1)
                {
                    return;
                }

                tracingService.Trace("Lead plugin executed.");


                string leadName = null;

                if (lead.Contains("dea_leadname"))
                {
                    leadName = lead.GetAttributeValue<string>("dea_leadname");
                }

                if (string.IsNullOrWhiteSpace(leadName))
                {
                    return;
                }

                if (!leadName.StartsWith("LEAD-", StringComparison.OrdinalIgnoreCase))
                {
                    string newLeadName = $"LEAD-{DateTime.Now.Year}-{leadName}";
                    lead["dea_leadname"] = newLeadName;
                }    

            }
            catch (Exception ex)
            {
                tracingService.Trace($"Error: {ex}");
                throw new InvalidPluginExecutionException(
                    "An error occurred in dea_lead_CreateAndUpdate.", ex);
            }
        }
    }
}
