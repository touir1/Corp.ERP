using Corp.ERP.Inventory.Application.Queries.GetEquipments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Corp.ERP.Inventory.Service.RestAPI.Controllers
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EquipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetEquipments([FromQuery] GetEquipmentsQuery request)
        {
            var equipments = await _mediator.Send(request);
            return Ok(equipments);
        }
    }
}
