using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FoxHornKeyboard.ViewModels;

namespace FoxHornKeyboard.Layouts
{
	/// <summary>
	/// Interaction logic for EnglishKeyboardLayout.xaml
	/// </summary>
	public partial class EnglishKeyboardLayout : UserControl
	{
		public event EventHandler CloseMe;

		public event EventHandler<KeyboardSearchEventArgs> SearchTermReady;

		private BaseKeyboardLayoutViewModel KeyboardViewModel => DataContext as BaseKeyboardLayoutViewModel;

		public EnglishKeyboardLayout()
		{
			InitializeComponent();
			var binding = new Binding("Text");
			binding.Mode = BindingMode.TwoWay;
			binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			binding.Source = DataContext;
			SetBinding(EnglishKeyboardLayout.ValueProperty, binding);
			DataContextChanged += (s, e) =>
			{
				if (KeyboardViewModel != null)
				{
					KeyboardViewModel.Finished += (se, ea) =>
					{
						CloseMe?.Invoke(se, ea);
					};
					KeyboardViewModel.SearchTermReady += (se, ea) =>
					{
						SearchTermReady?.Invoke(this, ea);
					};
					KeyboardViewModel.IsAutoCompleteMode = IsAutoCompleteMode;
					LoadAutoCompleteData = KeyboardViewModel.LoadAutoCompleteDataCommand;
				}
			};
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
			if (KeyboardViewModel != null)
				KeyboardViewModel.Text = e.NewValue as string;
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

			element.OnAllowDecimalPointChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle AllowDecimalPoint changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnAllowDecimalPointChanged(DependencyPropertyChangedEventArgs e)
		{
			if (KeyboardViewModel != null)
				if (e.NewValue != null)
					KeyboardViewModel.AllowDecimalPoint = (bool)e.NewValue;
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

			element.OnIsNumericChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle IsNumeric changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnIsNumericChanged(DependencyPropertyChangedEventArgs e)
		{
			if (KeyboardViewModel != null)
				if (e.NewValue != null)
					if (KeyboardViewModel != null)
						KeyboardViewModel.OnlyNumericInputAllowed = (bool)e.NewValue;
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
			if (KeyboardViewModel != null)
				KeyboardViewModel.FinishedCommand = e.NewValue as ICommand;
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

		#region IsAutoCompleteMode 

		public static readonly DependencyProperty IsAutoCompleteModeProperty =
			DependencyProperty.Register("IsAutoCompleteMode", typeof(bool), typeof(EnglishKeyboardLayout), new PropertyMetadata(IsAutoCompleteModeProperty_ChangedCallback));

		public bool IsAutoCompleteMode
		{
			get => (bool)GetValue(IsAutoCompleteModeProperty);
			set => SetValue(IsAutoCompleteModeProperty, value);
		}

		/// <summary>
		/// DP changed call back for IsAutoCompleteMode
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its IsAutoCompleteMode property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void IsAutoCompleteModeProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;

			element.OnIsAutoCompleteModeChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle IsAutoCompleteMode changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnIsAutoCompleteModeChanged(DependencyPropertyChangedEventArgs e)
		{
			if (KeyboardViewModel != null)
				if (e.NewValue != null)
					KeyboardViewModel.IsAutoCompleteMode = (bool)e.NewValue;
		}

		#endregion

		#region LoadAutoCompleteData 

		public static readonly DependencyProperty LoadAutoCompleteDataProperty =
			DependencyProperty.Register("LoadAutoCompleteData", typeof(ICommand), typeof(EnglishKeyboardLayout), new PropertyMetadata(LoadAutoCompleteDataProperty_ChangedCallback));

		public ICommand LoadAutoCompleteData
		{
			get => (ICommand)GetValue(LoadAutoCompleteDataProperty);
			set => SetValue(LoadAutoCompleteDataProperty, value);
		}

		/// <summary>
		/// DP changed call back for LoadAutoCompleteData
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its LoadAutoCompleteData property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void LoadAutoCompleteDataProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;

			element.OnLoadAutoCompleteDataChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle LoadAutoCompleteData changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnLoadAutoCompleteDataChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region AutoClearOnOpen 

		public static readonly DependencyProperty AutoClearOnOpenProperty =
			DependencyProperty.Register("AutoClearOnOpen", typeof(bool), typeof(EnglishKeyboardLayout), new PropertyMetadata(AutoClearOnOpenProperty_ChangedCallback));

		public bool AutoClearOnOpen
		{
			get { return (bool)GetValue(AutoClearOnOpenProperty); }
			set { SetValue(AutoClearOnOpenProperty, value); }
		}

		/// <summary>
		/// DP changed call back for AutoClearOnOpen
		/// </summary>
		/// <param name="obj">instance of EnglishKeyboardLayout that is having its AutoClearOnOpen property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void AutoClearOnOpenProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EnglishKeyboardLayout element = obj as EnglishKeyboardLayout;

			element.OnAutoClearOnOpenChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from EnglishKeyboardLayout to handle AutoClearOnOpen changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnAutoClearOnOpenChanged(DependencyPropertyChangedEventArgs e)
		{
			if (KeyboardViewModel != null)
				if (e.NewValue != null)
					KeyboardViewModel.AutoClearOnOpen = (bool)e.NewValue;
		}

		#endregion

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			CloseMe?.Invoke(this, EventArgs.Empty);
		}

		private void Freezable_OnChanged(object sender, EventArgs e)
		{
			var freezable = sender as Freezable;
			if (freezable?.CanFreeze ?? false)
				freezable.Freeze();
		}
	}
}