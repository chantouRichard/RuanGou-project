using frontend.Models;
using frontend.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace frontend.ViewModels
{
    public class FloatingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> ToDoItems { get; set; } = new ObservableCollection<TodoItem>();

        private ApiService _apiService = new ApiService();
        public ICollectionView SortedToDoItems { get; set; }

        private DispatcherTimer _timer = new DispatcherTimer();
        public FloatingViewModel()
        {
            //定时器
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            //创建排序视图
            SortedToDoItems = CollectionViewSource.GetDefaultView(ToDoItems);
            SortedToDoItems.SortDescriptions.Add(new SortDescription(nameof(TodoItem.IsCompleted), ListSortDirection.Ascending)); // 未完成在前
            SortedToDoItems.SortDescriptions.Add(new SortDescription(nameof(TodoItem.IsOverdue), ListSortDirection.Descending));  // 已到期在前
            SortedToDoItems.SortDescriptions.Add(new SortDescription(nameof(TodoItem.RemainingSeconds), ListSortDirection.Ascending)); // 剩余时间短在前

            ToDoItems.CollectionChanged += ToDoItems_CollectionChanged;
        }

        private void ToDoItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (TodoItem item in e.NewItems)
                    item.PropertyChanged += TodoItem_PropertyChanged;

            if (e.OldItems != null)
                foreach (TodoItem item in e.OldItems)
                    item.PropertyChanged -= TodoItem_PropertyChanged;
        }

        private async void TodoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsCompleted))
            {
                var todo = sender as TodoItem;
                if (todo == null) return;

                try
                {
                    await _apiService.UpdateTodo(todo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存失败: {ex.Message}");
                }
            }
        }
        public async Task LoadTodos()
        {
            try
            {
                var response = await _apiService.GetAllTodos();

                if (response != null && response.Success)
                {
                    // 当前上下文是 UI 线程，可直接操作
                    ToDoItems.Clear();
                    foreach (var item in response.Data)
                    {
                        ToDoItems.Add(item);
                    }
                }

                else
                {
                    Console.WriteLine("获取待办列表失败：" + response?.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("请求失败：" + ex.Message);
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await LoadTodos(); //5s刷新一次待办事项列表
        }

        public async Task Refresh()
        {
            try
            {
                // 获取已完成的任务
                var completedTasks = ToDoItems.Where(t => t.IsCompleted).ToList();

                if (completedTasks.Count == 0)
                {
                    return;
                }

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
                    await LoadTodos();
                }
                else
                {
                    //ShowErrorMessage($"清理失败: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                //ShowErrorMessage($"清除已完成任务失败: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
