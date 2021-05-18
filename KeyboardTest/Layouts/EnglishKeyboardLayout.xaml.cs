using FoxHornKeyboard.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FoxHornKeyboard.Layouts
{
	/// <summary>
	/// Interaction logic for EnglishKeyboardLayout.xaml
	/// </summary>
	public partial class EnglishKeyboardLayout : UserControl
	{
		public event EventHandler CloseMe;
		private BaseKeyboardLayoutViewModel KeyboardViewModel => DataContext as BaseKeyboardLayoutViewModel;

		public EnglishKeyboardLayout()
		{
			InitializeComponent();
			var binding = new Binding("Text");
			binding.Mode = BindingMode.TwoWay;
			binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			binding.Source = DataContext;
			SetBinding(EnglishKeyboardLayout.ValueProperty, binding);
		}

		#region Value 

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(string), typeof(EnglishKeyboardLayout), new PropertyMetadata("", ValueProperty_ChangedCallback));

		public string Value
		{
			get => (string)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		/// <summary>
		/// DP changed call back for Value
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its Value property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void ValueProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;
			element.OnValueChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle Value changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region AllowDecimalPoint 

		public static readonly DependencyProperty AllowDecimalPointProperty =
			DependencyProperty.Register("AllowDecimalPoint", typeof(bool), typeof(EnglishKeyboardLayout), new PropertyMetadata(AllowDecimalPointProperty_ChangedCallback));

		public bool AllowDecimalPoint
		{
			get => (bool)GetValue(AllowDecimalPointProperty);
			set => SetValue(AllowDecimalPointProperty, value);
		}

		/// <summary>
		/// DP changed call back for AllowDecimalPoint
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its AllowDecimalPoint property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void AllowDecimalPointProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;
			if (e.NewValue != null)
				element.KeyboardViewModel.AllowDecimalPoint = (bool)e.NewValue;
			element.OnAllowDecimalPointChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle AllowDecimalPoint changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnAllowDecimalPointChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region IsNumeric 

		public static readonly DependencyProperty IsNumericProperty =
			DependencyProperty.Register("IsNumeric", typeof(bool), typeof(EnglishKeyboardLayout), new PropertyMetadata(IsNumericProperty_ChangedCallback));

		public bool IsNumeric
		{
			get => (bool)GetValue(IsNumericProperty);
			set => SetValue(IsNumericProperty, value);
		}

		/// <summary>
		/// DP changed call back for IsNumeric
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its IsNumeric property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void IsNumericProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;
			if (e.NewValue != null)
				if (element.KeyboardViewModel != null)
					element.KeyboardViewModel.OnlyNumericInputAllowed = (bool)e.NewValue;
			element.OnIsNumericChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle IsNumeric changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnIsNumericChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region FinishedCommand 

		public static readonly DependencyProperty FinishedCommandProperty =
			DependencyProperty.Register("FinishedCommand", typeof(ICommand), typeof(EnglishKeyboardLayout), new PropertyMetadata(FinishedCommandProperty_ChangedCallback));

		public ICommand FinishedCommand
		{
			get => (ICommand)GetValue(FinishedCommandProperty);
			set => SetValue(FinishedCommandProperty, value);
		}

		/// <summary>
		/// DP changed call back for FinishedCommand
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its FinishedCommand property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void FinishedCommandProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;

			element.OnFinishedCommandChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle FinishedCommand changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnFinishedCommandChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region NextFieldCommand 

		public static readonly DependencyProperty NextFieldCommandProperty =
			DependencyProperty.Register("NextFieldCommand", typeof(ICommand), typeof(EnglishKeyboardLayout), new PropertyMetadata(NextFieldCommandProperty_ChangedCallback));

		public ICommand NextFieldCommand
		{
			get => (ICommand)GetValue(NextFieldCommandProperty);
			set => SetValue(NextFieldCommandProperty, value);
		}

		/// <summary>
		/// DP changed call back for NextFieldCommand
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its NextFieldCommand property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void NextFieldCommandProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;

			element.OnNextFieldCommandChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle NextFieldCommand changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnNextFieldCommandChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			CloseMe?.Invoke(this, EventArgs.Empty);
		}
	}
}