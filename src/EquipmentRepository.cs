using Domain.Оборудование.Contracts;
using Domain.Equipment;

namespace Infrastructure.Repositories
{
    public class EquipmentRepository : ICanStoreEquipment
    {
        private static readonly List<Equipment> _storage = new();

        public Task<Equipment?> Получитьоборудование(GetEquipmentOptions параметр, CancellationToken ct = default)
        {
            var query = _storage.AsQueryable();

            if (параметр.Id.HasValue)
            {
                query = query.Where(e => e.Id.Id == параметр.Id.Value);
            }

            if (!string.IsNullOrEmpty(параметр.name))
            {
                query = query.Where(e => e.Model.Value.Contains(параметр.name));
            }

            return Task.FromResult(query.FirstOrDefault());
        }
    }
}
