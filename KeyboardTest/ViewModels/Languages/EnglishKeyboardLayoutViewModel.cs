using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxHornKeyboard.ViewModels.Languages
{
	public class EnglishKeyboardLayoutViewModel : BaseKeyboardLayoutViewModel
	{
		public EnglishKeyboardLayoutViewModel()
		{
			//KeyPressCommand = new GenericCommand(o => true, PressKey);
			ActiveKeyMap = EnglishKeyMap;
			ChangeKeys();
			KeyPressCommand.UpdateCanExecuteState();
		}
	}
}
