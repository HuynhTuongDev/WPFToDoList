using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Category
    {
        public int Id { get; set; } // ID của danh mục

        [Required] // Tên danh mục là bắt buộc
        [StringLength(100)] // Độ dài tối đa của tên danh mục là 100 ký tự
        public string Name { get; set; }

        [StringLength(255)] // Mô tả có thể có tối đa 255 ký tự
        public string Description { get; set; }

        // Danh sách các công việc thuộc danh mục
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
