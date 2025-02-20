using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using HL7ConsentProcessing.Domain.Entities;
using HL7ConsentProcessing.Domain.Repositories;

namespace HL7ConsentProcessing.Application.Services
{
    public class FhirConsentService
    {

        private readonly IRepository<FhirConsent> _repository;

        public FhirConsentService(IRepository<FhirConsent> repository)
        {
            _repository = repository;
        }

        public async Task<FhirConsent> CreateConsentFromBundle(Bundle bundle)
        {
            var consentEntry = bundle.Entry.FirstOrDefault(e => e.Resource is Consent);
            if (consentEntry == null)
            {
                throw new InvalidOperationException("No Consent resource found in the bundle.");
            }

            var consent = (Consent)consentEntry.Resource;
            var fhirConsent = new FhirConsent
            {
                Id = consent.Id,
                Status = consent.Status,
                Patient = consent.Patient
                // Map other properties as needed
            };
            await _repository.AddAsync(fhirConsent);
            return fhirConsent;
        }

        public string SerializeConsent(Consent consent)
        {
            var serializer = new FhirJsonSerializer();
            return serializer.SerializeToString(consent);
        }
    }
}
