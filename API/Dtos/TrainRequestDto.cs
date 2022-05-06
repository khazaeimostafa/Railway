using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class TrainRequestDto
    {


        [Required]
        public string Name { get; set; } // varchar 50 not null

        [Required]
        public int Capacity { get; set; } // Not Null

        [Required]
        public string Status { get; set; } // 50 Not Null

    }
}
