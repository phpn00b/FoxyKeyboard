using FoxHornKeyboard.Model;
using System.Linq;
using System.Threading.Tasks;

namespace FoxHornKeyboard.ViewModels
{
	public abstract class BaseTypedKeyboardInteractionViewModel<TServerDataType> : BaseKeyboardInteractionViewModel
	{
		/// <summary>
		/// Process the search term being ready
		/// </summary>
		/// <param name="searchTerm"></param>
		protected override void OnRunSearch(string searchTerm)
		{
			new Task(() =>
			{
				var result = LoadServerData(searchTerm);
				UiDispatcher.Invoke(() => ProcessServerResults(result));
			}).Start();
		}

		private void ProcessServerResults(TServerDataType[] results)
		{
			KeyboardViewModel?.LoadAutoCompleteDataCommand?.Execute(results.Select(Convert).ToArray());
		}

		/// <summary>
		/// Convert server data type to autocomplete type
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected abstract EnumOption Convert(TServerDataType arg);

		/// <summary>
		/// Load data from the server for the auto complete logic 
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <returns></returns>
		protected abstract TServerDataType[] LoadServerData(string searchTerm);
	}
}