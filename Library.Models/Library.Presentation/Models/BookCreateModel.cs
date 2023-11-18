using Library.Presentation.Validators;
using System.ComponentModel.DataAnnotations;

namespace Library.Presentation.Models
{
    public class BookCreateModel
    {
        [Required(ErrorMessage ="the field is Required")]
        [MaxLength(500, ErrorMessage ="must not greet than 500")]
        [MinLength(4, ErrorMessage ="Must Be More Than or Equals 4 Chars.")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required,MinLength(30),Display(Name = "Book Description")]
        [DataType(DataType.MultilineText)]
        [MultiLine(Count0Lines = 3, CustomErr ="Must Be Three Lines")]
        public string Description { get; set; }
        [Required,Range(1,500),Display(Name = "Auther Name")]
        public int AuthorID { get; set; }
        [Required,Display(Name = "Please Upload Some Images")]
        public List<IFormFile> Images { get; set; }
    }
}
