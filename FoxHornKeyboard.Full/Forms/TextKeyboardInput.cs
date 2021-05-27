using FoxHornKeyboard.Forms.ViewModels;

namespace FoxHornKeyboard.Forms
{
	public partial class TextKeyboardInput : BaseOnScreenKeyboardInput
	{
		public TextKeyboardInput()
		: base(new TextViewModel())
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