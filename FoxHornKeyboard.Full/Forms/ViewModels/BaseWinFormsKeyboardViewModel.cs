using FoxHornKeyboard.ViewModels;
using System;

namespace FoxHornKeyboard.Forms.ViewModels
{
	public abstract class BaseWinFormsKeyboardViewModel : BaseKeyboardInteractionViewModel
	{
		private string _currentValue;
		private GenericCommand FinishCommand { get; }

		public event EventHandler<EventArgs> ValueChanged;

		protected BaseWinFormsKeyboardViewModel()
		{
			FinishCommand = new GenericCommand(o => true, OnFinished);
			FinishedCommand = FinishCommand;
		}

		private void OnFinished(object obj)
		{
			string val = obj as string;
			if (_currentValue != val)
			{
				OnValueChanged(_currentValue, val);
				_currentValue = val;
				ValueChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		protected abstract void OnValueChanged(string oldValue, string newValue);

		#region Overrides of BaseKeyboardInteractionViewModel

		/// <inheritdoc />
		protected override void OnRunSearch(string searchTerm)
		{
			// not used
		}

		#endregion
	}
}