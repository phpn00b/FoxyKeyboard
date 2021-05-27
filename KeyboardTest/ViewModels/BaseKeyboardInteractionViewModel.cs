using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FoxHornKeyboard.Model;
using FoxHornKeyboard.ViewModels.Languages;

namespace FoxHornKeyboard.ViewModels
{
	public abstract class BaseKeyboardInteractionViewModel : BaseViewModel
	{
		public EnglishKeyboardLayoutViewModel KeyboardViewModel { get; } = new EnglishKeyboardLayoutViewModel();
		public GenericCommand InternalFinishedCommand { get; }
		protected BaseKeyboardInteractionViewModel()
		{
			KeyboardOpenCommand = new GenericCommand(o => true, OpenKeyboard);
			InternalFinishedCommand = new GenericCommand(o => true, OnInternalFinish);
			KeyboardViewModel.PropertyChanged += (s, e) =>
			{
				switch (e.PropertyName)
				{
					case "Text":
						KeyboardValue = KeyboardViewModel.Text;
						break;
				}
			};
		}

		public void RunSearch(string searchTerm)
		{
			OnRunSearch(searchTerm);
		}

		protected abstract void OnRunSearch(string searchTerm);

		private void OnInternalFinish(object obj)
		{
			if (IsAutoCompleteMode)
			{
				var item = obj as EnumOption;
				KeyboardValue = item.Value as string;
				FormattedKeyboardValue = item.Name.Replace("\r", "").Replace("\n", "");
			}
			else
			{
				KeyboardValue = obj as string;
			}
			FinishedCommand?.Execute(obj);
		}

		private void OpenKeyboard(object obj)
		{
			IsOpen = !IsOpen;

			if (IsOpen)
				if (AutoClearOnOpen)
				{
					KeyboardViewModel.Text = "";
					KeyboardViewModel.InputIndex = 0;
				}
			if (KeyboardViewModel.Text != null)
				KeyboardViewModel.InputIndex = KeyboardViewModel.Text.Length - 1;
		}

		public GenericCommand KeyboardOpenCommand { get; }

		public enum DataType
		{
			String,
			Integer,
			Long,
			Decimal
		}

		private void SetFormattedValue()
		{
			object val = BoundValue;
			switch (BindingDataType)
			{
				case DataType.Integer:
					if (!string.IsNullOrWhiteSpace(KeyboardFormatString))
						FormattedKeyboardValue = ((int)val).ToString(KeyboardFormatString);
					else
						FormattedKeyboardValue = val.ToString();
					break;
				case DataType.Long:
					if (!string.IsNullOrWhiteSpace(KeyboardFormatString))
						FormattedKeyboardValue = ((long)val).ToString(KeyboardFormatString);
					else
						FormattedKeyboardValue = val.ToString();
					break;
				case DataType.Decimal:
					if (!string.IsNullOrWhiteSpace(KeyboardFormatString))
						FormattedKeyboardValue = ((decimal)val).ToString(KeyboardFormatString);
					else
						FormattedKeyboardValue = val.ToString();
					break;
			}
		}

		#region BindingDataType Property

		private DataType _bindingDataType;

		public DataType BindingDataType
		{
			get { return _bindingDataType; }
			set
			{
				if (_bindingDataType != value)
				{
					_bindingDataType = value;
					OnPropertyChanged("BindingDataType");
				}
			}
		}

		#endregion

		#region BoundValue Property

		public object BoundValue
		{
			get
			{
				switch (BindingDataType)
				{
					case DataType.String:
						return KeyboardValue;
					case DataType.Integer:
						try
						{ return System.Convert.ToInt32(KeyboardValue); }
						catch (FormatException) { }
						return 0;
					case DataType.Long:
						try
						{ return System.Convert.ToInt64(KeyboardValue); }
						catch (FormatException) { }
						return 0;
					case DataType.Decimal:
						try
						{ return System.Convert.ToDecimal(KeyboardValue); }
						catch (FormatException) { }
						return 0M;
					default:
						return "";
				}
			}
		}

		#endregion

		#region KeyboardValue Property

		private string _keyboardValue;

