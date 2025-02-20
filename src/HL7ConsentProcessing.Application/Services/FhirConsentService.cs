using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using HL7ConsentProcessing.Domain.Entities;

namespace HL7ConsentProcessing.Application.Services
{
    public class FhirConsentService
    {
        public Consent CreateConsent(FhirConsent fhirConsent)
        {
            var consent = new Consent
            {
                Id = fhirConsent.Id,
                Status = Consent.ConsentState.Active,
                Patient = new ResourceReference($"Patient/{fhirConsent.Patient}")
                // Map other properties
            };

            return consent;
        }

        public string SerializeConsent(Consent consent)
        {
            var serializer = new FhirJsonSerializer();
            return serializer.SerializeToString(consent);
        }

    }
}
