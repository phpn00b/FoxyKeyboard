using System;
using System.Windows.Forms;
using FoxHornKeyboard.Forms.ViewModels;
using FoxHornKeyboard.ViewModels;

namespace FoxHornKeyboard.Forms
{
	public abstract partial class BaseOnScreenKeyboardInput : UserControl
	{
		public event EventHandler<EventArgs> ValueChanged;

		protected readonly BaseKeyboardInteractionViewModel _viewModel;

		protected BaseOnScreenKeyboardInput(BaseKeyboardInteractionViewModel viewModel)
		{
			_viewModel = viewModel;
			InitializeComponent();

			winFormsTextInputWithOnscreenKeyboard1.DataContext = _viewModel;
			if (viewModel is BaseWinFormsKeyboardViewModel vm)
			{
				vm.ValueChanged += (s, e) =>
				{
					ValueChanged?.Invoke(this, EventArgs.Empty);
				};
			}
		}

		public bool AllowDecimalPoint
		{
			get => _viewModel.AllowDecimalPoint;
			set => _viewModel.AllowDecimalPoint = value;
		}

		public string FormattedKeyboardValue
		{
			get => _viewModel.FormattedKeyboardValue;
			set => _viewModel.FormattedKeyboardValue = value;
		}

		public bool IsAutoCompleteMode
		{
			get => _viewModel.IsAutoCompleteMode;
			set => _viewModel.IsAutoCompleteMode = value;
		}

		public bool AutoClearOnOpen
		{
			get => _viewModel.AutoClearOnOpen;
			set => _viewModel.AutoClearOnOpen = value;
		}

		public BaseKeyboardInteractionViewModel.DataType BindingDataType
		{
			get => _viewModel.BindingDataType;
			set => _viewModel.BindingDataType = value;
		}

		public bool IsNumeric
		{
			get => _viewModel.IsNumeric;
			set => _viewModel.IsNumeric = value;
		}

		public bool IsOpen
		{
			get => _viewModel.IsOpen;
			set => _viewModel.IsOpen = value;
		}
	}
}