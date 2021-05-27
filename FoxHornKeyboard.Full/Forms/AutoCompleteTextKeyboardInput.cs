using FoxHornKeyboard.Forms.ViewModels;

namespace FoxHornKeyboard.Forms
{
	public partial class AutoCompleteTextKeyboardInput : BaseOnScreenKeyboardInput
	{
		public AutoCompleteTextKeyboardInput()
		: base(new AutoCompleteViewModel())
		{
			InitializeComponent();
		}

		public string TextInput
		{
			get => _viewModel.FormattedKeyboardValue;
			set => _viewModel.FormattedKeyboardValue = value;
		}
	}
}