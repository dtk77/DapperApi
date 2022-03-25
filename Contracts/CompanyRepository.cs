using Dapper;
using DapperApi.Context;
using DapperApi.Dto;
using DapperApi.Entities;
using System.Data;

namespace DapperApi.Contract;

public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext _context;

    public CompanyRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Company> CreateCompany(CompanyForCreationDto company)
    {
          var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)"
                        + "SELECT CAST(SCOPE_IDENTITY() as int)";
           
        var parameters = new DynamicParameters();
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, parameters);
            var createdCompany = new Company
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
            return createdCompany;
        }
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