using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcPL.UnitTests.UWP.ViewModels;

namespace jcPL.UnitTests.UWP {
    public sealed partial class MainPage : Page {
        private MainModel viewModel => (MainModel)DataContext;

        public MainPage() {
            this.InitializeComponent();

            DataContext = new MainModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel.LoadData();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.LoadData();
        }
    }
}