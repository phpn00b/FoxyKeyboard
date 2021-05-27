using System;
using FoxHornKeyboard.Model;
using FoxHornKeyboard.ViewModels;

namespace FoxHornKeyboard.Forms.ViewModels
{
	public class AutoCompleteViewModel : BaseTypedKeyboardInteractionViewModel<AutoCompleteViewModel.Data>
	{
		private EnumOption _currentValue;
		private GenericCommand FinishCommand { get; }

		public event EventHandler<ItemPickedEventArgs> ValueChanged;
		public AutoCompleteViewModel()
		{
			IsAutoCompleteMode = true;
			FinishCommand = new GenericCommand(o => true, OnFinished);
			FinishedCommand = FinishCommand;
		}

		private void OnFinished(object obj)
		{
			var val = obj as EnumOption;
			if (_currentValue != val)
			{
				OnValueChanged(_currentValue, val);
				_currentValue = val;
				ValueChanged?.Invoke(this, new ItemPickedEventArgs(val));
			}
		}

		protected virtual void OnValueChanged(EnumOption oldValue, EnumOption newValue)
		{

		}

		public class Data
		{
			public string Name { get; set; }
			public int Value { get; set; }
		}

		#region Overrides of BaseTypedKeyboardInteractionViewModel<Data>

		/// <inheritdoc />
		protected override EnumOption Convert(Data arg)
		{
			return new EnumOption
			{
				Name = arg.Name,
				Value = arg.Value
			};
		}

		/// <inheritdoc />
		protected override Data[] LoadServerData(string searchTerm)
		{
			// call the server to get data here
			Data[] items = new Data[10];
			for (int i = 0; i < 10; i++)
			{
				items[i] = new Data
				{
					Name = $"Item {i}",
					Value = i
				};
			}

			return items;
		}

		#endregion
	}
}