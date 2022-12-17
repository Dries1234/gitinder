using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Windows.Input;

namespace gitinder;
public class MockViewModel : ViewModelBase
{
    private Repository _repo = null;
    public Repository repo { get { return _repo; } set { _repo = value; OnPropertyChanged(); } }

    public ObservableCollection<Repository> repos { get; set; }

    public ICommand RejectCommand { get; set; }
    public ICommand AcceptCommand { get; set; }
    public ICommand MatchesCommand { get; set; }

    public MainPage mainPage { get; set; }

    public MockViewModel()
    {

        RejectCommand = new Command(OnReject);
        MatchesCommand = new Command(OnMatches);
    }

    public void OnReject()
    {
        
        if(repos.Count == 0) {
            repo = new Repository();
            return; 
        }
        if(repo == repos[0])
        {
            repos.RemoveAt(0);
        }
        repo = repos[0];
        repos.RemoveAt(0);
    }


    private async void OnMatches()
    {
        await mainPage.Navigation.PushAsync(new MatchesPage());
    }

}

public class MainViewModel : ViewModelBase
{

    private Repository _repo = null;
    public Repository repo { get { return _repo; } set { _repo = value; OnPropertyChanged(); } }

    public ObservableCollection<Repository> repos { get; set; }

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
        if(repo == repos[0])
        {
            repos.RemoveAt(0);
        }
        repo = repos[0];
        repos.RemoveAt(0);
    }

    private async void OnAccept()
    {
        if (repos.Count == 0)
        {
            return;
        }
        try
        {
            await ApiController.AddRepository(repo);
        }
        catch
        {
            await mainPage.DisplayAlert("Oopsie", "Something went wrong trying to add the repository to your matches :)", "Noooooooooooooo....");
        }
        if(repo == repos[0])
        {
            repos.RemoveAt(0);
        }
        repo = repos[0];
        repos.RemoveAt(0);
    }

    private async void OnMatches()
    {
        await mainPage.Navigation.PushAsync(new MatchesPage());
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
        Task.Run(() => {
            var repositories = ApiController.GetPublicRepositories().Result.ToList();
            var matches = ApiController.GetMatches().Result.ToList();
            vm.repos = new ObservableCollection<Repository>(repositories.ExceptBy(matches.Select(x => x.id), x => x.id).ToList());
            }
        ).Wait();
        vm.repo = vm.repos[0];

    }
}

