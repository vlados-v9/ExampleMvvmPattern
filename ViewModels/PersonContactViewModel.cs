using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExampleMvvmPattern.Models;

namespace ExampleMvvmPattern.ViewModels
{
    public partial class PersonContactViewModel : ObservableObject
    {
        [ObservableProperty]
        private PersonContact _currentContact;

        private PersonContact _contactTemp;

        public PersonContactViewModel(PersonContact aPersonContact)
        {
            _contactTemp = new PersonContact();

            CurrentContact = aPersonContact;
            changeValue(_contactTemp, CurrentContact);
        }

        [RelayCommand]
        public void Save(object aWindow)
        {
            if(aWindow is System.Windows.Window w)
            {
                w.DialogResult = true;
                w.Close();
            }
        }

        [RelayCommand]
        public void Cancel(object aWindow)
        {
            if (aWindow is System.Windows.Window w)
            {
                changeValue(CurrentContact, _contactTemp);
                w.DialogResult = false;
                w.Close();
            }
        }

        private void changeValue(PersonContact aFirstContact, PersonContact aSecondContact)
        {
            aFirstContact.Name = aSecondContact.Name;
            aFirstContact.Surname = aSecondContact.Surname;
            aFirstContact.Email = aSecondContact.Email;
            aFirstContact.Location = aSecondContact.Location;
            aFirstContact.PhoneNumber = aSecondContact.PhoneNumber;
        }
    }
}
