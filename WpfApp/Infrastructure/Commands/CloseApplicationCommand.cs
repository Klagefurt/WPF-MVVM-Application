using System.Windows;
using WpfApp.Infrastructure.Commands.Base;

namespace WpfApp.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) => Application.Current.Shutdown();
    }
}
