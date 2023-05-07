using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using BDS_WEBAPI.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BDS_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRespository newsRespository;
        public NewsController(INewsRespository _I)
        {
            newsRespository = _I;
        }

        [HttpGet]
        public async Task<ActionResult<News>> GetAll(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            try
            {

                var myModel = await newsRespository.GetAll();
                //Lọc theo user và fulluser
                if (!string.IsNullOrEmpty(keyword))
                {

                    // tìm kiếm tương đối
                    myModel = myModel.Where(u => u.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || u.By.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

                }

                return Ok(myModel);
            }

            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ ghi log, trả về mã lỗi 500 Internal Server Error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Insert([FromBody] News model)//done
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                model._id = ObjectId.GenerateNewId().ToString();//tạo 1 id mới trong collection
                if (await newsRespository.Exits(model))
                {
                    return Conflict("The record already exists.");
                }
                // Thêm đối tượng vào cơ sở dữ liệu
                await newsRespository.Insert(model);
                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(string id, [FromBody] News model)//done
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                if (await newsRespository.Exits(id))
                {
                    model._id = id;
                    var curent = await newsRespository.GetbyId(id);//dữ liệu bản ghi hiện tại
                    var newmodel = new News()
                    {
                        _id = id,
                        Title = model.Title ?? curent.Title,
                        content = model.content ?? curent.content,
                        By = model.By ?? curent.By,
                       
                    };
                    await newsRespository.Update(newmodel);
                    return StatusCode(202);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string id)//done
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var Sinhvien = new News();
                Sinhvien._id = id;
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                if (await newsRespository.Exits(Sinhvien))
                {
                    await newsRespository.DeletebyId(id);
                    return Ok("deleted");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}