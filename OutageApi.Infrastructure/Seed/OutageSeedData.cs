using OutageApi.Domain.Entities;
namespace OutageApi.Infrastructure.Seed;

using OutageApi.Domain.Entities;
public static class OutageSeedData
{
    private static readonly string[] Areas = 
    {
        "Toronto", "Ottawa", "Mississauga", "Hamilton", "London",
        "Calgary", "Edmonton", "Vancouver", "Winnipeg", "Halifax",
        "Montreal", "Quebec City", "Victoria", "Saskatoon", "Regina"
    };

    private static readonly string[] DescriptionTemplates =
    {
        "Pressure drop detected at station {0}",
        "Scheduled maintenance on line {0}",
        "Unexpected shutdown reported in sector {0}",
        "Gas leak investigation in progress near {0}",
        "Sensor malfunction flagged at facility {0}",
        "Flow rate anomaly in distribution pipe {0}",
        "Equipment failure reported at compressor {0}",
        "Routine inspection triggered alert at valve {0}"
    };

    private static readonly string[] TagPool =
    {
        "high-priority", "critical", "low-impact", "weather-related",
        "equipment", "scheduled", "investigation", "customer-impact",
        "environmental", "safety", "planned", "emergency"
    };

    public static List<Outage> Generate(int count = 1000)
    {
        var random = new Random(42); // fixed seed for reproducible data
        var outages = new List<Outage>(count);
        var types = Enum.GetValues<OutageType>();
        var statuses = Enum.GetValues<OutageStatus>();

        for (int i = 1; i <= count; i++)
        {
            var template = DescriptionTemplates[random.Next(DescriptionTemplates.Length)];
            var description = string.Format(template, $"#{random.Next(100, 999)}");
            
            // Pick 1 to 3 random tags
            var tagCount = random.Next(1, 4);
            var tags = TagPool
                .OrderBy(_ => random.Next())
                .Take(tagCount)
                .ToList();

            outages.Add(new Outage
            {
                OutageId = Guid.NewGuid(),
                Description = description,
                Area = Areas[random.Next(Areas.Length)],
                Type = types[random.Next(types.Length)],
                Tags = tags,
                ReportedAt = DateTime.UtcNow.AddHours(-random.Next(0, 720)),
                Status = statuses[random.Next(statuses.Length)]
            });
        }

        return outages;
    }
}