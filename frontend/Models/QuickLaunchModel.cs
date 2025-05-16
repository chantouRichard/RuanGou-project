using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace frontend.Models
{
    public partial class QuickLaunchModel : ObservableObject
    {
        [ObservableProperty]
        [property: JsonPropertyName("name")]
        private string _name;

        [ObservableProperty]
        [property: JsonPropertyName("path")]
        private string _path;

        [ObservableProperty]
        [property: JsonPropertyName("iconPath")]
        private string _iconPath;

        [ObservableProperty]
        [property: JsonPropertyName("createTime")]
        private DateTime _createTime;

        [ObservableProperty]
        [property: JsonPropertyName("useCount")]
        private int _useCount;

        public QuickLaunchModel()
        {
            CreateTime = DateTime.Now;
            UseCount = 0;
        }

        public QuickLaunchModel(string name, string path, string iconPath = null)
        {
            Name = name;
            Path = path;
            IconPath = iconPath ?? "/frontend;component/Assets/DefaultIcon.png";
            CreateTime = DateTime.Now;
            UseCount = 0;
        }
    }
} 