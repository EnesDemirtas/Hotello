﻿using AutoMapper;
using Hotello.Core.Contracts;
using Hotello.Core.Models.Hotel;
using Hotello.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotello.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;
    public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
    {
        var hotels = await _hotelRepository.GetAllAsync();
        return Ok(_mapper.Map<List<HotelDTO>>(hotels));
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDTO>> GetHotel(int id)
    {
        var hotel = await _hotelRepository.GetAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<HotelDTO>(hotel));
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDTO)
    {
        if (id != hotelDTO.Id)
        {
            return BadRequest();
        }

        var hotel = await _hotelRepository.GetAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        _mapper.Map(hotelDTO, hotel);

        try
        {
            await _hotelRepository.UpdateAsync(hotel);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await HotelExists(id))
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

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO createHotelDTO)
    {
        var hotel = _mapper.Map<Hotel>(createHotelDTO);
        var createdHotel = await _hotelRepository.AddAsync(hotel);

        return CreatedAtAction("GetHotel", new { id = createdHotel.Id }, createdHotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _hotelRepository.GetAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        await _hotelRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> HotelExists(int id)
    {
        return await _hotelRepository.Exists(id);
    }
}
