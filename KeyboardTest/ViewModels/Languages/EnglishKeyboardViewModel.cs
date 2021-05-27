namespace FoxHornKeyboard.ViewModels.Languages
{
	public class EnglishKeyboardLayoutViewModel : BaseKeyboardLayoutViewModel
	{
		public EnglishKeyboardLayoutViewModel()
		{
			ActiveKeyMap = EnglishKeyMap;
			ChangeKeys();
		}
	}
}