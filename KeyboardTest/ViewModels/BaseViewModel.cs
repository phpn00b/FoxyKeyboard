using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FoxHornKeyboard.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private readonly Dispatcher _dispatcher;

		public event PropertyChangedEventHandler PropertyChanged;

		public static bool IsInDesigner => (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

		protected BaseViewModel()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
		}

		[Display(AutoGenerateField = false)]
		public bool IsInDesignMode
		{
			get { return IsInDesigner; }
		}

		[Display(AutoGenerateField = false)]
		public Dispatcher UiDispatcher
		{
			get { return _dispatcher; }
		}

		protected void OnPropertyChanged(string propertyName = "")
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		[Display(AutoGenerateField = false)]
		public bool CallingThreadIsUi
		{
			get { return Thread.CurrentThread.ManagedThreadId == UiDispatcher.Thread.ManagedThreadId; }
		}

		protected static BitmapImage ConvertByteArrayToBitmapImage(byte[] bytes)
		{
			var stream = new MemoryStream(bytes);
			stream.Seek(0, SeekOrigin.Begin);
			var image = new BitmapImage();
			image.BeginInit();
			image.StreamSource = stream;
			image.EndInit();
			return image;
		}
	}
}