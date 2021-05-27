namespace FoxHornKeyboard
{
	public static class Extensions
	{
		private const char Space = ' ';

		/// <summary>
		/// Used to transform a pascal case string (default casing that c# uses) 
		/// to human readable ie with spaces on every capital letter
		/// </summary>
		/// <param name="source">pascal case string</param>
		/// <returns></returns>
		public static string PascalToHuman(this string source)
		{
			if (source == null)
				return null;
			string human = "";
			char[] characters = source.ToCharArray();
			for (int i = 0; i < characters.Length; i++)
			{
				char c = characters[i];
				if (char.IsUpper(c) && i != 0)
					human += Space;
				human += c;
			}
			return human;
		}
	}
}