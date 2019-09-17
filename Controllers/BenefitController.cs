using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using luidy_bus_test.Model;
using luidy_bus_test.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusMessaging;

namespace luidy_bus_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitController : Controller
    {
        private readonly ServiceBusTopic _serviceBusTopic;

        public BenefitController(ServiceBusTopic serviceBusTopic)
        {
            _serviceBusTopic = serviceBusTopic;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MyPayload), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody][Required] MyPayload request)
        {            
            // Send this to the bus for the other services
            await _serviceBusTopic.SendMessage(request);

            return Ok(request);
        }
    }
}