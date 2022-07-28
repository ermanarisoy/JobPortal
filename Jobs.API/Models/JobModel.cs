using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quality { get; set; }
        public string SideRights { get; set; }
        public string WorkingType { get; set; }
        public decimal Salary { get; set; }
        public string Owner { get; set; }
    }
}
