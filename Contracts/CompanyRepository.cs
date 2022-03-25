using Dapper;
using DapperApi.Context;
using DapperApi.Entities;

namespace DapperApi.Contract;

public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext _context;

    public CompanyRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        var query = "SELECT * FROM Companies";
        using (var connection = _context.CreateConnection())
        {
            var companies = await connection.QueryAsync<Company>(query);
            return companies.ToList();
        }
    }

    public async Task<Company> GetCompanyById(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @id";

        using (var connect = _context.CreateConnection())
        {
            var company = await connect.QuerySingleOrDefaultAsync<Company>(query, new { id });

            return company;
        }

    }
}