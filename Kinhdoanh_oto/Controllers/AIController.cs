using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Kinhdoanh_oto.Controllers
{
    public class AIController : Controller
    {
        private readonly string apiKey = "AIzaSyDFLkQGewJrP7POD49HogKFUb3tu6TZXqA";

        [HttpPost]
        public async Task<IActionResult> AskAI(string message)
        {
            try
            {
                var client = new HttpClient();

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new
                                {
                                    text = "Bạn là AI tư vấn xe cho website bán ô tô. Trả lời ngắn gọn: " + message
                                }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

                var response = await client.PostAsync(url, content);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Lỗi API: " + result);
                }

                var jsonDoc = JsonDocument.Parse(result);

                var text = jsonDoc
                    .RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return Content(text ?? "AI không phản hồi.");
            }
            catch (Exception ex)
            {
                return Content("AI lỗi: " + ex.Message);
            }
        }
    }
}