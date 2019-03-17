using Ritter.Samples.Domain.Aggregates.People;

namespace Ritter.Samples.Domain.Aggregates.Employees
{
    public static class EmployeeFactory
    {
        public static Employee CreateEmployee(string firstName, string lastName, string cpf)
        {
            return new Employee(
                new Name(firstName, lastName),
                new Cpf(cpf));
        }
    }
}
