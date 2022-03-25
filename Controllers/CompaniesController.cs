using DapperApi.Contract;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers;

[ApiController]
[Route("api/companies")]
public class CompainesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepo;
    public CompainesController(ICompanyRepository companyRepo)
    {
        _companyRepo = companyRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompanyById(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}