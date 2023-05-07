using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BDS_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesRespository propertiesRespository;
        public PropertiesController(IPropertiesRespository _I)
        {
            propertiesRespository = _I;
        }

        [HttpGet]
        public async Task<ActionResult<Propeties>> GetAll(
             [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0,
            [FromQuery] int? minPrice = null,
            [FromQuery] int? maxPrice = null
        )
        {
            try
            {

                var myModel = await propertiesRespository.GetAll();
                //Lọc theo user và fulluser
               

               
                if (!string.IsNullOrEmpty(keyword))
                {

                    // tìm kiếm tương đối
                    myModel = myModel.Where(u => u.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || u.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

                }
               // Tìm theo giá
                if (minPrice != null)
                {
                    myModel = myModel.Where(p => p.Price >= minPrice);
                }

                if (maxPrice != null)
                {
                    myModel = myModel.Where(p => p.Price <= maxPrice);
                }
               // phân trang
                myModel = myModel.Skip(offset).Take(limit);

                return Ok(myModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ ghi log, trả về mã lỗi 500 Internal Server Error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
