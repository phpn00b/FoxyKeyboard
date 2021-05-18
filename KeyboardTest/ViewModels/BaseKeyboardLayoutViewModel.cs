using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FoxHornKeyboard.ViewModels
{
	public abstract class BaseKeyboardLayoutViewModel : BaseViewModel
	{
		private readonly SolidColorBrush _visibleSelectionBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 0, 120, 215));
		private readonly SolidColorBrush _blinkSelectionBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(20, 0, 120, 215));
		protected static readonly string[][][] SpanishKeyMap = new[]
		{
			new string[][]
			{
				new string[] {"\\"},
				new string[] {"1", "!", "|"},
				new string[] {"2", "\"", "@"},
				new string[] {"3", ".", "#"},
				new string[] {"4", "$"},
				new string[] {"5", "%"},
				new string[] {"6", "&"},
				new string[] {"7", "/"},
				new string[] {"8", "("},
				new string[] {"9", ")"},
				new string[] {"0", "="},
				new string[] {"'", "?"},
				new string[] {"¡", "¿"},
				new string[] {"BACKSPACE"}
			},
			new string[][]
			{
				new string[] {"TAB"},
				new string[] {"q", "Q"},
				new string[] {"w", "W"},
				new string[] {"e", "E"},
				new string[] {"r", "R"},
				new string[] {"t", "T"},
				new string[] {"y", "Y"},
				new string[] {"u", "U"},
				new string[] {"i", "I"},
				new string[] {"o", "O"},
				new string[] {"p", "P"},
				new string[] {"`", "^", "["},
				new string[] {"+", "*", "]"},
				new string[] {"ENTER"},
			},
			new string[][]
			{
				new string[] {"CAP-LOCK"},
				new string[] {"a", "A"},
				new string[] {"s", "S"},
				new string[] {"d", "D"},
				new string[] {"f", "F"},
				new string[] {"g", "G"},
				new string[] {"h", "H"},
				new string[] {"j", "J"},
				new string[] {"k", "K"},
				new string[] {"l", "L"},
				new string[] {"ñ", "Ñ"},
				new string[] {"´", "¨", "{"},
				new string[] {"Ç", "}"},
			},
			new string[][]
			{
				new string[] {"SHIFT"},
				new string[] {"<", ">"},
				new string[] {"z", "Z"},
				new string[] {"x", "X"},
				new string[] {"c", "C"},
				new string[] {"v", "V"},
				new string[] {"b", "B"},
				new string[] {"n", "N"},
				new string[] {"m", "M"},
				new string[] {",", ";"},
				new string[] {".", ":"},
				new string[] {"-", "_"},
				new string[] {"SHIFT"}
			},
			new string[][]
			{
				new string[] {"CTRL","CTRL","CTRL"},
				new string[] {"","",""},
				new string[] {"ALT","ALT","ALT"},
				new string[] {"SPACE","SPACE","SPACE"},
				new string[] {"ALT GR","ALT GR","ALT GR"},
				new string[] {"","",""},
				new string[] {"","",""},
				new string[] {"CTRL","CTRL","CTRL"},
			}
		};

		protected static readonly string[][][] EnglishKeyMap = new[]
		{
			new string[][]
			{
				new string[] {"`","~"},
				new string[] {"1", "!"},
				new string[] {"2", "@"},
				new string[] {"3", "#"},
				new string[] {"4", "$"},
				new string[] {"5", "%"},
				new string[] {"6", "^"},
				new string[] {"7", "&"},
				new string[] {"8", "*"},
				new string[] {"9", "("},
				new string[] {"0", ")"},
				new string[] {"-", "_"},
				new string[] {"=", "+"},
				new string[] {"BACKSPACE"}
			},
			new string[][]
			{
				new string[] {"TAB"},
				new string[] {"q", "Q"},
				new string[] {"w", "W"},
				new string[] {"e", "E"},
				new string[] {"r", "R"},
				new string[] {"t", "T"},
				new string[] {"y", "Y"},
				new string[] {"u", "U"},
				new string[] {"i", "I"},
				new string[] {"o", "O"},
				new string[] {"p", "P"},
				new string[] {"[", "["},
				new string[] {"]", "]"},
				new string[] {"\\","|"},
			},
			new string[][]
			{
				new string[] {"CAP-LOCK"},
				new string[] {"a", "A"},
				new string[] {"s", "S"},
				new string[] {"d", "D"},
				new string[] {"f", "F"},
				new string[] {"g", "G"},
				new string[] {"h", "H"},
				new string[] {"j", "J"},
				new string[] {"k", "K"},
				new string[] {"l", "L"},
				new string[] {";", ":"},
				new string[] {"'", "\""},
				new string[] {"ENTER"},
			},
			new string[][]
			{
				new string[] {"SHIFT"},

				new string[] {"z", "Z"},
				new string[] {"x", "X"},
				new string[] {"c", "C"},
				new string[] {"v", "V"},
				new string[] {"b", "B"},
				new string[] {"n", "N"},
				new string[] {"m", "M"},
				new string[] {",", "<"},
				new string[] {".", ">"},
				new string[] {"/", "?"},
				new string[] {"SHIFT"}
			},
			new string[][]
			{
				new string[] {""},
				new string[] {""},
				new string[] {""},
				new string[] {"SPACE"},
				new string[] {""},
				new string[] {""},
				new string[] {""},
				new string[] {""},
			}
		};

		public string[][][] ActiveKeyMap { get; protected set; }
		private readonly FontFamily _fontFamily;
		private readonly Typeface _typeface;
		private readonly double _selectionCharHeight;
		private readonly double _selectionCharWidth;
		private readonly DpiScale _dipInfo;
		private readonly Timer _timer = new Timer(700);
		private bool _selectionIsVisible;


		#region HasResults Property

		private bool _hasResults;

		public bool HasResults
		{
			get { return _hasResults; }
			set
			{
				if (_hasResults != value)
				{
					_hasResults = value;
					OnPropertyChanged("HasResults");
					ShowResultsCommand.UpdateCanExecuteState();
				}
			}
		}

		#endregion

		private void SetupFakeResults()
		{
			Results.Clear();
			for (int i = 0; i < 20; i++)
			{
				Results.Add(new EnumOption
				{
					Name = $"Item #{i}",
					Value = i
				});
			}
		}

		public class EnumOption
		{
			public string Name { get; set; }
			public object Value { get; set; }
		}

		public ObservableCollection<EnumOption> Results { get; } = new ObservableCollection<EnumOption>();

		protected BaseKeyboardLayoutViewModel()
		{
			_fontFamily = new FontFamily("Consolas");
			_timer.Elapsed += (s, e) =>
			{
				_selectionIsVisible = !_selectionIsVisible;

				SelectionBrush = _selectionIsVisible ? _visibleSelectionBrush : _blinkSelectionBrush;
			};

			_typeface = new Typeface(_fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
			var data = DpiUtilities.GetDpiForDesktop();

			if (!IsInDesigner)
			{
				_timer.Start();
				_dipInfo = new DpiScale(data, data);
				var size = this.MeasureString("_");
				_selectionCharWidth = size.Width;
				_selectionCharHeight = size.Height + 4;
				KeyboardVisibility = Visibility.Visible;
				ResultsVisibility = Visibility.Collapsed;
			}
			else
			{
				_dipInfo = new DpiScale(96, 96);
				var size = this.MeasureString("_");
				_selectionCharWidth = size.Width;
				_selectionCharHeight = size.Height + 4;
				SetupFakeResults();
				ResultsVisibility = Visibility.Visible;
				KeyboardVisibility = Visibility.Visible;
			}

			SelectionHeight = _selectionCharHeight;
			SelectionWidth = _selectionCharWidth;
			KeyPressCommand = new GenericCommand(o => CheckKeyAllowed(o as string), PressKey);
			TabCommand = new GenericCommand(o => true, OnTabPress);
			EnterCommand = new GenericCommand(o => true, OnEnterPress);
			BackspaceCommand = new GenericCommand(o => Text?.Length > 0, OnBackspacePress);
			DeleteCommand = new GenericCommand(o => InputIndex < Text?.Length, OnDeletePress);
			UpArrowCommand = new GenericCommand(o => true, OnUpArrowPress);
			DownArrowCommand = new GenericCommand(o => true, OnDownArrowPress);
			RightArrowCommand = new GenericCommand(o => InputIndex < Text?.Length, OnRightArrowPress);
			LeftArrowCommand = new GenericCommand(o => InputIndex > 0, OnLeftArrowPress);
			HomeCommand = new GenericCommand(o => InputIndex > 0, OnHomePress);
			EndCommand = new GenericCommand(o => InputIndex < Text?.Length, OnEndPress);
			PageUpCommand = new GenericCommand(o => true, OnPageUpPress);
			PageDownCommand = new GenericCommand(o => true, OnPageDownPress);
			SpaceCommand = new GenericCommand(o => true, OnSpaceBarPress);
			SelectAllCommand = new GenericCommand(o => Text?.Length > 0, OnSelectAllPress);
			ClearInputCommand = new GenericCommand(o => true, OnClearInputPress);
			PickResultItemCommand = new GenericCommand(o => true, PickResultItem);
			ShowKeyboardCommand = new GenericCommand(o => true, ShowKeyboard);
			ShowResultsCommand = new GenericCommand(o => HasResults, ShowResults);

			ActiveKeyMap = EnglishKeyMap;
			ChangeKeys();
			SetSelectionPosition();

		}

		private void ShowResults(object obj)
		{


		}

		private void ShowKeyboard(object obj)
		{
			throw new NotImplementedException();
		}

		private void PickResultItem(object obj)
		{
			throw new NotImplementedException();
		}

		private void UpdateButtons()
		{
			ClearInputCommand.UpdateCanExecuteState();
			BackspaceCommand.UpdateCanExecuteState();
			LeftArrowCommand.UpdateCanExecuteState();
			RightArrowCommand.UpdateCanExecuteState();
			DeleteCommand.UpdateCanExecuteState();
			HomeCommand.UpdateCanExecuteState();
			EndCommand.UpdateCanExecuteState();
			BackspaceCommand.UpdateCanExecuteState();
			SelectAllCommand.UpdateCanExecuteState();
			ClearInputCommand.UpdateCanExecuteState();
			ShowResultsCommand.UpdateCanExecuteState();
			ShowKeyboardCommand.UpdateCanExecuteState();
		}

		#region InputIndex Property

		private int _inputIndex;

		public int InputIndex
		{
			get { return _inputIndex; }
			set
			{
				if (value < 0)
					value = 0;
				if (_inputIndex != value)
				{
					_inputIndex = value;
					OnPropertyChanged("InputIndex");
					SetSelectionPosition();
				}
			}
		}

		#endregion


		private void SetSelectionPosition()
		{
			SelectionX = _inputIndex * _selectionCharWidth + 8;
		}

		private void OnSelectAllPress(object obj)
		{
			var size = MeasureString(Text);
			InputIndex = 0;
			SelectionWidth = size.Width;
		}

		private void OnClearInputPress(object obj)
		{
			Text = "";
			InputIndex = 0;
		}

		private void OnEnterPress(object obj)
		{
			SetupFakeResults();
			UpdateButtons();
			HasResults = true;
		}

		private void OnBackspacePress(object obj)
		{
			Text = Text.Remove(InputIndex - 1, 1);
			InputIndex--;
		}

		private void OnDeletePress(object obj)
		{
			Text = Text.Remove(InputIndex, 1);
		}

		private void OnUpArrowPress(object obj)
		{

		}

		private void OnDownArrowPress(object obj)
		{

		}

		private void OnRightArrowPress(object obj)
		{
			InputIndex++;
		}

		private void OnLeftArrowPress(object obj)
		{
			InputIndex--;
		}

		private void OnHomePress(object obj)
		{
			InputIndex = 0;

		}

		private void OnEndPress(object obj)
		{
			InputIndex = Text.Length - 1;

		}

		private void OnPageUpPress(object obj)
		{
			throw new NotImplementedException();
		}

		private void OnPageDownPress(object obj)
		{
			throw new NotImplementedException();
		}

		private void OnSpaceBarPress(object obj)
		{
			Text += " ";
			InputIndex++;
		}

		private void OnTabPress(object obj)
		{
			//		throw new NotImplementedException();
		}

		protected void PressKey(object obj)
		{
			var args = obj as string;
			if (string.IsNullOrWhiteSpace(Text))
				Text = "";
			if (args?.Length > 2)
			{
				var pos = args.Split('|').Select(o => Convert.ToInt32(o)).ToArray();
				Text = Text.Insert(InputIndex, GetKeyValueAt(pos[0], pos[1], _shiftIndex));
				//Text += GetKeyValueAt(pos[0], pos[1], _shiftIndex);
				InputIndex++;
			}
			else if (args?.Length == 1)
			{
				Text = Text.Insert(InputIndex, args);
				InputIndex++;
			}

			//this needs to be at the end
			if (IsShiftActive && EnableAutoClearShiftAfterKeyPress)
				IsShiftActive = false;

		}

		#region Text Property

		private string _text = "";

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
				{
					_text = value;
					OnPropertyChanged("Text");
					UpdateButtons();
				}
			}
		}

		#endregion

		public void ActivateShift()
		{

		}

		private int _shiftIndex;
		#region Position_0_0 Properties
		public object Position_0_0Display => GetKeyDisplayAt(0, 0, _shiftIndex);

		public string Position_0_0Value => GetKeyValueAt(0, 0, _shiftIndex);
		#endregion

		#region Position_0_1 Properties
		public object Position_0_1Display => GetKeyDisplayAt(0, 1, _shiftIndex);

		public string Position_0_1Value => GetKeyValueAt(0, 1, _shiftIndex);
		#endregion

		#region Position_0_2 Properties
		public object Position_0_2Display => GetKeyDisplayAt(0, 2, _shiftIndex);

		public string Position_0_2Value => GetKeyValueAt(0, 2, _shiftIndex);
		#endregion

		#region Position_0_3 Properties
		public object Position_0_3Display => GetKeyDisplayAt(0, 3, _shiftIndex);

		public string Position_0_3Value => GetKeyValueAt(0, 3, _shiftIndex);
		#endregion

		#region Position_0_4 Properties
		public object Position_0_4Display => GetKeyDisplayAt(0, 4, _shiftIndex);

		public string Position_0_4Value => GetKeyValueAt(0, 4, _shiftIndex);
		#endregion

		#region Position_0_5 Properties
		public object Position_0_5Display => GetKeyDisplayAt(0, 5, _shiftIndex);

		public string Position_0_5Value => GetKeyValueAt(0, 5, _shiftIndex);
		#endregion

		#region Position_0_6 Properties
		public object Position_0_6Display => GetKeyDisplayAt(0, 6, _shiftIndex);

		public string Position_0_6Value => GetKeyValueAt(0, 6, _shiftIndex);
		#endregion

		#region Position_0_7 Properties
		public object Position_0_7Display => GetKeyDisplayAt(0, 7, _shiftIndex);

		public string Position_0_7Value => GetKeyValueAt(0, 7, _shiftIndex);
		#endregion

		#region Position_0_8 Properties
		public object Position_0_8Display => GetKeyDisplayAt(0, 8, _shiftIndex);

		public string Position_0_8Value => GetKeyValueAt(0, 8, _shiftIndex);
		#endregion

		#region Position_0_9 Properties
		public object Position_0_9Display => GetKeyDisplayAt(0, 9, _shiftIndex);

		public string Position_0_9Value => GetKeyValueAt(0, 9, _shiftIndex);
		#endregion

		#region Position_0_10 Properties
		public object Position_0_10Display => GetKeyDisplayAt(0, 10, _shiftIndex);

		public string Position_0_10Value => GetKeyValueAt(0, 10, _shiftIndex);
		#endregion

		#region Position_0_11 Properties
		public object Position_0_11Display => GetKeyDisplayAt(0, 11, _shiftIndex);

		public string Position_0_11Value => GetKeyValueAt(0, 11, _shiftIndex);
		#endregion

		#region Position_0_12 Properties
		public object Position_0_12Display => GetKeyDisplayAt(0, 12, _shiftIndex);

		public string Position_0_12Value => GetKeyValueAt(0, 12, _shiftIndex);
		#endregion

		#region Position_0_13 Properties
		public object Position_0_13Display => GetKeyDisplayAt(0, 13, _shiftIndex);

		public string Position_0_13Value => GetKeyValueAt(0, 13, _shiftIndex);
		#endregion

		#region Position_0_14 Properties
		public object Position_0_14Display => GetKeyDisplayAt(0, 14, _shiftIndex);

		public string Position_0_14Value => GetKeyValueAt(0, 14, _shiftIndex);
		#endregion

		#region Position_0_15 Properties
		public object Position_0_15Display => GetKeyDisplayAt(0, 15, _shiftIndex);

		public string Position_0_15Value => GetKeyValueAt(0, 15, _shiftIndex);
		#endregion

		#region Position_1_0 Properties
		public object Position_1_0Display => GetKeyDisplayAt(1, 0, _shiftIndex);

		public string Position_1_0Value => GetKeyValueAt(1, 0, _shiftIndex);
		#endregion

		#region Position_1_1 Properties
		public object Position_1_1Display => GetKeyDisplayAt(1, 1, _shiftIndex);

		public string Position_1_1Value => GetKeyValueAt(1, 1, _shiftIndex);
		#endregion

		#region Position_1_2 Properties
		public object Position_1_2Display => GetKeyDisplayAt(1, 2, _shiftIndex);

		public string Position_1_2Value => GetKeyValueAt(1, 2, _shiftIndex);
		#endregion

		#region Position_1_3 Properties
		public object Position_1_3Display => GetKeyDisplayAt(1, 3, _shiftIndex);

		public string Position_1_3Value => GetKeyValueAt(1, 3, _shiftIndex);
		#endregion

		#region Position_1_4 Properties
		public object Position_1_4Display => GetKeyDisplayAt(1, 4, _shiftIndex);

		public string Position_1_4Value => GetKeyValueAt(1, 4, _shiftIndex);
		#endregion

		#region Position_1_5 Properties
		public object Position_1_5Display => GetKeyDisplayAt(1, 5, _shiftIndex);

		public string Position_1_5Value => GetKeyValueAt(1, 5, _shiftIndex);
		#endregion

		#region Position_1_6 Properties
		public object Position_1_6Display => GetKeyDisplayAt(1, 6, _shiftIndex);

		public string Position_1_6Value => GetKeyValueAt(1, 6, _shiftIndex);
		#endregion

		#region Position_1_7 Properties
		public object Position_1_7Display => GetKeyDisplayAt(1, 7, _shiftIndex);

		public string Position_1_7Value => GetKeyValueAt(1, 7, _shiftIndex);
		#endregion

		#region Position_1_8 Properties
		public object Position_1_8Display => GetKeyDisplayAt(1, 8, _shiftIndex);

		public string Position_1_8Value => GetKeyValueAt(1, 8, _shiftIndex);
		#endregion

		#region Position_1_9 Properties
		public object Position_1_9Display => GetKeyDisplayAt(1, 9, _shiftIndex);

		public string Position_1_9Value => GetKeyValueAt(1, 9, _shiftIndex);
		#endregion

		#region Position_1_10 Properties
		public object Position_1_10Display => GetKeyDisplayAt(1, 10, _shiftIndex);

		public string Position_1_10Value => GetKeyValueAt(1, 10, _shiftIndex);
		#endregion

		#region Position_1_11 Properties
		public object Position_1_11Display => GetKeyDisplayAt(1, 11, _shiftIndex);

		public string Position_1_11Value => GetKeyValueAt(1, 11, _shiftIndex);
		#endregion

		#region Position_1_12 Properties
		public object Position_1_12Display => GetKeyDisplayAt(1, 12, _shiftIndex);

		public string Position_1_12Value => GetKeyValueAt(1, 12, _shiftIndex);
		#endregion

		#region Position_1_13 Properties
		public object Position_1_13Display => GetKeyDisplayAt(1, 13, _shiftIndex);

		public string Position_1_13Value => GetKeyValueAt(1, 13, _shiftIndex);
		#endregion

		#region Position_1_14 Properties
		public object Position_1_14Display => GetKeyDisplayAt(1, 14, _shiftIndex);

		public string Position_1_14Value => GetKeyValueAt(1, 14, _shiftIndex);
		#endregion

		#region Position_1_15 Properties
		public object Position_1_15Display => GetKeyDisplayAt(1, 15, _shiftIndex);

		public string Position_1_15Value => GetKeyValueAt(1, 15, _shiftIndex);
		#endregion

		#region Position_2_0 Properties
		public object Position_2_0Display => GetKeyDisplayAt(2, 0, _shiftIndex);

		public string Position_2_0Value => GetKeyValueAt(2, 0, _shiftIndex);
		#endregion

		#region Position_2_1 Properties
		public object Position_2_1Display => GetKeyDisplayAt(2, 1, _shiftIndex);

		public string Position_2_1Value => GetKeyValueAt(2, 1, _shiftIndex);
		#endregion

		#region Position_2_2 Properties
		public object Position_2_2Display => GetKeyDisplayAt(2, 2, _shiftIndex);

		public string Position_2_2Value => GetKeyValueAt(2, 2, _shiftIndex);
		#endregion

		#region Position_2_3 Properties
		public object Position_2_3Display => GetKeyDisplayAt(2, 3, _shiftIndex);

		public string Position_2_3Value => GetKeyValueAt(2, 3, _shiftIndex);
		#endregion

		#region Position_2_4 Properties
		public object Position_2_4Display => GetKeyDisplayAt(2, 4, _shiftIndex);

		public string Position_2_4Value => GetKeyValueAt(2, 4, _shiftIndex);
		#endregion

		#region Position_2_5 Properties
		public object Position_2_5Display => GetKeyDisplayAt(2, 5, _shiftIndex);

		public string Position_2_5Value => GetKeyValueAt(2, 5, _shiftIndex);
		#endregion

		#region Position_2_6 Properties
		public object Position_2_6Display => GetKeyDisplayAt(2, 6, _shiftIndex);

		public string Position_2_6Value => GetKeyValueAt(2, 6, _shiftIndex);
		#endregion

		#region Position_2_7 Properties
		public object Position_2_7Display => GetKeyDisplayAt(2, 7, _shiftIndex);

		public string Position_2_7Value => GetKeyValueAt(2, 7, _shiftIndex);
		#endregion

		#region Position_2_8 Properties
		public object Position_2_8Display => GetKeyDisplayAt(2, 8, _shiftIndex);

		public string Position_2_8Value => GetKeyValueAt(2, 8, _shiftIndex);
		#endregion

		#region Position_2_9 Properties
		public object Position_2_9Display => GetKeyDisplayAt(2, 9, _shiftIndex);

		public string Position_2_9Value => GetKeyValueAt(2, 9, _shiftIndex);
		#endregion

		#region Position_2_10 Properties
		public object Position_2_10Display => GetKeyDisplayAt(2, 10, _shiftIndex);

		public string Position_2_10Value => GetKeyValueAt(2, 10, _shiftIndex);
		#endregion

		#region Position_2_11 Properties
		public object Position_2_11Display => GetKeyDisplayAt(2, 11, _shiftIndex);

		public string Position_2_11Value => GetKeyValueAt(2, 11, _shiftIndex);
		#endregion

		#region Position_2_12 Properties
		public object Position_2_12Display => GetKeyDisplayAt(2, 12, _shiftIndex);

		public string Position_2_12Value => GetKeyValueAt(2, 12, _shiftIndex);
		#endregion

		#region Position_2_13 Properties
		public object Position_2_13Display => GetKeyDisplayAt(2, 13, _shiftIndex);

		public string Position_2_13Value => GetKeyValueAt(2, 13, _shiftIndex);
		#endregion

		#region Position_2_14 Properties
		public object Position_2_14Display => GetKeyDisplayAt(2, 14, _shiftIndex);

		public string Position_2_14Value => GetKeyValueAt(2, 14, _shiftIndex);
		#endregion

		#region Position_2_15 Properties
		public object Position_2_15Display => GetKeyDisplayAt(2, 15, _shiftIndex);

		public string Position_2_15Value => GetKeyValueAt(2, 15, _shiftIndex);
		#endregion

		#region Position_3_0 Properties
		public object Position_3_0Display => GetKeyDisplayAt(3, 0, _shiftIndex);

		public string Position_3_0Value => GetKeyValueAt(3, 0, _shiftIndex);
		#endregion

		#region Position_3_1 Properties
		public object Position_3_1Display => GetKeyDisplayAt(3, 1, _shiftIndex);

		public string Position_3_1Value => GetKeyValueAt(3, 1, _shiftIndex);
		#endregion

		#region Position_3_2 Properties
		public object Position_3_2Display => GetKeyDisplayAt(3, 2, _shiftIndex);

		public string Position_3_2Value => GetKeyValueAt(3, 2, _shiftIndex);
		#endregion

		#region Position_3_3 Properties
		public object Position_3_3Display => GetKeyDisplayAt(3, 3, _shiftIndex);

		public string Position_3_3Value => GetKeyValueAt(3, 3, _shiftIndex);
		#endregion

		#region Position_3_4 Properties
		public object Position_3_4Display => GetKeyDisplayAt(3, 4, _shiftIndex);

		public string Position_3_4Value => GetKeyValueAt(3, 4, _shiftIndex);
		#endregion

		#region Position_3_5 Properties
		public object Position_3_5Display => GetKeyDisplayAt(3, 5, _shiftIndex);

		public string Position_3_5Value => GetKeyValueAt(3, 5, _shiftIndex);
		#endregion

		#region Position_3_6 Properties
		public object Position_3_6Display => GetKeyDisplayAt(3, 6, _shiftIndex);

		public string Position_3_6Value => GetKeyValueAt(3, 6, _shiftIndex);
		#endregion

		#region Position_3_7 Properties
		public object Position_3_7Display => GetKeyDisplayAt(3, 7, _shiftIndex);

		public string Position_3_7Value => GetKeyValueAt(3, 7, _shiftIndex);
		#endregion

		#region Position_3_8 Properties
		public object Position_3_8Display => GetKeyDisplayAt(3, 8, _shiftIndex);

		public string Position_3_8Value => GetKeyValueAt(3, 8, _shiftIndex);
		#endregion

		#region Position_3_9 Properties
		public object Position_3_9Display => GetKeyDisplayAt(3, 9, _shiftIndex);

		public string Position_3_9Value => GetKeyValueAt(3, 9, _shiftIndex);
		#endregion

		#region Position_3_10 Properties
		public object Position_3_10Display => GetKeyDisplayAt(3, 10, _shiftIndex);

		public string Position_3_10Value => GetKeyValueAt(3, 10, _shiftIndex);
		#endregion

		#region Position_3_11 Properties
		public object Position_3_11Display => GetKeyDisplayAt(3, 11, _shiftIndex);

		public string Position_3_11Value => GetKeyValueAt(3, 11, _shiftIndex);
		#endregion

		#region Position_3_12 Properties
		public object Position_3_12Display => GetKeyDisplayAt(3, 12, _shiftIndex);

		public string Position_3_12Value => GetKeyValueAt(3, 12, _shiftIndex);
		#endregion

		#region Position_3_13 Properties
		public object Position_3_13Display => GetKeyDisplayAt(3, 13, _shiftIndex);

		public string Position_3_13Value => GetKeyValueAt(3, 13, _shiftIndex);
		#endregion

		#region Position_3_14 Properties
		public object Position_3_14Display => GetKeyDisplayAt(3, 14, _shiftIndex);

		public string Position_3_14Value => GetKeyValueAt(3, 14, _shiftIndex);
		#endregion

		#region Position_3_15 Properties
		public object Position_3_15Display => GetKeyDisplayAt(3, 15, _shiftIndex);

		protected object GetKeyDisplayAt(int row, int column, int shiftIndex)
		{
			if (row < ActiveKeyMap.Length)
			{
				if (column < ActiveKeyMap[row].Length)
				{
					if (shiftIndex < ActiveKeyMap[row][column].Length)
					{
						return ActiveKeyMap[row][column][shiftIndex];
					}
					return ActiveKeyMap[row][column][0];
				}
			}
			return string.Empty;
		}

		public string Position_3_15Value => GetKeyValueAt(3, 15, _shiftIndex);

		protected string GetKeyValueAt(int row, int column, int shiftIndex)
		{
			if (row < ActiveKeyMap.Length)
				if (column < ActiveKeyMap[row].Length)
					if (shiftIndex < ActiveKeyMap[row][column].Length)
						return ActiveKeyMap[row][column][shiftIndex];
			return null;
		}

		#endregion

		protected void ChangeKeys()
		{
			OnPropertyChanged("Position_0_0Display");
			OnPropertyChanged("Position_0_1Display");
			OnPropertyChanged("Position_0_2Display");
			OnPropertyChanged("Position_0_3Display");
			OnPropertyChanged("Position_0_4Display");
			OnPropertyChanged("Position_0_5Display");
			OnPropertyChanged("Position_0_6Display");
			OnPropertyChanged("Position_0_7Display");
			OnPropertyChanged("Position_0_8Display");
			OnPropertyChanged("Position_0_8Display");
			OnPropertyChanged("Position_0_9Display");
			OnPropertyChanged("Position_0_10Display");
			OnPropertyChanged("Position_0_11Display");
			OnPropertyChanged("Position_0_12Display");
			OnPropertyChanged("Position_0_13Display");
			OnPropertyChanged("Position_0_14Display");
			OnPropertyChanged("Position_0_15Display");
			OnPropertyChanged("Position_1_0Display");
			OnPropertyChanged("Position_1_1Display");
			OnPropertyChanged("Position_1_2Display");
			OnPropertyChanged("Position_1_3Display");
			OnPropertyChanged("Position_1_4Display");
			OnPropertyChanged("Position_1_5Display");
			OnPropertyChanged("Position_1_6Display");
			OnPropertyChanged("Position_1_7Display");
			OnPropertyChanged("Position_1_8Display");
			OnPropertyChanged("Position_1_9Display");
			OnPropertyChanged("Position_1_10Display");
			OnPropertyChanged("Position_1_11Display");
			OnPropertyChanged("Position_1_12Display");
			OnPropertyChanged("Position_1_13Display");
			OnPropertyChanged("Position_1_14Display");
			OnPropertyChanged("Position_1_15Display");
			OnPropertyChanged("Position_2_0Display");
			OnPropertyChanged("Position_2_1Display");
			OnPropertyChanged("Position_2_2Display");
			OnPropertyChanged("Position_2_3Display");
			OnPropertyChanged("Position_2_4Display");
			OnPropertyChanged("Position_2_5Display");
			OnPropertyChanged("Position_2_6Display");
			OnPropertyChanged("Position_2_7Display");
			OnPropertyChanged("Position_2_8Display");
			OnPropertyChanged("Position_2_9Display");
			OnPropertyChanged("Position_2_10Display");
			OnPropertyChanged("Position_2_11Display");
			OnPropertyChanged("Position_2_12Display");
			OnPropertyChanged("Position_2_13Display");
			OnPropertyChanged("Position_2_14Display");
			OnPropertyChanged("Position_2_15Display");
			OnPropertyChanged("Position_3_0Display");
			OnPropertyChanged("Position_3_1Display");
			OnPropertyChanged("Position_3_2Display");
			OnPropertyChanged("Position_3_3Display");
			OnPropertyChanged("Position_3_4Display");
			OnPropertyChanged("Position_3_5Display");
			OnPropertyChanged("Position_3_6Display");
			OnPropertyChanged("Position_3_7Display");
			OnPropertyChanged("Position_3_8Display");
			OnPropertyChanged("Position_3_9Display");
			OnPropertyChanged("Position_3_10Display");
			OnPropertyChanged("Position_3_11Display");
			OnPropertyChanged("Position_3_12Display");
			OnPropertyChanged("Position_3_13Display");
			OnPropertyChanged("Position_3_14Display");
			OnPropertyChanged("Position_3_15Display");
			KeyPressCommand.UpdateCanExecuteState();
		}

		#region IsShiftActive Property

		private bool _isShiftActive;

		public bool IsShiftActive
		{
			get { return _isShiftActive; }
			set
			{
				if (_isShiftActive != value)
				{
					_isShiftActive = value;
					OnPropertyChanged("IsShiftActive");
					UpdateShiftIndex();
					ChangeKeys();
				}
			}
		}

		#endregion

		#region IsCapLockActive Property

		private bool _isCapLockActive;

		public bool IsCapLockActive
		{
			get { return _isCapLockActive; }
			set
			{
				if (_isCapLockActive != value)
				{
					_isCapLockActive = value;
					OnPropertyChanged("IsCapLockActive");
					UpdateShiftIndex();
					ChangeKeys();
				}
			}
		}

		#endregion

		private void UpdateShiftIndex()
		{
			if (IsCapLockActive)
				_shiftIndex = IsShiftActive ? 0 : 1;
			else
				_shiftIndex = IsShiftActive ? 1 : 0;
			KeyPressCommand.UpdateCanExecuteState();
		}

		#region EnableAutoClearShiftAfterKeyPress Property

		private bool _enableAutoClearShiftAfterKeyPress = true;

		public bool EnableAutoClearShiftAfterKeyPress
		{
			get { return _enableAutoClearShiftAfterKeyPress; }
			set
			{
				if (_enableAutoClearShiftAfterKeyPress != value)
				{
					_enableAutoClearShiftAfterKeyPress = value;
					OnPropertyChanged("EnableAutoClearShiftAfterKeyPress");
				}
			}
		}

		#endregion

		public GenericCommand KeyPressCommand { get; }

		public GenericCommand TabCommand { get; }

		public GenericCommand SelectAllCommand { get; }

		public GenericCommand ClearInputCommand { get; }

		public GenericCommand EnterCommand { get; }

		public GenericCommand BackspaceCommand { get; }

		public GenericCommand UpArrowCommand { get; }

		public GenericCommand DownArrowCommand { get; }

		public GenericCommand RightArrowCommand { get; }

		public GenericCommand LeftArrowCommand { get; }

		public GenericCommand HomeCommand { get; }

		public GenericCommand EndCommand { get; }

		public GenericCommand PageUpCommand { get; }

		public GenericCommand PageDownCommand { get; }

		public GenericCommand DeleteCommand { get; }

		public GenericCommand SpaceCommand { get; }

		public GenericCommand ShowResultsCommand { get; }

		public GenericCommand ShowKeyboardCommand { get; }

		public GenericCommand PickResultItemCommand { get; }

		public ICommand FinishedCommand { get; set; }

		public ICommand NextInputFieldCommand { get; set; }

		#region OnlyNumericInputAllowed Property

		private bool _onlyNumericInputAllowed;

		public bool OnlyNumericInputAllowed
		{
			get { return _onlyNumericInputAllowed; }
			set
			{
				if (_onlyNumericInputAllowed != value)
				{
					_onlyNumericInputAllowed = value;
					OnPropertyChanged("OnlyNumericInputAllowed");
					KeyPressCommand.UpdateCanExecuteState();
				}
			}
		}

		#endregion

		#region KeyboardVisibility Property

		private Visibility _keyboardVisibility = Visibility.Visible;

		public Visibility KeyboardVisibility
		{
			get { return _keyboardVisibility; }
			set
			{
				if (_keyboardVisibility != value)
				{
					_keyboardVisibility = value;
					OnPropertyChanged("KeyboardVisibility");
				}
			}
		}

		#endregion

		#region ResultsVisibility Property

		private Visibility _resultsVisibility = Visibility.Collapsed;

		public Visibility ResultsVisibility
		{
			get { return _resultsVisibility; }
			set
			{
				if (_resultsVisibility != value)
				{
					_resultsVisibility = value;
					OnPropertyChanged("ResultsVisibility");
				}
			}
		}

		#endregion

		#region AllowDecimalPoint Property

		private bool _allowDecimalPoint;

		public bool AllowDecimalPoint
		{
			get { return _allowDecimalPoint; }
			set
			{
				if (_allowDecimalPoint != value)
				{
					_allowDecimalPoint = value;
					OnPropertyChanged("AllowDecimalPoint");
					KeyPressCommand.UpdateCanExecuteState();
				}
			}
		}

		#endregion

		protected bool CheckKeyAllowed(string value)
		{
			if (value == null)
				return true;
			if (!OnlyNumericInputAllowed)
				return true;
			if (AllowDecimalPoint && value == ".")
				return true;
			if (value.Length > 2)
			{
				var pos = value.Split('|').Select(o => Convert.ToInt32(o)).ToArray();
				if (pos.Length == 2)
				{
					var key = GetKeyValueAt(pos[0], pos[1], _shiftIndex);
					if (string.IsNullOrWhiteSpace(key))
						return false;
					return Char.IsDigit(key.First());
				}
			}
			return Char.IsDigit(value.First());
		}

		private Size MeasureString(string candidate)
		{
			var formattedText = new FormattedText(candidate, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, _typeface, 24, Brushes.Black, _dipInfo.PixelsPerDip);
			return new Size(formattedText.Width, formattedText.Height);
		}


		#region SelectionX Property

		private double _selectionX = 12;

		public double SelectionX
		{
			get { return _selectionX; }
			set
			{
				if (_selectionX != value)
				{
					_selectionX = value;
					OnPropertyChanged("SelectionX");
				}
			}
		}

		#endregion

		#region SelectionY Property

		private double _selectionY = 2;

		public double SelectionY
		{
			get { return _selectionY; }
			set
			{
				if (_selectionY != value)
				{
					_selectionY = value;
					OnPropertyChanged("SelectionY");
				}
			}
		}

		#endregion

		#region SelectionHeight Property

		private double _selectionHeight = 36;

		public double SelectionHeight
		{
			get { return _selectionHeight; }
			set
			{
				if (_selectionHeight != value)
				{
					_selectionHeight = value;
					OnPropertyChanged("SelectionHeight");
				}
			}
		}

		#endregion

		#region SelectionWidth Property

		private double _selectionWidth = 5;

		public double SelectionWidth
		{
			get { return _selectionWidth; }
			set
			{
				if (_selectionWidth != value)
				{
					_selectionWidth = value;
					OnPropertyChanged("SelectionWidth");
				}
			}
		}

		#endregion

		#region SelectionBrush Property

		private Brush _selectionBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(40, 0, 120, 215));

		public Brush SelectionBrush
		{
			get { return _selectionBrush; }
			set
			{
				if (_selectionBrush != value)
				{
					_selectionBrush = value;
					OnPropertyChanged("SelectionBrush");
				}
			}
		}

		#endregion


	}
}