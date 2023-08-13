using WebSampleApplicationAPI.Models.Domain;

namespace WebSampleApplicationAPI.Respositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
