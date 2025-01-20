using System.ComponentModel.DataAnnotations;

namespace MVC23._10._1403.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Length(1, 20, ErrorMessage = "az 10 ta 20 bashe")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ye order bezar")]
        [Range(100,int.MaxValue,ErrorMessage ="min = 100")]
        public int Order { get; set; }
    }
}
