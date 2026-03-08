using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    // Cái dòng này biến nó thành API chuẩn
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocApiController : ControllerBase
    {
        private readonly ApplicationDbContextContext _context;

        //  Nhận chìa khóa kho dữ liệu (Database)
        public KhoaHocApiController(ApplicationDbContextContext context)
        {
            _context = context;
        }       

        //  1. LẤY TẤT CẢ (GET)
        // Link chạy: https://localhost:port/api/KhoaHocApi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.KhoaHocs.ToListAsync();
            return Ok(list); // Trả về JSON (200 OK)
        }

        // 2. LẤY 1 CÁI THEO ID (GET)
        // Link chạy: https://localhost:port/api/KhoaHocApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.KhoaHocs.FindAsync(id);
            if (item == null) return NotFound(); // Trả về 404 (Không thấy)
            return Ok(item);
        }

        // 3. THÊM MỚI (POST)
        [HttpPost]
        public async Task<IActionResult> Create(KhoaHoc model)
        {
            _context.KhoaHocs.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // 4. SỬA (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KhoaHoc model)
        {
            if (id != model.Id) return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.KhoaHocs.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent(); // Trả về 204 (Thành công nhưng không cần báo gì thêm)
        }

        // 5. XÓA (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.KhoaHocs.FindAsync(id);
            if (item == null) return NotFound();

            _context.KhoaHocs.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}