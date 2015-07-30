using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GcmSender.Utils
{
    public class Command : ICommand
    {
        private readonly Action _Action;

        public Command(Action action ) {
            _Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _Action();
        }
    }
}
