using System.Net.WebSockets;
using System.Windows.Input;

namespace gitinder;
public class MainViewModel : ViewModelBase
{

    public NavigationPage matchesPage {get;set;}
    private Repository _repo = null;
    public Repository repo { get { return _repo; } set { _repo = value; OnPropertyChanged(); } }

    public List<Repository> repos { get; set; }

    public ICommand RejectCommand { get; set; }
    public ICommand AcceptCommand { get; set; }
    public ICommand MatchesCommand { get; set; }

    public MainPage mainPage { get; set; }

    public MainViewModel(MainPage main)
    {

        mainPage = main;
        RejectCommand = new Command(OnReject);
        AcceptCommand = new Command(OnAccept);
        MatchesCommand = new Command(OnMatches);
    }

    private void OnReject()
    {
        
        if(repos.Count == 0) {
            repo = new Repository();
            return; 
        }
        repo = repos[0];
        repos.RemoveAt(0);


    }

    private void OnAccept()
    {
        if (repos.Count == 0)
        {
            return;
        }
        repo = repos[0];
        repos.RemoveAt(0);
    }

    private async void OnMatches()
    {
        await mainPage.Navigation.PushAsync(matchesPage);
    }
}
public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        var vm = new MainViewModel(this);
        BindingContext = vm;
        // Implement reject action here
        vm.matchesPage = new NavigationPage(new MatchesPage());
        Task.Run(() => vm.repos = ApiController.GetPublicRepositories().Result.ToList()).Wait();
        vm.repo = vm.repos[0];

    }
}

