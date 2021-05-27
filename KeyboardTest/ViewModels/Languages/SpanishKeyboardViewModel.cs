namespace FoxHornKeyboard.ViewModels.Languages
{
	public class SpanishKeyboardLayoutViewModel : BaseKeyboardLayoutViewModel
	{
		public SpanishKeyboardLayoutViewModel()
		{
			ActiveKeyMap = SpanishKeyMap;
			ChangeKeys();
		}
	}
}