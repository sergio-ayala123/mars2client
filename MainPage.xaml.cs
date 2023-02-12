using System.ComponentModel;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace MarsClient;

public partial class MainPage : ContentPage
{
    

    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

  
}