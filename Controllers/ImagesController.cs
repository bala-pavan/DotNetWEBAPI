using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;
using WebSampleApplicationAPI.Respositories;

namespace WebSampleApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) { 
            this.imageRepository=imageRepository;
        }
        //Post :/api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //convert DTO to DOmain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                //User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel); 

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowed = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowed.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB");
            }
        }
    }
}
