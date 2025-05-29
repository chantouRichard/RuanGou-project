using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using NHotkey;
using NHotkey.Wpf;

namespace frontend.Views
{
    public class HotkeySetting
    {
        public string ActionName { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public string? CustomPath { get; set; } = null;
        public string Key { get; set; } = "None";
        public string Modifiers { get; set; } = "None";
    }

    public partial class HotkeyBindingWindow : Page
    {
        private const string HotkeyConfigPath = "hotkeys.json";
        private List<HotkeySetting> _hotkeySettings = new();

        [DllImport("PowrProf.dll", SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        public HotkeyBindingWindow()
        {
            InitializeComponent();
            CustomPathBox.Visibility = Visibility.Collapsed;
            BrowseButton.Visibility = Visibility.Collapsed;
            LoadHotkeySettings();
        }


        private void ActionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ActionComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var visible = selected == "自定义程序" ? Visibility.Visible : Visibility.Collapsed;
            CustomPathBox.Visibility = visible;
            BrowseButton.Visibility = visible;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                CustomPathBox.Text = dlg.FileName;
            }
        }

        private void LoadHotkeySettings()
        {
            if (File.Exists(HotkeyConfigPath))
            {
                var json = File.ReadAllText(HotkeyConfigPath);
                _hotkeySettings = JsonConvert.DeserializeObject<List<HotkeySetting>>(json) ?? new();
                foreach (var setting in _hotkeySettings)
                {
                    if (Enum.TryParse(setting.Key, out Key key) &&
                        Enum.TryParse(setting.Modifiers, out ModifierKeys modifiers))
                    {
                        try
                        {
                            HotkeyManager.Current.AddOrReplace(setting.ActionName, key, modifiers, (s, e) =>
                            {
                                ExecuteAction(setting);
                                e.Handled = true;
                            });
                        }
                        catch { /* 跳过已注册异常 */ }
                    }
                }
                HotkeyListView.ItemsSource = null;
                HotkeyListView.ItemsSource = _hotkeySettings;
            }
        }

        private void BindButton_Click(object sender, RoutedEventArgs e)
        {
            var actionName = ActionNameBox.Text.Trim();
            if (string.IsNullOrEmpty(actionName)) return;

            string actionType = ((ComboBoxItem)ActionComboBox.SelectedItem)?.Content?.ToString() switch
            {
                "关机" => "Shutdown",
                "重启" => "Restart",
                "待机" => "Sleep",
                "自定义程序" => "Custom",
                _ => "Custom"
            };

            string? path = actionType == "Custom" ? CustomPathBox.Text.Trim() : null;

            ModifierKeys modifiers = ModifierKeys.None;
            if (CtrlCheck.IsChecked == true) modifiers |= ModifierKeys.Control;
            if (AltCheck.IsChecked == true) modifiers |= ModifierKeys.Alt;
            if (ShiftCheck.IsChecked == true) modifiers |= ModifierKeys.Shift;
            if (WinCheck.IsChecked == true) modifiers |= ModifierKeys.Windows;

            if (KeyComboBox.SelectedItem is not ComboBoxItem selectedKeyItem ||
                !Enum.TryParse<Key>(selectedKeyItem.Content.ToString(), out var key))
                return;

            var setting = new HotkeySetting
            {
                ActionName = actionName,
                ActionType = actionType,
                CustomPath = path,
                Key = key.ToString(),
                Modifiers = modifiers.ToString()
            };

            try
            {
                HotkeyManager.Current.AddOrReplace(actionName, key, modifiers, (s, eArgs) =>
                {
                    ExecuteAction(setting);
                    eArgs.Handled = true;
                });

                _hotkeySettings.RemoveAll(h => h.ActionName == actionName);
                _hotkeySettings.Add(setting);
                File.WriteAllText(HotkeyConfigPath, JsonConvert.SerializeObject(_hotkeySettings, Formatting.Indented));

                HotkeyListView.ItemsSource = null;
                HotkeyListView.ItemsSource = _hotkeySettings;

                MessageBox.Show($"绑定成功：{modifiers} + {key} → {actionName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"绑定失败：{ex.Message}");
            }
        }

        private void DeleteHotkey_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string actionName)
            {
                HotkeyManager.Current.Remove(actionName);
                _hotkeySettings.RemoveAll(h => h.ActionName == actionName);
                File.WriteAllText(HotkeyConfigPath, JsonConvert.SerializeObject(_hotkeySettings, Formatting.Indented));

                HotkeyListView.ItemsSource = null;
                HotkeyListView.ItemsSource = _hotkeySettings;
            }
        }

        private void ExecuteAction(HotkeySetting setting)
        {
            switch (setting.ActionType)
            {
                case "Shutdown":
                    if (MessageBox.Show("确定要关机吗？", "确认操作", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") { UseShellExecute = false });
                    }
                    break;

                case "Restart":
                    if (MessageBox.Show("确定要重启吗？", "确认操作", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("shutdown", "/r /t 0") { UseShellExecute = false });
                    }
                    break;

                case "Sleep":
                    if (MessageBox.Show("确定要进入睡眠模式吗？", "确认操作", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SetSuspendState(false, true, true);
                    }
                    break;

                case "Custom":
                    if (!string.IsNullOrWhiteSpace(setting.CustomPath))
                    {
                        try
                        {
                            Process.Start(setting.CustomPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"启动失败：{ex.Message}");
                        }
                    }
                    break;
            }
        }

    }
}
