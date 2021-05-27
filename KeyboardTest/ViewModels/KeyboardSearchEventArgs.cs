using System;

namespace FoxHornKeyboard.ViewModels
{
	public class KeyboardSearchEventArgs : EventArgs
	{
		public string Term { get; }

		public KeyboardSearchEventArgs(string term)
		{
			Term = term;
		}
	}
}