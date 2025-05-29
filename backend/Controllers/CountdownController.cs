using backend.Data;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountdownDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountdownDaysController(AppDbContext context)
        {
            _context = context;
        }

        // 获取所有倒数日，包含天数差计算
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var today = DateTime.UtcNow.Date;

            // 先从数据库查询出所有数据
            var entities = await _context.CountdownDays.ToListAsync();

            // 然后在内存中进行日期计算
            var list = entities.Select(c => new CountdownDayDto
            {
                Id = c.Id,
                Name = c.Name,
                TargetDate = c.TargetDate,
                DaysDifference = (c.TargetDate.Date - today).Days
            }).ToList();

            return Ok(list);
        }


        // 添加一个新的倒数日
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountdownDayDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.TargetDate == default)
            {
                return BadRequest("无效的倒数日数据");
            }

            var entity = new CountdownDay
            {
                Name = dto.Name,
                TargetDate = dto.TargetDate
            };

            _context.CountdownDays.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.DaysDifference = (entity.TargetDate.Date - DateTime.UtcNow.Date).Days;

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        }

        // 根据ID获取单个倒数日
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.CountdownDays.FindAsync(id);
            if (entity == null) return NotFound();

            var today = DateTime.UtcNow.Date;

            var dto = new CountdownDayDto
            {
                Id = entity.Id,
                Name = entity.Name,
                TargetDate = entity.TargetDate,
                DaysDifference = (entity.TargetDate.Date - today).Days
            };

            return Ok(dto);
        }


        // 删除倒数日
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.CountdownDays.FindAsync(id);
            if (entity == null) return NotFound();

            _context.CountdownDays.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
