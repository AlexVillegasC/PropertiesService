
using System.Threading.Tasks;
using PropertyService.Dtos;
using PropertyService.FunctionalExtensions;
using PropertyService.Helpers;
using PropertyService.Impersonation;
using PropertyService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PropertyService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/properties")]
    [ApiController]

    // [Authorize(Roles = Roles.Gdlusers)]
    public class PropertiesController : ControllerBase
    {
        private readonly ILogger<PropertiesController> _logger;
        private readonly IPropertiesModel _productModel;
        private readonly IImpersonation _impersonation;

        public PropertiesController(ILogger<PropertiesController> logger, IPropertiesModel productMode, IImpersonation impersonation)
        {
            _logger = logger;
            _productModel = productMode;
            _impersonation = impersonation;
        }

        /// <summary>
        /// Get all product name.
        /// </summary>
        /// <returns>Products name list.</returns>
        [HttpGet("getPropertiesService", Name = "getPropertiesService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropertiesDto>> getProperties()
        {
            if (!ModelState.IsValid)
            {
                return ModelState.CreateValidationError();
            }

            var result =
            await _impersonation.Run(
                async () =>
                {
                    var productType = await _productModel.getProperties();
                    return productType.ToActionResult(this);
                }, LogonType.LOGON32_LOGON_NEW_CREDENTIALS,
                LogonProvider.LOGON32_PROVIDER_DEFAULT);

            return result.Result;
        }


        /// <summary>
        /// Get all property repo.
        /// </summary>
        /// <returns>Products name list.</returns>
        [HttpGet("propertiesRepository", Name = "propertiesRepository")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropertiesDto>> propertiesRepository()
        {
            if (!ModelState.IsValid)
            {
                return ModelState.CreateValidationError();
            }

            var result =
            await _impersonation.Run(
                async () =>
                {
                    var productType = await _productModel.propertiesRepository();
                    return productType.ToActionResult(this);
                }, LogonType.LOGON32_LOGON_NEW_CREDENTIALS,
                LogonProvider.LOGON32_PROVIDER_DEFAULT);

            return result.Result;
        }

        /// <summary>
        /// Get the template line items' values.
        /// </summary>
        /// <param name="templateName">Template Name.</param>
        /// <returns>Template.</returns>
        [HttpPost("AddProperty", Name = "AddProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropertyDto>> AddProduct(PropertyDto property)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.CreateValidationError();
            }

            var result =
            await _impersonation.Run(
                async () =>
                {
                    var productType = await _productModel.AddProperty(property);
                    return productType.ToActionResult(this);
                }, LogonType.LOGON32_LOGON_NEW_CREDENTIALS,
                LogonProvider.LOGON32_PROVIDER_DEFAULT);

            return result.Result;
        }

    }
}