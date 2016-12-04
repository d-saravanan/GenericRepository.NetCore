using GenericRepository.EntityFramework.SampleCore.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.EntityFramework.SampleCore
{
    public static class HotelRepositoryExtensions
    {
        public static Task<IQueryable<Hotel>> GetAllByResortId(this IEntityRepository<Hotel, int> hotelRepository, int resortId)
        {
            return hotelRepository.FindByAsync(x => x.ResortId == resortId);
        }
    }
}