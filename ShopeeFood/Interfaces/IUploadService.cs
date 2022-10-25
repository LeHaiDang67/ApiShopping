using ShopeeFood.Dtos.UploadFile;

namespace ShopeeFood.Interfaces
{
	public interface IUploadService
	{
		public void Base64ToFile(UploadRequestDTO request);
		public string GetImgFromLocal(string nameImg);
	}
}
