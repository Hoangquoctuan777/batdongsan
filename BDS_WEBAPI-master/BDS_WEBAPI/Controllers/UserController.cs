using BDS_WEBAPI.IRespository;
using BDS_WEBAPI.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BDS_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRespository userRespository;
        public UserController (IUserRespository _I)
        {
            userRespository = _I;
        }



        [HttpGet]
        public async Task<ActionResult<Users>> GetALL(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            try
            {
                
                var myModel = await userRespository.GetAll();
                //Lọc theo user và fulluser
                if (!string.IsNullOrEmpty(keyword))
                {
                    //tìm kiếm tuyệt đối
                   // myModel = myModel.Where(u => u.Username.Contains(keyword) || u.Fullname.Contains(keyword));
                   // tìm kiếm tương đối
                    myModel = myModel.Where(u => u.Username.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || u.Fullname.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);

                }
                //phân trang 
                myModel = myModel.Skip(offset).Take(limit);
                
                return Ok(myModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ ghi log, trả về mã lỗi 500 Internal Server Error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Getbyid")]
        public async Task<ActionResult<Users>> GetbyId(string id)//done
        {
            try
            {
                var myModel = await userRespository.GetbyId(id);

                if (myModel == null)
                {
                    return NotFound();
                }

                return Ok(myModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ ghi log, trả về mã lỗi 500 Internal Server Error
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetbyUsername")]
        public async Task<ActionResult<Users>> GetbyUsername(string username)//done
        {
            try
            {
                var myModel = await userRespository.GetbyUsername(username);
                if (myModel == null)
                {
                    return NotFound();
                }
                return Ok(myModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ ghi log, trả về mã lỗi 500 Internal Server Error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Insert([FromBody] Users model)//done
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                model._id = ObjectId.GenerateNewId().ToString();//tạo 1 id mới trong collection
                if (await userRespository.Exits(model))
                {
                    return Conflict("The record already exists.");
                }
                // Thêm đối tượng vào cơ sở dữ liệu
                await userRespository.Insert(model);
                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(string id,[FromBody] Users model)//done
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                if (await userRespository.Exits(id))
                {
                    model._id= id;
                    var curent= await userRespository.GetbyId(id);//dữ liệu bản ghi hiện tại
                    var newmodel = new Users()
                    {
                        _id= id,
                        Username=curent.Username,
                        Email=model.Email??curent.Email,
                        Phone=model.Phone??curent.Phone,
                        Password=model.Password??curent.Password,
                        CreateDate=curent.CreateDate,
                        Fullname=model.Fullname??curent.Fullname,
                        Role=curent.Role
                    };
                    await userRespository.Update(newmodel);
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
                var Sinhvien = new Users();
                Sinhvien._id = id;
                // Kiểm tra xem đối tượng đã tồn tại trong cơ sở dữ liệu hay chưa
                if (await userRespository.Exits(Sinhvien))
                {
                    await userRespository.DeletebyId(id);
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
