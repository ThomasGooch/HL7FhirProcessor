using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using HL7ConsentProcessing.Domain.Entities;
using HL7ConsentProcessing.Domain.Repositories;
using Newtonsoft.Json;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace HL7ConsentProcessing.Infrastructure.Repositories
{
    public class FhirConsentRepository : IRepository<FhirConsent>
    {
        private readonly HttpClient _httpClient;

        public FhirConsentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FhirConsent> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"https://fhirserver.com/Consent/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var consent = JsonConvert.DeserializeObject<FhirConsent>(content);

            return consent;
        }

        public async Task AddAsync(FhirConsent entity)
        {
            var consent = new Consent
            {
                Id = entity.Id,
                Status = Consent.ConsentState.Active,
                Patient = new ResourceReference($"Patient/{entity.Patient}")
                // Map other properties
            };

            var serializer = new FhirJsonSerializer();
            var json = serializer.SerializeToString(consent);

            var content = new StringContent(json, Encoding.UTF8, "application/fhir+json");
            var response = await _httpClient.PostAsync("https://fhirserver.com/Consent", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(FhirConsent entity)
        {
            var consent = new Consent
            {
                Id = entity.Id,
                Status = Consent.ConsentState.Active,
                Patient = new ResourceReference($"Patient/{entity.Patient}")
                // Map other properties
            };

            var serializer = new FhirJsonSerializer();
            var json = serializer.SerializeToString(consent);

            var content = new StringContent(json, Encoding.UTF8, "application/fhir+json");
            var response = await _httpClient.PutAsync($"https://fhirserver.com/Consent/{entity.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://fhirserver.com/Consent/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
