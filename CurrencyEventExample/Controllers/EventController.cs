using Microsoft.AspNetCore.Mvc;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;

namespace CurrencyEventExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventController : ControllerBase
    {

        [HttpPost]
        public ActionResult CurrencyEvent(List<EventGridEvent> events)
        {
            if (events != null && events.Count > 0)
            {
                var e = events[0];

                if (e.EventType.Contains("SubscriptionValidationEvent"))
                {
                    if (e.TryGetSystemEventData(out object eventData))
                    {
                        var data = (SubscriptionValidationEventData)eventData;
                        return Ok(new { ValidationResponse = data.ValidationCode });
                    }
                }
            }

            return Ok(new { Message = "Event received, but not a validation event." });
        }
    }
}
