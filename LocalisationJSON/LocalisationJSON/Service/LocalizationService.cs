using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace LocalisationJSON.Service;


public class LocalizationService : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, Dictionary<string, string>> _cache = new();
        private Dictionary<string, string> _localizedStrings;
        private string _currentLanguage = "ar-SA";

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationService()
        {
            LoadLocalization(_currentLanguage);
        }

        public string CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage == value) return;
                _currentLanguage = value;
                LoadLocalization(value);
                OnPropertyChanged(nameof(CurrentLanguage));
                OnPropertyChanged(nameof(Greeting));
            }
        }

        private void LoadLocalization(string languageCode)
        {
            if (_cache.ContainsKey(languageCode))
            {
                _localizedStrings = _cache[languageCode];
                return;
            }

            string filePath = Path.Combine(AppContext.BaseDirectory, "Locales", $"{languageCode}.json");
            Console.WriteLine(filePath);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (translations != null)
                {
                    _localizedStrings = translations;
                    _cache[languageCode] = translations; 
                }
            }
            else
            {
                Console.WriteLine("File not found");
                return;
            }
        }

        public string GetString(string key)
        {
            return _localizedStrings != null && _localizedStrings.ContainsKey(key) ? _localizedStrings[key] : key;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Example binding property
        public string Greeting => GetString("Greeting");
    }