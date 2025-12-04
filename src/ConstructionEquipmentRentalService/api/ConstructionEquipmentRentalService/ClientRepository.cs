using Domain.Клиент.Contracts;
using Domain.Клиент;

namespace RentalService.Infrastructure.Repositories
{
    public class ClientRepository : ICanStoreClient
    {
        private static readonly List<Клиент> _storage = new();

        public Task<Клиент?> Получитьклиента(GetClientOptions параметр, CancellationToken ct = default)
        {
            var query = _storage.AsQueryable();

            if (параметр.Id.HasValue)
            {
                query = query.Where(c => c.Id.Id == параметр.Id.Value);
            }

            if (!string.IsNullOrEmpty(параметр.name))
            {
                query = query.Where(c => c.ФИО.Значние.Contains(параметр.name));
            }

            return Task.FromResult(query.FirstOrDefault());
        }
    }
}
