using API.DTOs.Providers;
using API.Services.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("providers")]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService _service;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger, ProviderService service)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtener datos de un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <returns>Objeto de un proveedor</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var providers = await _service.GetProviderAsync(id);
            return Ok(providers);
        }

        /// <summary>
        /// Obtener lista de proveedores
        /// </summary>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de proveedores</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProviderRequest request)
        {
            var providers = await _service.SearchProvidersAsync(request);
            return Ok(providers);
        }

        /// <summary>
        /// Agregar un nuevo proveedor
        /// </summary>
        /// <param name="request">Datos del nuevo proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor agregado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(AddProviderRequest request)
        {
            var provider = await _service.AddProviderAsync(request);
            return Ok(provider);
        }

        /// <summary>
        /// Actualizar un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos a actualizar del proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor actualizado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateProviderRequest request)
        {
            var provider = await _service.UpdateProviderAsync(id, request);
            return Ok(provider);
        }

        /// <summary>
        /// Eliminar un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <returns>Objeto con resultado y objeto proveedor eliminado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var provider = await _service.DeleteProviderAsync(id);
            return Ok(provider);
        }

        /// <summary>
        /// Obtener la lista de servicios de un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de servicios</returns>
        /// <response code="200">Ok</response>
		/// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpGet]
        [Route("{id}/services")]
        public async Task<IActionResult> GetServices(int id, [FromQuery] GetProviderServiceRequest request)
        {
            var providers = await _service.GetServicesAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Agregar un nuevo servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos de servicio asociado</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPost]
        [Route("{id}/services")]
        public async Task<IActionResult> PostServices(int id, AddServiceProviderRequest request)
        {
            var providers = await _service.AddServiceProviderAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Actualizar servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del servicio asociado a proveedor</param>
        /// <param name="request">Datos de servicio asociado</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPut]
        [Route("services/{id}")]
        public async Task<IActionResult> PutServices(int id, UpdateServiceProviderRequest request)
        {
            var providers = await _service.UpdateServiceProviderAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Eliminar servicios asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del servicio asociado a proveedor</param>
        /// <returns>Objeto con resultado y objeto servicios a proveedor asociado</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpDelete]
        [Route("services/{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var providers = await _service.DeleteServiceProviderAsync(id);
            return Ok(providers);
        }

        /// <summary>
        /// Obtener la lista de detalle de un proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Parametros de busqueda y paginación</param>
        /// <returns>Objeto con datos paginados y Lista de servicios</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpGet]
        [Route("{id}/details")]
        public async Task<IActionResult> GetDetails(int id, [FromQuery] GetProviderDetailsRequest request)
        {
            var providers = await _service.GetDetailsAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Agregar un nuevo detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del proveedor</param>
        /// <param name="request">Datos de detalle asociado</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPost]
        [Route("{id}/details")]
        public async Task<IActionResult> PostDetails(int id, AddDetailProviderRequest request)
        {
            var providers = await _service.AddDetailProviderAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Actualiza un detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del detalle asociado a proveedor</param>
        /// <param name="request">Datos de detalle asociado</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpPut]
        [Route("details/{id}")]
        public async Task<IActionResult> PutDetails(int id, UpdateDetailProviderRequest request)
        {
            var providers = await _service.UpdateDetailProviderAsync(id, request);
            return Ok(providers);
        }

        /// <summary>
        /// Actualiza un detalle asociado al proveedor
        /// </summary>
        /// <param name="id">Identificador del detalle asociado a proveedor</param>
        /// <param name="request">Datos de detalle asociado</param>
        /// <returns>Objeto con resultado y objeto detalle a proveedor</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Indica el usuario no esta autorizado</response>
        [Authorize]
        [HttpDelete]
        [Route("details/{id}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            var providers = await _service.DeleteDetailProviderAsync(id);
            return Ok(providers);
        }
    }
}