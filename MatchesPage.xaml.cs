using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace gitinder;

public class MatchesViewModel : ViewModelBase
{

    public MatchesPage matchesPage { get; set; }

    private ObservableCollection<Repository> _repos = null;
    public ObservableCollection<Repository> repos { get { return _repos; } set { _repos = value; OnPropertyChanged(); } }

    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public ICommand DeleteCommand { get; set; }
    public MatchesViewModel(MatchesPage matches)
    {

        matchesPage = matches;
        DeleteCommand = new Command<Repository>(OnDelete);
    }

    private async void OnDelete(Repository repo)
    {
        try
        {
            await ApiController.RemoveRepository(repo);
            repos.Remove(repos.ToList().Find(r => r.id == repo.id));
        }
        catch{
            await matchesPage.DisplayAlert("Gawd Damn", "Oh no, we couldn't delete that match!", ":(?");
        }
        
    }


}
public partial class MatchesPage : ContentPage
{
	public MatchesPage()
	{
		InitializeComponent();
        var vm = new MatchesViewModel(this);
        BindingContext = vm;
        Appearing += async (object sender, EventArgs args) => vm.repos = new ObservableCollection<Repository>(await ApiController.GetMatches());
	}
}