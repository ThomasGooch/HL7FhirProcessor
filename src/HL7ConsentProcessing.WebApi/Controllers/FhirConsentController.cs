using Hl7.Fhir.Model;
using HL7ConsentProcessing.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HL7ConsentProcessing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FhirConsentController : ControllerBase
    {
        private readonly FhirConsentService _service;

        public FhirConsentController(FhirConsentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsentAsync([FromBody] Bundle bundle)
        {
            try
            {
                var fhirConsent = await _service.CreateConsentFromBundle(bundle);
                var serializedConsent = _service.SerializeConsent(fhirConsent);
                return Ok(serializedConsent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
