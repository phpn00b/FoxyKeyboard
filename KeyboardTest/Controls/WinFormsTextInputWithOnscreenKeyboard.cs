using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FoxHornKeyboard.ViewModels;

namespace FoxHornKeyboard.Controls
{
	/// <summary>
	/// Interaction logic for WinFormsTextInputWithOnscreenKeyboard.xaml
	/// </summary>
	public partial class WinFormsTextInputWithOnscreenKeyboard : UserControl
	{
		public event EventHandler<KeyboardSearchEventArgs> SearchTermReady;

		public WinFormsTextInputWithOnscreenKeyboard()
		{
			InitializeComponent();

			Keyboard.SearchTermReady += (s, e) =>
			{
				SearchTermReady?.Invoke(s, e);
				var vm = DataContext as BaseKeyboardInteractionViewModel;
				vm?.RunSearch(e.Term);
			};
			Keyboard.CloseMe += (s, e) =>
			{
				if (DataContext is BaseKeyboardInteractionViewModel vm)
					vm.IsOpen = false;
			};
			SetBinding(IsNumericProperty, new Binding("IsNumeric"));
			SetBinding(FinishedCommandProperty, new Binding("InternalFinishedCommand"));
			SetBinding(AllowDecimalPointProperty, new Binding("AllowDecimalPoint"));
			SetBinding(ValueProperty, new Binding("KeyboardValue"));
			var b = new Binding("LoadAutoCompleteData");
			b.Mode = BindingMode.TwoWay;
			SetBinding(LoadAutoCompleteDataProperty, b);
			SetBinding(AutoClearOnOpenProperty, new Binding("AutoClearOnOpen"));
			SetBinding(IsAutoCompleteModeProperty, new Binding("IsAutoCompleteMode"));
		}

		#region IsNumeric 

		public static readonly DependencyProperty IsNumericProperty =
			DependencyProperty.Register("IsNumeric", typeof(bool), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(IsNumericProperty_ChangedCallback));

		public bool IsNumeric
		{
			get => (bool)GetValue(IsNumericProperty);
			set => SetValue(IsNumericProperty, value);
		}

		/// <summary>
		/// DP changed call back for IsNumeric
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its IsNumeric property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void IsNumericProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;
			element.OnIsNumericChanged(e);

		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle IsNumeric changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnIsNumericChanged(DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
				Keyboard.IsNumeric = (bool)e.NewValue;
		}

		#endregion

		#region AllowDecimalPoint 

		public static readonly DependencyProperty AllowDecimalPointProperty =
			DependencyProperty.Register("AllowDecimalPoint", typeof(bool), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(AllowDecimalPointProperty_ChangedCallback));

		public bool AllowDecimalPoint
		{
			get => (bool)GetValue(AllowDecimalPointProperty);
			set => SetValue(AllowDecimalPointProperty, value);
		}

		/// <summary>
		/// DP changed call back for AllowDecimalPoint
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its AllowDecimalPoint property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void AllowDecimalPointProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;
			element.OnAllowDecimalPointChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle AllowDecimalPoint changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnAllowDecimalPointChanged(DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
				Keyboard.AllowDecimalPoint = (bool)e.NewValue;
		}

		#endregion

		#region Value 

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(string), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(ValueProperty_ChangedCallback));

