using ExampleMvvmPattern.Contract;
using ExampleMvvmPattern.Models;
using ExampleMvvmPattern.ViewModels;
using ExampleMvvmPattern.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMvvmPattern.Implementation
{
    public class DialogService : IDialogService
    {
        public bool EditPersonContact(PersonContact aPersonContact)
        {
            var viewModel = new PersonContactViewModel(aPersonContact);
            var window = new EditPersonContactWindow
            {
                DataContext = viewModel
            };

            return window.ShowDialog() == true;
        }
    }
}
