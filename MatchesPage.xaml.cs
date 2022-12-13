using System.Collections.ObjectModel;

namespace gitinder;

public class MatchesViewModel : ViewModelBase
{

    public MatchesPage matchesPage { get; set; }

    private ObservableCollection<Repository> _repos = null;
    public ObservableCollection<Repository> repos { get { return _repos; } set { _repos = value; OnPropertyChanged(); } }

    public MatchesViewModel(MatchesPage matches)
    {

        matchesPage = matches;
    }

}
public partial class MatchesPage : ContentPage
{
	public MatchesPage()
	{
		InitializeComponent();
        var vm = new MatchesViewModel(this);
        BindingContext = vm;
        Task.Run(() => vm.repos = new ObservableCollection<Repository>(ApiController.GetPublicRepositories().Result)).Wait();
	}
}