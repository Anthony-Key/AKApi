using System.ComponentModel.DataAnnotations;

namespace AKAPI.Models
{
    public class NationalParkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }
}
