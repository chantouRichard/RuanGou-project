using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Models
{
    public class TodoItem: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }


        private bool _isCompleted = false;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusText));
                    OnPropertyChanged(nameof(IsOverdue));
                    
                }
            }
        }



        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        // 如果需要用户关联
        public string UserId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 剩余时间属性
        public string RemainingTime => GetRemainingTime();
        private string GetRemainingTime()
        { 
             var now = DateTime.Now;
             var timeLeft = DueDate - now;

             if (timeLeft.TotalSeconds <= 0)
                    return "已到期";

             return $"{(int)timeLeft.TotalDays}天{timeLeft.Hours}时{timeLeft.Minutes}分";
        }

        public void UpdateRemainingTime()
        {
            OnPropertyChanged(nameof(RemainingTime));
        }

     

        // 新增状态文本属性
        public string StatusText
        {
            get
            {
                return IsCompleted ? "已完成" : "未完成";
            }
        }
        public bool IsOverdue => !IsCompleted && DueDate < DateTime.Now;

        public int RemainingSeconds
        {
            get
            {
                var diff = DueDate - DateTime.Now;
                return diff.TotalSeconds > 0 ? (int)diff.TotalSeconds : 0;
            }
        }


    }
}
