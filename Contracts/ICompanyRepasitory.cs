using DapperApi.Entities;

namespace DapperApi.Contract;

public interface ICompanyRepository
{
    public Task<IEnumerable<Company>> GetCompanies();
    public Task<Company> GetCompanyById(int id);
}