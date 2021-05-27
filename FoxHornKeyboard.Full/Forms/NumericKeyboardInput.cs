using FoxHornKeyboard.Forms.ViewModels;

namespace FoxHornKeyboard.Forms
{
	public partial class NumericKeyboardInput : BaseOnScreenKeyboardInput
	{
		public NumericKeyboardInput()
		: base(new NumericViewModel())
		{
			InitializeComponent();
		}

		private NumericViewModel MyVm => _viewModel as NumericViewModel;

		public decimal? Value
		{
			get => MyVm.DecimalValue;
			set => MyVm.DecimalValue = value;
		}
	}
}