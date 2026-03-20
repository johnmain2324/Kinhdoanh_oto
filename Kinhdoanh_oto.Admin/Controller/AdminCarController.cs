using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Admin.Data;
using Kinhdoanh_oto.Admin.Models;
using System.Linq;
using System.Text.Json;

namespace Kinhdoanh_oto.Admin.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly AdminDbContext _context;

        public AdminCarController(AdminDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car model, IFormFile ThumbnailFile)
        {
            if (ThumbnailFile != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ThumbnailFile.FileName);

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ThumbnailFile.CopyTo(stream);
                }

                model.Thumbnail = "/images/" + fileName;
            }

            model.NgayTao = DateTime.Now;

            _context.Cars.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var car = _context.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [HttpPost]
        public IActionResult Edit(Car model, IFormFile ThumbnailFile)
        {
            var car = _context.Cars.Find(model.Id);

            if (car == null)
            {
                return NotFound();
            }

            car.TenXe = model.TenXe;
            car.HangXe = model.HangXe;
            car.DongXe = model.DongXe;
            car.NamSanXuat = model.NamSanXuat;
            car.GiaNiemYet = model.GiaNiemYet;
            car.GiaKhuyenMai = model.GiaKhuyenMai;
            car.Noidung = model.Noidung;

            if (ThumbnailFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ThumbnailFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ThumbnailFile.CopyTo(stream);
                }

                car.Thumbnail = "/images/" + fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return RedirectToAction("Index"); // quay lại danh sách
        }

        [HttpPost]
        public async Task<IActionResult> CreateByAI([FromBody] dynamic body)
        {
            string input = body?.text ?? "";

            var prompt = $@"
        Tạo thông tin xe từ: {input}

        Trả về JSON:
        {{
            ""TenXe"": """",
            ""HangXe"": """",
            ""NamSanXuat"": 2024,
            ""GiaNiemYet"": 0,
            ""HopSo"": """",
            ""Noidung"": """"
        }}

        CHỈ TRẢ JSON
        ";

            var client = new HttpClient();

            string apiKey = "AIzaSyB491Uk-2GXeGE4pFIGhCJoDIxmdZEqsjs";

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
            };

            var response = await client.PostAsJsonAsync(
                $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={apiKey}",
                requestBody
            );

            // 🔥 LẤY RAW JSON (QUAN TRỌNG)
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            var doc = JsonDocument.Parse(json);

            string? content = "";

            try
            {
                var parts = doc
                    .RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts");

                if (parts.GetArrayLength() > 0)
                {
                    var firstPart = parts[0];

                    if (firstPart.TryGetProperty("text", out var textElement))
                    {
                        content = textElement.GetString();
                    }
                }
            }
            catch
            {
                return Content("{\"error\":\"AI parse lỗi\"}", "application/json");
            }

            if (string.IsNullOrEmpty(content))
            {
                return Content("{\"error\":\"AI không trả dữ liệu\"}", "application/json");
            }

            // 🔥 CLEAN JSON
            content = content.Replace("```json", "")
                             .Replace("```", "")
                             .Trim();

            int start = content.IndexOf("{");
            int end = content.LastIndexOf("}");

            if (start >= 0 && end > start)
            {
                content = content.Substring(start, end - start + 1);
            }

            return Content(content, "application/json");
        }
    }
}