
namespace KeyboardSample.Full.WinForm
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textKeyboardInput1 = new FoxHornKeyboard.Forms.TextKeyboardInput();
			this.numericKeyboardInput1 = new FoxHornKeyboard.Forms.NumericKeyboardInput();
			this.autoCompleteTextKeyboardInput1 = new FoxHornKeyboard.Forms.AutoCompleteTextKeyboardInput();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(262, 55);
			this.label1.TabIndex = 1;
			this.label1.Text = "NUMERIC:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(115, 134);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(159, 55);
			this.label2.TabIndex = 3;
			this.label2.Text = "TEXT:";
			// 
			// textKeyboardInput1
			// 
			this.textKeyboardInput1.AllowDecimalPoint = true;
			this.textKeyboardInput1.AutoClearOnOpen = false;
			this.textKeyboardInput1.BindingDataType = FoxHornKeyboard.ViewModels.BaseKeyboardInteractionViewModel.DataType.String;
			this.textKeyboardInput1.FormattedKeyboardValue = null;
			this.textKeyboardInput1.IsAutoCompleteMode = false;
			this.textKeyboardInput1.IsNumeric = false;
			this.textKeyboardInput1.IsOpen = false;
			this.textKeyboardInput1.Location = new System.Drawing.Point(280, 119);
			this.textKeyboardInput1.Name = "textKeyboardInput1";
			this.textKeyboardInput1.Size = new System.Drawing.Size(554, 80);
			this.textKeyboardInput1.TabIndex = 2;
			this.textKeyboardInput1.TextInput = null;
			// 
			// numericKeyboardInput1
			// 
			this.numericKeyboardInput1.AllowDecimalPoint = true;
			this.numericKeyboardInput1.AutoClearOnOpen = false;
			this.numericKeyboardInput1.BindingDataType = FoxHornKeyboard.ViewModels.BaseKeyboardInteractionViewModel.DataType.Decimal;
			this.numericKeyboardInput1.FormattedKeyboardValue = null;
			this.numericKeyboardInput1.IsAutoCompleteMode = false;
			this.numericKeyboardInput1.IsNumeric = true;
			this.numericKeyboardInput1.IsOpen = false;
			this.numericKeyboardInput1.Location = new System.Drawing.Point(280, 9);
			this.numericKeyboardInput1.Name = "numericKeyboardInput1";
			this.numericKeyboardInput1.Size = new System.Drawing.Size(554, 80);
			this.numericKeyboardInput1.TabIndex = 0;
			this.numericKeyboardInput1.Value = null;
			// 
			// autoCompleteTextKeyboardInput1
			// 
			this.autoCompleteTextKeyboardInput1.AllowDecimalPoint = false;
			this.autoCompleteTextKeyboardInput1.AutoClearOnOpen = false;
			this.autoCompleteTextKeyboardInput1.BindingDataType = FoxHornKeyboard.ViewModels.BaseKeyboardInteractionViewModel.DataType.String;
			this.autoCompleteTextKeyboardInput1.FormattedKeyboardValue = null;
			this.autoCompleteTextKeyboardInput1.IsAutoCompleteMode = true;
			this.autoCompleteTextKeyboardInput1.IsNumeric = false;
			this.autoCompleteTextKeyboardInput1.IsOpen = false;
			this.autoCompleteTextKeyboardInput1.Location = new System.Drawing.Point(280, 234);
			this.autoCompleteTextKeyboardInput1.Name = "autoCompleteTextKeyboardInput1";
			this.autoCompleteTextKeyboardInput1.Size = new System.Drawing.Size(554, 80);
			this.autoCompleteTextKeyboardInput1.TabIndex = 4;
			this.autoCompleteTextKeyboardInput1.TextInput = null;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(104, 247);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(170, 55);
			this.label3.TabIndex = 5;
			this.label3.Text = "AUTO:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(930, 450);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.autoCompleteTextKeyboardInput1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textKeyboardInput1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numericKeyboardInput1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FoxHornKeyboard.Forms.NumericKeyboardInput numericKeyboardInput1;
		private System.Windows.Forms.Label label1;
		private FoxHornKeyboard.Forms.TextKeyboardInput textKeyboardInput1;
		private System.Windows.Forms.Label label2;
		private FoxHornKeyboard.Forms.AutoCompleteTextKeyboardInput autoCompleteTextKeyboardInput1;
		private System.Windows.Forms.Label label3;
	}
}

