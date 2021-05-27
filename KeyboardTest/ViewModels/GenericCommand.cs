using System;
using System.Windows.Input;

namespace FoxHornKeyboard.ViewModels
{
	public class GenericCommand : ICommand
	{
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _executeAction;

		public event EventHandler CanExecuteChanged;

		public GenericCommand(Predicate<object> canExecute, Action<object> executeAction)
		{
			_canExecute = canExecute;
			_executeAction = executeAction;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute != null)
				return _canExecute(parameter);
			return true;
		}

		public void UpdateCanExecuteState()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Execute(object parameter)
		{
			_executeAction?.Invoke(parameter);
			UpdateCanExecuteState();
		}
	}
}