using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMvvmPattern.Models
{
    public partial class PersonContact : ObservableObject
    {
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _surname;
        [ObservableProperty] private string _email;
        [ObservableProperty] private string _location;
        [ObservableProperty] private string _phoneNumber;
    }
}
