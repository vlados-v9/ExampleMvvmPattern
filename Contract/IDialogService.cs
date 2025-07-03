using ExampleMvvmPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleMvvmPattern.Contract
{
    public interface IDialogService
    {
        bool EditPersonContact(PersonContact aPersonContact);
    }
}
