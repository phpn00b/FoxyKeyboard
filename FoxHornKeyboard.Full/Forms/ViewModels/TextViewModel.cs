namespace FoxHornKeyboard.Forms.ViewModels
{
	public class TextViewModel : BaseWinFormsKeyboardViewModel
	{
		private string _myValue;

		public TextViewModel()
		{
			BindingDataType = DataType.String;
			AllowDecimalPoint = true;
			IsNumeric = false;
			IsAutoCompleteMode = false;
		}

		/// <inheritdoc />
		protected override void OnValueChanged(string oldValue, string newValue)
		{
			Text = newValue;
		}

		public string Text
		{
			get => _myValue;
			set
			{
				_myValue = value;
				FormattedKeyboardValue = value;
			}
		}

		#region Overrides of BaseKeyboardInteractionViewModel

		/// <inheritdoc />
		protected override void OnRunSearch(string searchTerm)
		{
			// not used
		}

		#endregion
	}
}