		public string Value
		{
			get => (string)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		/// <summary>
		/// DP changed call back for Value
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its Value property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void ValueProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;

			element.OnValueChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle Value changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		#endregion

		#region FinishedCommand 

		public static readonly DependencyProperty FinishedCommandProperty =
			DependencyProperty.Register("FinishedCommand", typeof(ICommand), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(FinishedCommandProperty_ChangedCallback));

		public ICommand FinishedCommand
		{
			get => (ICommand)GetValue(FinishedCommandProperty);
			set => SetValue(FinishedCommandProperty, value);
		}

		/// <summary>
		/// DP changed call back for FinishedCommand
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its FinishedCommand property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void FinishedCommandProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;
			element.OnFinishedCommandChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle FinishedCommand changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnFinishedCommandChanged(DependencyPropertyChangedEventArgs e)
		{
			Keyboard.FinishedCommand = e.NewValue as ICommand;
		}

		#endregion

		#region NextFieldCommand 

		public static readonly DependencyProperty NextFieldCommandProperty =
			DependencyProperty.Register("NextFieldCommand", typeof(ICommand), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(NextFieldCommandProperty_ChangedCallback));

		public ICommand NextFieldCommand
		{
			get => (ICommand)GetValue(NextFieldCommandProperty);
			set => SetValue(NextFieldCommandProperty, value);
		}

		/// <summary>
		/// DP changed call back for NextFieldCommand
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its NextFieldCommand property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void NextFieldCommandProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;
			element.OnNextFieldCommandChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle NextFieldCommand changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnNextFieldCommandChanged(DependencyPropertyChangedEventArgs e)
		{
			Keyboard.NextFieldCommand = e.NewValue as ICommand;
		}

		#endregion

		#region IsAutoCompleteMode 

		public static readonly DependencyProperty IsAutoCompleteModeProperty =
			DependencyProperty.Register("IsAutoCompleteMode", typeof(bool), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(IsAutoCompleteModeProperty_ChangedCallback));

		public bool IsAutoCompleteMode
		{
			get => (bool)GetValue(IsAutoCompleteModeProperty);
			set => SetValue(IsAutoCompleteModeProperty, value);
		}

		/// <summary>
		/// DP changed call back for IsAutoCompleteMode
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its IsAutoCompleteMode property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void IsAutoCompleteModeProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;

			element.OnIsAutoCompleteModeChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle IsAutoCompleteMode changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnIsAutoCompleteModeChanged(DependencyPropertyChangedEventArgs e)
		{

		}

		#endregion

		#region LoadAutoCompleteData 

		public static readonly DependencyProperty LoadAutoCompleteDataProperty =
			DependencyProperty.Register("LoadAutoCompleteData", typeof(ICommand), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(LoadAutoCompleteDataProperty_ChangedCallback));

		public ICommand LoadAutoCompleteData
		{
			get => (ICommand)GetValue(LoadAutoCompleteDataProperty);
			set => SetValue(LoadAutoCompleteDataProperty, value);
		}

		/// <summary>
		/// DP changed call back for LoadAutoCompleteData
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its LoadAutoCompleteData property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void LoadAutoCompleteDataProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;
			element.OnLoadAutoCompleteDataChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle LoadAutoCompleteData changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnLoadAutoCompleteDataChanged(DependencyPropertyChangedEventArgs e)
		{
			Keyboard.LoadAutoCompleteData = e.NewValue as ICommand;
		}

		#endregion

		#region AutoClearOnOpen 

		public static readonly DependencyProperty AutoClearOnOpenProperty =
			DependencyProperty.Register("AutoClearOnOpen", typeof(bool), typeof(WinFormsTextInputWithOnscreenKeyboard), new PropertyMetadata(AutoClearOnOpenProperty_ChangedCallback));

		public bool AutoClearOnOpen
		{
			get { return (bool)GetValue(AutoClearOnOpenProperty); }
			set { SetValue(AutoClearOnOpenProperty, value); }
		}

		/// <summary>
		/// DP changed call back for AutoClearOnOpen
		/// </summary>
		/// <param name="obj">instance of WinFormsTextInputWithOnscreenKeyboard that is having its AutoClearOnOpen property changed</param>
		/// <param name="e">the DP changed arguments</param>
		private static void AutoClearOnOpenProperty_ChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			WinFormsTextInputWithOnscreenKeyboard element = obj as WinFormsTextInputWithOnscreenKeyboard;

			element.OnAutoClearOnOpenChanged(e);
		}

		/// <summary>
		/// Method will enable classes that derive from WinFormsTextInputWithOnscreenKeyboard to handle AutoClearOnOpen changes
		/// </summary>
		/// <param name="e">the DP changed arguments passed in to the DP changed callback</param>
		protected virtual void OnAutoClearOnOpenChanged(DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
				Keyboard.AutoClearOnOpen = (bool)e.NewValue;
		}

		#endregion

		private void button_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as BaseKeyboardInteractionViewModel;
			vm?.KeyboardOpenCommand?.Execute(null);
		}

		private void TextInput_GotFocus(object sender, RoutedEventArgs e)
		{
			/*
			var vm = ((BaseKeyboardLayoutViewModel)Keyboard.DataContext);
			Popup.IsOpen = true;
			Keyboard.Focus();
			e.Handled = true;
			if (Keyboard.Value != null)
				vm.InputIndex = Keyboard.Value.Length - 1;
			if (Popup.IsOpen)
				if (AutoClearOnOpen)
					vm.Text = "";
			*/
		}

		private void TextInput_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var vm = DataContext as BaseKeyboardInteractionViewModel;
			vm?.ProcessMouseOrTouch();
		}

		private void TextInput_TouchDown(object sender, TouchEventArgs e)
		{
			var vm = DataContext as BaseKeyboardInteractionViewModel;
			vm?.ProcessMouseOrTouch();
		}

		private void TextInput_TouchUp(object sender, TouchEventArgs e)
		{
			//e.Handled = true;
		}

		private void TextInput_MouseUp(object sender, MouseButtonEventArgs e)
		{
			//e.Handled = true;
		}

		private void Freezable_OnChanged(object sender, EventArgs e)
		{
			var freezable = sender as Freezable;
			if (freezable?.CanFreeze ?? false)
				freezable.Freeze();
		}
	}
}