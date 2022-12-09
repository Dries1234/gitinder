using System.Net.WebSockets;
using System.Windows.Input;

namespace gitinder;
public class MainViewModel : ViewModelBase
{

    private Repository _repo = null;
    public int CurrentIndex = 0;
    public Repository repo { get { return _repo; } set { _repo = value; OnPropertyChanged(); } }

    public Repository[] repos { get; set; }

    public ICommand RejectCommand { get; set; }
    public ICommand AcceptCommand { get; set; }
    public ICommand MatchesCommand { get; set; }

    public MainViewModel()
    {
        RejectCommand = new Command(OnReject);
        AcceptCommand = new Command(OnAccept);
        MatchesCommand = new Command(OnMatches);
    }

    private void OnReject()
    {
        repo = repos[CurrentIndex];
        CurrentIndex++;
    }

    private void OnAccept()
    {
        repo = repos[CurrentIndex];
        CurrentIndex++;
    }

    private void OnMatches()
    {
        // Implement matches action here
    }
}
public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        var vm = new MainViewModel();
        BindingContext = vm;
        // Implement reject action here
        Task.Run(() => vm.repos = ApiController.GetPublicRepositories().Result).Wait();
        vm.repo = vm.repos[vm.CurrentIndex];
        vm.CurrentIndex++;

    }
}

