using System;
using System.ComponentModel.DataAnnotations;

namespace UploadFilesServer.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public string? ImgPath { get; set; }

        public Boolean? pullFlag { get; set; }

    }
}
