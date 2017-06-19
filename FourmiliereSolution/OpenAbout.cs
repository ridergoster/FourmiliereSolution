using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FourmiliereSolution
{
    public class OpenAbout : ICommand
    {
        public void Execute(object parameter)
        {
            AproposWindow aProposWindow = new AproposWindow();
            aProposWindow.ShowDialog();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
    }
}
