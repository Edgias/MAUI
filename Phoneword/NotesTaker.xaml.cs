namespace Phoneword;

public partial class NotesTaker : ContentPage
{
    readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public NotesTaker()
	{
		InitializeComponent();

        if (File.Exists(_fileName))
        {
            editor.Text = File.ReadAllText(_fileName);
        }
        else
        {
            editor.Text = "Mota irikutengwa before unit day 2022";
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs args)
    {
        editor.Text = string.Empty;
    }

    private void OnSaveButtonClicked(object sender, EventArgs args)
    {

    }
}