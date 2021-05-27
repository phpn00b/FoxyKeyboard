using System;

namespace FoxHornKeyboard.Forms.ViewModels
{
	public class NumericViewModel : BaseWinFormsKeyboardViewModel
	{
		private decimal? _myValue;

		public NumericViewModel()
		{
			KeyboardFormatString = "N2";
			BindingDataType = DataType.Decimal;
			AllowDecimalPoint = true;
			IsNumeric = true;
			IsAutoCompleteMode = false;
		}

		public decimal? DecimalValue
		{
			get => _myValue;
			set
			{
				_myValue = value;
				FormattedKeyboardValue = value?.ToString(KeyboardFormatString);
			}
		}

		#region Overrides of BaseWinFormsKeyboardViewModel

		/// <inheritdoc />
		protected override void OnValueChanged(string oldValue, string newValue)
		{
			DecimalValue = newValue != null ? Convert.ToDecimal(newValue) : default(decimal?);
		}

		#endregion
	}
}