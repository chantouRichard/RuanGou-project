using frontend.Models;
using frontend.Services;
using ModernWpf.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace frontend.Views.Pages
{
    public partial class MemoPage : UserControl
    {
        private readonly ApiService _apiService;
        private ObservableCollection<TodoItem> _todoItems;
        private int _editingTaskId = -1;

        public MemoPage()
        {
            InitializeComponent();
            DataContext = this;
            _apiService = new ApiService();
            _todoItems = new ObservableCollection<TodoItem>();
            LoadTodos();

            //List<TodoItem> items = new List<TodoItem>();

            //items.Add(new TodoItem() { Title = "test1", Description = "test1", DueDate = DateTime.Now });
            //items.Add(new TodoItem() { Title = "test2", Description = "test2", DueDate = DateTime.Now });
            //items.Add(new TodoItem() { Title = "test3", Description = "test3", DueDate = DateTime.Now });

            //TodoListView.ItemsSource = items;
            TodoListView.ItemsSource = _todoItems;

            // 初始化日期为明天
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);

        }

        private async void LoadTodos()
        {
            Console.WriteLine("加载Todo列表:");
            var result = await _apiService.GetAllTodos();
            Dispatcher.Invoke(() =>
            {
                if (result.Success)
                {
                    _todoItems.Clear();
                    foreach (var item in result.Data.OrderBy(t => t.DueDate))
                    {
                        _todoItems.Add(item);
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            });
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleTextBox.Text))
            {
                ShowErrorMessage("请输入任务标题");
                return;
            }

            if (DueDatePicker.SelectedDate == null)
            {
                MessageBox.Show("请选择截止日期");
                return;
            }

            var newTask = new TodoItem
            {
                Title = TaskTitleTextBox.Text,
                Description = TaskDesciptionTextBox.Text,
                DueDate = DueDatePicker.SelectedDate.Value,
                IsCompleted = false,
                UserId = Properties.Settings.Default.UserId
            };

            try
            {
                var result = await _apiService.CreateTodo(newTask);
                if (result.Success)
                {
                    TaskTitleTextBox.Clear();
                    LoadTodos();
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"添加任务失败: {ex.Message}");
            }
        }

        private async void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int taskId)
            {
                try
                {
                    var result = await _apiService.DeleteTodo(taskId);
                    if (result.Success)
                    {
                        LoadTodos();
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除任务失败: {ex.Message}");
                }
            }
        }

        private async void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int taskId)
            {
                try
                {
                    var result = await _apiService.GetTodoById(taskId);
                    if (result.Success)
                    {
                        _editingTaskId = taskId;
                        EditTitleTextBox.Text = result.Data.Title;
                        EditDescriptionTextBox.Text = result.Data.Description;
                        EditDueDatePicker.SelectedDate = result.Data.DueDate;
                        EditIsCompletedCheckBox.IsChecked = result.Data.IsCompleted;

                        // 显示编辑窗口
                        EditTaskPopup.IsOpen = true;
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"获取任务详情失败: {ex.Message}");
                }
            }
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            EditTaskPopup.IsOpen = false; // 关闭Popup
        }

        private async void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditTitleTextBox.Text))
            {
                MessageBox.Show("请输入任务标题");
                return;
            }

            if (EditDueDatePicker.SelectedDate == null)
            {
                MessageBox.Show("请选择截止日期");
                return;
            }

            var updatedTask = new TodoItem
            {
                Id = _editingTaskId,
                Title = EditTitleTextBox.Text,
                Description = EditDescriptionTextBox.Text,
                DueDate = EditDueDatePicker.SelectedDate.Value,
                IsCompleted = EditIsCompletedCheckBox.IsChecked ?? false,
                UserId = Properties.Settings.Default.UserId
            };

            try
            {
                var result = await _apiService.UpdateTodo(updatedTask);
                if (result.Success)
                {
                    EditTaskPopup.IsOpen = false; // 关闭Popup
                    LoadTodos();
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新任务失败");
            }
        }

        private async void TaskCompleted_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TodoItem task)
            {
                try
                {
                    task.IsCompleted = checkBox.IsChecked ?? false;
                    var result = await _apiService.UpdateTodo(task);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result.Message);
                        // 恢复原状态
                        checkBox.IsChecked = !task.IsCompleted;
                        task.IsCompleted = !task.IsCompleted;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新任务状态失败: {ex.Message}");
                    checkBox.IsChecked = !task.IsCompleted;
                    task.IsCompleted = !task.IsCompleted;
                }
            }
        }

        private async void ClearCompleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var completedTasks = _todoItems.Where(t => t.IsCompleted).ToList();
                bool allSuccess = true;
                string errorMessage = "";

                foreach (var task in completedTasks)
                {
                    var result = await _apiService.DeleteTodo(task.Id);
                    if (!result.Success)
                    {
                        allSuccess = false;
                        errorMessage = result.Message;
                        break;
                    }
                }

                if (allSuccess)
                {
                    LoadTodos();
                }
                else
                {
                    ShowErrorMessage(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"清除已完成任务失败: {ex.Message}");
            }
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            LoadTodos();
        }

        private void ShowErrorMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var dialog = new ContentDialog
                {
                    Title = "错误",
                    Content = message,
                    CloseButtonText = "确定",
                    DefaultButton = ContentDialogButton.Close
                };
                dialog.ShowAsync();
            });
        }
    }
}