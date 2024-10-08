﻿using Asp.Versioning;
using AutoMapper;
using Hotello.Core.Contracts;
using Hotello.Core.Exceptions;
using Hotello.Core.Models;
using Hotello.Core.Models.Country;
using Hotello.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace Hotello.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CountriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;

    public CountriesController(IMapper mapper, ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    // GET: api/Countries
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
    {
        var countries = await _countryRepository.GetAllAsync();
        var records = _mapper.Map<List<GetCountryDTO>>(countries);
        return Ok(records);
    }

    // GET: api/Countries?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<PagedResult<GetCountryDTO>>> GetPagedCountries(
        [FromQuery] QueryParams queryParams)
    {
        var pagedCountriesResult = await _countryRepository.GetAllAsync<GetCountryDTO>(queryParams);
        return Ok(pagedCountriesResult);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDTO>> GetCountry(int id)
    {
        var country = await _countryRepository.GetDetailsAsync(id);

        if (country == null)
        {
            throw new NotFoundException(nameof(GetCountry), id);
        }

        var countryDTO = _mapper.Map<CountryDTO>(country);

        return Ok(countryDTO);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
    {
        if (id != updateCountryDTO.Id)
        {
            return BadRequest("Invalid Record ID");
        }

        var country = await _countryRepository.GetAsync(id);
        if (country == null)
        {
            throw new NotFoundException(nameof(GetCountry), id);
        }

        _mapper.Map(updateCountryDTO, country);

        try
        {
            await _countryRepository.UpdateAsync(country);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountryDTO)
    {
        var country = _mapper.Map<Country>(createCountryDTO);
        var createdCountry = await _countryRepository.AddAsync(country);

        return CreatedAtAction("GetCountry", new { id = createdCountry.Id }, createdCountry);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _countryRepository.GetAsync(id);
        if (country == null)
        {
            throw new NotFoundException(nameof(GetCountry), id);
        }

        await _countryRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> CountryExists(int id)
    {
        return await _countryRepository.Exists(id);
    }
}
