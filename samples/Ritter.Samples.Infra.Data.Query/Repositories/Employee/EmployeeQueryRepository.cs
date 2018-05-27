using Ritter.Infra.Data.Query;
using Ritter.Samples.Application.DTO.Employees.Response;

namespace Ritter.Samples.Infra.Data.Query.Repositories.Employee
{
    public class EmployeeQueryRepository : QueryRepository<EmployeeDto>, IEmployeeQueryRepository
    {
        public EmployeeQueryRepository(IEFQueryUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}