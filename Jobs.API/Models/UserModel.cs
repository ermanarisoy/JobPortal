using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Models
{
    public class UserModel
    {
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public int RightToPublish { get; set; }
    }
}
