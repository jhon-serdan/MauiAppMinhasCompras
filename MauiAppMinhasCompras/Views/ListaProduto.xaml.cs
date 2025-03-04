namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
	}

    private async void Button_Novo_Produto(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new NovoProduto());
    }
}