namespace SADC.API.Helpers
{
    public interface IUtil
    {
        Task<string> SaveImage(IFormFile imageFile, string destiny);
        void DeleteImage(string imageName, string destiny);
    }
}
