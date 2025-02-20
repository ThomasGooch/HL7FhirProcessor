using HL7ConsentProcessing.Application.Services;
using HL7ConsentProcessing.Domain.Entities;
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
        public IActionResult CreateConsent([FromBody] FhirConsent fhirConsent)
        {
            var consent = _service.CreateConsent(fhirConsent);
            var serializedConsent = _service.SerializeConsent(consent);
            return Ok(serializedConsent);
        }
    }
}
