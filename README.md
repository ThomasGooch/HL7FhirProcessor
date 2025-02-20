# HL7FhirProcessor
 POC: small clean architecture for processing HL7 consent 

 
Got it! Here is a simplified Markdown explanation of Clean Architecture tailored to a Proof of Concept (POC) focused on FHIR Consent:

# Clean Architecture for FHIR Consent POC

Clean Architecture is a design philosophy that emphasizes the separation of concerns and the independence of business logic from implementation details. It helps create systems that are easy to maintain, test, and evolve. For this POC focused on FHIR Consent, we will outline the key layers and their responsibilities.

## Layers of Clean Architecture

1. **Entities**: 
   - Core business objects representing FHIR Consent.
   - Example: `Consent`.

2. **Use Cases**:
   - Application-specific business rules for handling FHIR Consent.
   - Example: `CreateConsent`, `ValidateConsent`.

3. **Interface Adapters**:
   - Adapters and converters to transform data between the use cases and external systems.
   - Example: `ConsentDTO`, `ConsentRepository`.

4. **Frameworks and Drivers**:
   - Implementation details such as frameworks, databases, and user interfaces.
   - Example: `ConsentController`, `DatabaseContext`.

## Dependency Rule

The Dependency Rule states that source code dependencies can only point inward. This ensures that the inner layers are independent of the outer layers and can be tested in isolation.

## Applying Clean Architecture to FHIR Consent POC

### Entities
Define the core business object for FHIR Consent.

```csharp
public class Consent
{
    public string Id { get; set; }
    public string PatientId { get; set; }
    public string PolicyRule { get; set; }
    // Other consent-related properties
}
```

### Use Cases
Implement the business logic for creating and validating FHIR Consent.

```csharp
public class CreateConsent
{
    private readonly IConsentRepository _consentRepository;

    public CreateConsent(IConsentRepository consentRepository)
    {
        _consentRepository = consentRepository;
    }

    public void Execute(Consent consent)
    {
        // Business logic for creating consent
        _consentRepository.Save(consent);
    }
}
```

### Interface Adapters
Create adapters to interact with the database and convert data as needed.

```csharp
public class ConsentRepository : IConsentRepository
{
    private readonly DatabaseContext _context;

    public ConsentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Save(Consent consent)
    {
        // Logic to save consent to the database
        _context.Consents.Add(consent);
        _context.SaveChanges();
    }
}
```

### Frameworks and Drivers
Implement the actual input/output operations, such as receiving API requests and storing consents in the database.

```csharp
[ApiController]
[Route("api/[controller]")]
public class ConsentController : ControllerBase
{
    private readonly CreateConsent _createConsent;

    public ConsentController(CreateConsent createConsent)
    {
        _createConsent = createConsent;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Consent consent)
    {
        _createConsent.Execute(consent);
        return Ok();
    }
}
```

By following Clean Architecture principles, even in a simple POC focused on FHIR Consent, you can ensure that your solution is modular, testable, and maintainable. Each layer has a clear responsibility, and changes in one layer have minimal impact on the others.