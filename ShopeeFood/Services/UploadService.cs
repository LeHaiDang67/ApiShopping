using ShopeeFood.Dtos.UploadFileDTO;
using ShopeeFood.Interfaces;
using System.Drawing.Imaging;

namespace ShopeeFood.Services
{
	public class UploadService:IUploadService
	{

		public void Base64ToFile(UploadRequestDTO request)
		{
			var replaceImg = request.ImageBase64.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", "");
			//var replaceImg = request.ImageBase64.Replace("data:image/jpeg;base64,", string.Empty);
			var DefaultImagePath = "D:\\Images\\uploads\\img\\";
			byte[] imageBytes = Convert.FromBase64String(replaceImg);
			var pathType = request.Name + ".png";
			System.Drawing.Image image;
			using (MemoryStream ms = new MemoryStream(imageBytes))
			{
				using (System.Drawing.Image pic = System.Drawing.Image.FromStream(ms, true))
				{
					pic.Save(DefaultImagePath + pathType, ImageFormat.Png);
				}
			}

		}

		public string GetImgFromLocal(string nameImg)
		{
			var DefaultImagePath = "D:\\Images\\uploads\\img\\";
			var path = DefaultImagePath + nameImg + ".png";
			if (File.Exists(path))
			{
				byte[] imageArray = System.IO.File.ReadAllBytes(path);
				string base64ImageRepresentation = Convert.ToBase64String(imageArray);
				return base64ImageRepresentation;
			} else
			{
				return string.Empty;
			}
			

			
		}
	}
}
