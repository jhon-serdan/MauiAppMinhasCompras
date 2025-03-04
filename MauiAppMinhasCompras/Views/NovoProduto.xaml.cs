using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void Button_Clicked_Voltar_Home(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

}