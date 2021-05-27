using FoxHornKeyboard.Model;
using System;

namespace FoxHornKeyboard.Forms.ViewModels
{
	public class ItemPickedEventArgs : EventArgs
	{
		public EnumOption Item { get; }

		public ItemPickedEventArgs(EnumOption item)
		{
			Item = item;
		}
	}
}