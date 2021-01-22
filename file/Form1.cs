
using System.Windows.Forms;
public class Form1:
Form {
	internal System.Windows.Forms.RichTextBox RichTextBox1;
	internal System.Windows.Forms.Button Button1;
	internal System.Windows.Forms.RichTextBox RichTextBox2;
	internal System.Windows.Forms.Button Button2;
	internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;

	public Form1() : base()   	{   
		
		this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
		this.Button1 = new System.Windows.Forms.Button();
		this.RichTextBox2 = new System.Windows.Forms.RichTextBox();
		this.Button2 = new System.Windows.Forms.Button();
		this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.SuspendLayout();
		this.RichTextBox1.Location = new System.Drawing.Point(24, 64);
		this.RichTextBox1.Name = "RichTextBox1";
		this.RichTextBox1.TabIndex = 0;
		this.RichTextBox1.Text = "Type something here.";
		this.Button1.Location = new System.Drawing.Point(96, 16);
		this.Button1.Name = "Button1";
		this.Button1.Size = new System.Drawing.Size(96, 24);
		this.Button1.TabIndex = 1;
		this.Button1.Text = "Save To Stream";
		this.Button1.Click += new System.EventHandler(Button1_Click);
		this.RichTextBox2.Location = new System.Drawing.Point(152, 64);
		this.RichTextBox2.Name = "RichTextBox2";
		this.RichTextBox2.TabIndex = 3;
		this.RichTextBox2.Text = "It will be added to the stream " + "and appear here.";
		this.Button2.Location = new System.Drawing.Point(104, 200);
		this.Button2.Name = "Button2";
		this.Button2.Size = new System.Drawing.Size(88, 32);
		this.Button2.TabIndex = 4;
		this.Button2.Text = "Save Stream To File";
		this.Button2.Click += new System.EventHandler(Button2_Click);
		this.ClientSize = new System.Drawing.Size(292, 266);
		this.Controls.Add(this.Button2);
		this.Controls.Add(this.RichTextBox2);
		this.Controls.Add(this.Button1);
		this.Controls.Add(this.RichTextBox1);
		this.Name = "Form1";
		this.Text = "Form1";
		this.ResumeLayout(false);

	}

	// Declare a new memory stream.
	System.IO.MemoryStream userInput = new System.IO.MemoryStream();

	// Save the content of RichTextBox1 to the memory stream, 
	// appending a LineFeed character.  
	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		RichTextBox1.SaveFile(userInput, RichTextBoxStreamType.PlainText);
		userInput.WriteByte(13);

		// Display the entire contents of the stream,
		// by setting its position to 0, to RichTextBox2.
        userInput.Position = 0;
        RichTextBox2.LoadFile(userInput, RichTextBoxStreamType.PlainText);
	}

	// Shows the use of a SaveFileDialog to save a MemoryStream to a file.
	private void Button2_Click(System.Object sender, System.EventArgs e) 	{
		// Set the properties on SaveFileDialog1 so the user is 
		// prompted to create the file if it doesn't exist 
		// or overwrite the file if it does exist.
		SaveFileDialog1.CreatePrompt = true;
		SaveFileDialog1.OverwritePrompt = true;

		// Set the file name to myText.txt, set the type filter
		// to text files, and set the initial directory to drive C.
		SaveFileDialog1.FileName = "myText";
		SaveFileDialog1.DefaultExt = "txt";
		SaveFileDialog1.Filter = "Text files (*.txt)|*.txt";
		SaveFileDialog1.InitialDirectory = "c:\\";

		// Call ShowDialog and check for a return value of DialogResult.OK,
		// which indicates that the file was saved. 
		DialogResult result = SaveFileDialog1.ShowDialog();
		System.IO.Stream fileStream;

		if (result == DialogResult.OK)

			// Open the file, copy the contents of memoryStream to fileStream,
			// and close fileStream. Set the memoryStream.Position value to 0 to 
			// copy the entire stream. 
		{
			fileStream = SaveFileDialog1.OpenFile();
			userInput.Position = 0;
			userInput.WriteTo(fileStream);
			fileStream.Close();
		}
	}

	private void InitializeComponent( ) {
			this.SuspendLayout();
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(368, 308);
			this.Name = "Form1";
			this.ResumeLayout(false);

	}
}