		public string KeyboardValue
		{
			get { return _keyboardValue; }
			set
			{
				if (_keyboardValue != value)
				{
					_keyboardValue = value;
					OnPropertyChanged("BoundValue");
					OnPropertyChanged("KeyboardValue");
					SetFormattedValue();
				}
			}
		}

		#endregion

		#region FormattedKeyboardValue Property

		private string _formattedKeyboardValue;

		public string FormattedKeyboardValue
		{
			get { return _formattedKeyboardValue; }
			set
			{
				if (_formattedKeyboardValue != value)
				{
					_formattedKeyboardValue = value;
					OnPropertyChanged("FormattedKeyboardValue");
				}
			}
		}

		#endregion

		#region KeyboardFormatString Property

		private string _keyboardFormatString;

		public string KeyboardFormatString
		{
			get { return _keyboardFormatString; }
			set
			{
				if (_keyboardFormatString != value)
				{
					_keyboardFormatString = value;
					OnPropertyChanged("KeyboardFormatString");
				}
			}
		}

		#endregion

		#region AutoClearOnOpen Property

		private bool _autoClearOnOpen;

		public bool AutoClearOnOpen
		{
			get { return _autoClearOnOpen; }
			set
			{
				if (_autoClearOnOpen != value)
				{
					_autoClearOnOpen = value;
					OnPropertyChanged("AutoClearOnOpen");
				}
			}
		}

		#endregion

		#region LoadAutoCompleteData Property

		private ICommand _loadAutoCompleteData;

		public ICommand LoadAutoCompleteData
		{
			get { return _loadAutoCompleteData; }
			set
			{
				if (_loadAutoCompleteData != value)
				{
					_loadAutoCompleteData = value;
					OnPropertyChanged("LoadAutoCompleteData");
				}
			}
		}

		#endregion

		#region IsAutoCompleteMode Property

		private bool _isAutoCompleteMode;

		public bool IsAutoCompleteMode
		{
			get { return _isAutoCompleteMode; }
			set
			{
				if (_isAutoCompleteMode != value)
				{
					_isAutoCompleteMode = value;
					OnPropertyChanged("IsAutoCompleteMode");
				}
			}
		}

		#endregion

		#region FinishedCommand Property

		private ICommand _finishedCommand;

		public ICommand FinishedCommand
		{
			get { return _finishedCommand; }
			set
			{
				if (_finishedCommand != value)
				{
					_finishedCommand = value;
					OnPropertyChanged("FinishedCommand");
				}
			}
		}

		#endregion

		#region AllowDecimalPoint Property

		private bool _allowDecimalPoint;

		public bool AllowDecimalPoint
		{
			get { return _allowDecimalPoint; }
			set
			{
				if (_allowDecimalPoint != value)
				{
					_allowDecimalPoint = value;
					OnPropertyChanged("AllowDecimalPoint");
				}
			}
		}

		#endregion

		#region IsNumeric Property

		private bool _isNumeric;

		public bool IsNumeric
		{
			get { return _isNumeric; }
			set
			{
				if (_isNumeric != value)
				{
					_isNumeric = value;
					OnPropertyChanged("IsNumeric");
				}
			}
		}

		#endregion

		#region IsOpen Property

		private bool _isOpen;

		public bool IsOpen
		{
			get { return _isOpen; }
			set
			{
				if (_isOpen != value)
				{
					_isOpen = value;
					OnPropertyChanged("IsOpen");
				}
			}
		}

		#endregion

		public void ProcessMouseOrTouch()
		{
			new Task(() =>
			{
				Thread.Sleep(100);
				UiDispatcher.Invoke(() => IsOpen = true);
			}).Start();
			//Popup.IsOpen = true;
			//e.Handled = true;
			//Popup.Focus();
			if (KeyboardValue != null)
				KeyboardViewModel.InputIndex = KeyboardViewModel.Text.Length - 1;
			if (!IsOpen)
				if (AutoClearOnOpen)
				{
					KeyboardValue = "";
					KeyboardViewModel.Text = "";
					KeyboardViewModel.InputIndex = 0;
					//KeyboardViewModel.ShowKeyboard(null);
				}
		}
	}
}