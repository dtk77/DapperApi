using DapperApi.Contract;
using DapperApi.Dto;
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

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
    {
        try
        {
            var createdCompany = await _companyRepo.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompanyById(id);
            if (dbCompany == null)
                return NotFound();
            await _companyRepo.UpdateCompany(id, company);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompanyById(id);
            if (dbCompany == null)
                return NotFound();
            await _companyRepo.DeleteCompany(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("ByEmployeeId/{id}")]
    public async Task<IActionResult> GetCompanyForEmployee(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompanyByEmployeeId(id);
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

    [HttpGet("MultipleMapping")]
    public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
    {
        try
        {
            var company = await _companyRepo
                .GetCompaniesEmployeesMultipleMapping();

            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}