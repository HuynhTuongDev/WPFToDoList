using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Todo
    {
        public int Id { get; set; } // ID của công việc

        [Required] // Tiêu đề là bắt buộc
        [StringLength(100)] // Độ dài tối đa của tiêu đề là 100 ký tự
        public string Title { get; set; }

        [StringLength(255)] // Độ dài tối đa của mô tả là 255 ký tự
        public string Description { get; set; }

        public DateTime DueDate { get; set; } // Thời hạn công việc
        public bool IsCompleted { get; set; } // Trạng thái hoàn thành

        public DateTime CreatedAt { get; set; } // Thời điểm tạo công việc
        public DateTime UpdatedAt { get; set; } // Thời điểm cập nhật công việc

        public int CreatedBy { get; set; } // Tên của người tạo công việc

        // Khóa ngoại liên kết đến User
        public int UserId { get; set; } // ID của người dùng

        [Required] // User là bắt buộc
        public User User { get; set; }

        // Khóa ngoại liên kết đến Category
        public int CategoryId { get; set; } // ID của danh mục

        [Required] // Category là bắt buộc
        public Category Category { get; set; }
    }
}
