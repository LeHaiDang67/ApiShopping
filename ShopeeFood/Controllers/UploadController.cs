using Microsoft.AspNetCore.Mvc;
using ShopeeFood.Dtos.UploadFile;
using ShopeeFood.Interfaces;
using System.Drawing.Imaging;

namespace ShopeeFood.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UploadController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUploadService _iUploadService;
		public UploadController(IWebHostEnvironment webHostEnvironment, IUploadService iUploadService)
		{
			_webHostEnvironment = webHostEnvironment;
			_iUploadService = iUploadService;
		}

		private string IformToFile(IFormFile thefile)
		{

			var imgProduct = string.Empty;
			string uploadPath = "uploads/img";
			var files = HttpContext.Request.Form.Files;
			foreach (var file in files)
			{
				if (file != null && file.Length > 0)
				{
					var fileName = thefile.FileName;
					var uploadPathWithfileName = Path.Combine(uploadPath, fileName);
					var uploadAbsolutePath = Path.Combine("D:\\Images", uploadPathWithfileName);
					using (var fileStream = new FileStream(uploadAbsolutePath, FileMode.Create))
					{
						file.CopyTo(fileStream);
						imgProduct = uploadPathWithfileName;
					}
				}
			}
			return imgProduct;
		}


		[HttpPost]
		[Route("UploadImg")]
		public async Task<ActionResult> UploadImg(UploadRequestDTO request)
		{
			if (request is null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "File is null"
				});
			}
			//var bytes = Convert.FromBase64String(request.ImageBase64);
			_iUploadService.Base64ToFile(request);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		} 

		[HttpPost]
		[Route("GetImg")]
		public async Task<ActionResult> GetImage(string NameImg)
		{

			if(NameImg == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Name image is null"
				});
			}
			var img = _iUploadService.GetImgFromLocal(NameImg);
			return Ok(new
			{
				Success = true,
				Data = img,
				Message = "Success"
			});
		}
	}
}
