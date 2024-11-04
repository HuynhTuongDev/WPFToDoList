using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Services;

namespace ToDoList
{
    public partial class CreateTask : Window
    {
        private readonly ITodoService _todoService;
        private readonly ICategoryService _categoryService ; // Thêm dịch vụ danh mục
        private readonly int _userId;

        // Constructor nhận ITodoService và ICategoryService để thực hiện lưu trữ công việc và danh mục.
        public CreateTask(int userID, ITodoService todoService, ICategoryService categoryService)
        {
            InitializeComponent();
            _userId = userID;
            _todoService = todoService; // Khởi tạo ITodoService
            _categoryService = categoryService; // Khởi tạo ICategoryService
            LoadCategories(); 
            LoadTimeComboBoxes();
        }

        private void LoadTimeComboBoxes()
        {
            // Điền giờ từ 1 đến 24
            for (int hour = 1; hour <= 24; hour++)
            {
                HourComboBox.Items.Add(hour);
            }

            // Điền phút từ 0 đến 59
            for (int minute = 0; minute < 60; minute++)
            {
                MinuteComboBox.Items.Add(minute);
            }

            // Đặt giá trị mặc định cho ComboBox
            HourComboBox.SelectedIndex = 0; // Chọn giờ đầu tiên (1)
            MinuteComboBox.SelectedIndex = 0; // Chọn phút đầu tiên (0)
        }

        private async void LoadCategories()
        {
            try
            {
                var categories =  _categoryService.GetCategories();
                if (categories != null && categories.Count > 0)
                {
                    CategoryComboBox.ItemsSource = categories;
                    CategoryComboBox.DisplayMemberPath = "Name"; // Thuộc tính hiển thị
                    CategoryComboBox.SelectedValuePath = "Id"; // Thuộc tính giá trị
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện khi nhấn nút "Save"
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các TextBox và ComboBox
            string title = TitleTextBox.Text;
            string description = DescriptionTextbox.Text;

            // Lấy ngày từ DatePicker
            DateTime dueDate = PeriodDatePicker.SelectedDate ?? DateTime.Now;

            // Lấy giờ và phút từ ComboBox
            int selectedHour = (int)(HourComboBox.SelectedItem ?? 0); // Mặc định là 0 nếu không chọn
            int selectedMinute = (int)(MinuteComboBox.SelectedItem ?? 0); // Mặc định là 0 nếu không chọn

            // Tạo một DateTime từ ngày, giờ và phút
            dueDate = new DateTime(dueDate.Year, dueDate.Month, dueDate.Day, selectedHour, selectedMinute, 0);

            // Kiểm tra nếu tiêu đề công việc trống
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Title is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo một đối tượng Todo mới
            var newTask = new Todo
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                UserId = _userId,
                CategoryId = (int)CategoryComboBox.SelectedValue,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Lưu công việc
            await _todoService.AddTodoAsync(newTask);

            MessageBox.Show("Task saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng cửa sổ hiện tại sau khi lưu
            this.Close();
        }


        // Sự kiện khi nhấn nút "Back"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ hiện tại
            this.Close();
        }

        // Sự kiện khi thay đổi văn bản trong TitleTextBox (nếu cần)
        private void TitleTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            // Bạn có thể thêm logic tại đây nếu cần kiểm tra hoặc xử lý trong quá trình người dùng nhập văn bản.
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
