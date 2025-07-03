using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExampleMvvmPattern.Contract;
using ExampleMvvmPattern.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;


namespace ExampleMvvmPattern.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        private ObservableCollection<PersonContact> _allContact = new();

        [ObservableProperty]
        private ObservableCollection<PersonContact> _personContacts = new();

        [ObservableProperty]
        private PersonContact? _selectedPersonC;

        [ObservableProperty]
        private string? filterText;

        public MainWindowViewModel(IDialogService aDialogService)
        {
            _dialogService = aDialogService ?? throw new ArgumentNullException(nameof(aDialogService));
        }

        [RelayCommand]
        private void SaveToJson()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            var pathToFile = dialog.ShowDialog() == true ? dialog.FileName : null;

            if(pathToFile is not null)
            {
                var json = JsonSerializer.Serialize(_allContact, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(pathToFile, json);
            }
        }

        [RelayCommand]
        private void LoadFromFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                CheckFileExists = true                
            };

            var pathToFile = dialog.ShowDialog() == true ? dialog.FileName : null;

            if (pathToFile is not null)
            {
                var json = File.ReadAllText(pathToFile);
                var personContacts = JsonSerializer.Deserialize<ObservableCollection<PersonContact>>(json);
                if (personContacts != null)
                {
                    PersonContacts = personContacts;
                    _allContact = new ObservableCollection<PersonContact>(personContacts);
                }
            }  
        }

        [RelayCommand]
        private void AddPerson()
        {
            var newPersonContact = new PersonContact();
            if (_dialogService.EditPersonContact(newPersonContact))
            {
                _allContact.Add(newPersonContact);
                applyFilter();
            }
        }

        [RelayCommand]
        private void EditPerson()
        {
            if(SelectedPersonC != null)
            {
                var index = _allContact.IndexOf(SelectedPersonC);
                var editPersonC = new PersonContact
                {
                    Name = SelectedPersonC.Name,
                    Surname = SelectedPersonC.Surname,
                    Email = SelectedPersonC.Email,
                    Location = SelectedPersonC.Location,
                    PhoneNumber = SelectedPersonC.PhoneNumber,
                };

                if(_dialogService.EditPersonContact(editPersonC))
                {
                    _allContact[index] = editPersonC;
                    applyFilter();
                }
            }
        }

        [RelayCommand]
        private void RemovePerson()
        {
            if(SelectedPersonC != null)
            {
                _allContact.Remove(SelectedPersonC);
                applyFilter();
            }
        }

        private void applyFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                PersonContacts = new ObservableCollection<PersonContact>(_allContact);
            }
            else
            {
                var filtered = _allContact.Where(s =>
                    (!string.IsNullOrEmpty(s.Name) && s.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.Surname) && s.Surname.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.PhoneNumber) && s.PhoneNumber.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.Location) && s.Location.Contains(FilterText, StringComparison.OrdinalIgnoreCase))
                );
                PersonContacts = new ObservableCollection<PersonContact>(filtered);
            }
        }

        partial void OnFilterTextChanged(string aValue) => applyFilter();
    }
}
