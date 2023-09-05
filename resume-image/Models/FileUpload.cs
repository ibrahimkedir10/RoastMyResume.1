namespace resume_image.Models
{
    public class FileUpload
    {
        public IFormFile file { get; set; } // Add this property
        public string User { get; set; }    // Add this property
    }
}
