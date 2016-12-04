using GenericRepository.Entities;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.EntityFramework.SampleCore.Services
{
    public class CountryService : GenericService.Services.GenericService<Country, int>
    {
        IEntityRepository<Country, int> _repository = null;

        public CountryService(IEntityRepository<Country, int> repository) : base(repository)
        {
            _repository = repository;
        }

        protected override void CompleteServiceCall(EntityOperations operation, params Country[] entity)
        {
            Trace.WriteLine("completed the operation" + operation);
        }

        protected override void OperationFailed(EntityOperations operation, params Country[] entity)
        {
            Trace.WriteLine(operation + " failed for the country entity");
        }

        public override async Task<PaginatedList<Country>> SearchAsync(EntitySearchCondition<int> searchCondition)
        {
                if (searchCondition == null) return null;

                var result = await _repository.PaginateAsync(searchCondition.PageNo, searchCondition.RecordsPerPage);
                return result;
        }

        protected override bool TryPostProcessEntity(EntityOperations operation, out string message, params Country[] entity)
        {
            message = string.Empty;
            return true;
        }

        protected override bool TryPreProcessEntity(Country entity, EntityOperations operation, out string message)
        {
            message = string.Empty;
            return true;
        }

        protected override void UnPrivilegedAccess(Country entity)
        {
            return;
        }

        protected override void UnPrivilegedAccess(IEnumerable<int> entityIds)
        {
            return;
        }

        protected override bool ValidateEntity(Country entity, EntityOperations operation, out IEnumerable<string> messages)
        {
            messages = Enumerable.Empty<string>();
            return true;
        }

        protected override bool ValidateEntityIds(EntityOperations operation, out Dictionary<string, string> messages, params int[] entityIds)
        {
            messages = new Dictionary<string, string>();
            return true;
        }
    }
}
