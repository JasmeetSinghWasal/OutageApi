namespace OutageApi.Infrastructure.Repositories;

using OutageApi.Application.Interfaces;
using OutageApi.Domain.Entities;
using OutageApi.Infrastructure.Seed;

public class InMemoryOutageRepository : IOutageRepository
{
    private readonly List<Outage> _outages = OutageSeedData.Generate(1000);
    public IEnumerable<Outage> GetAll()
    {
        return _outages.AsEnumerable();
    }
    public Outage GetById(Guid id)
    {
        var outage = _outages.FirstOrDefault(o => o.OutageId == id);
        if (outage == null)
        {
            throw new InvalidOperationException("Outage not found");
        }
        return outage;
    }
}