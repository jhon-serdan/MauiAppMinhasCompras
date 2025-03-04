namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
	}

    private async void Button_Novo_Produto(object sender, EventArgs e)
    {
		try
		{
			await Navigation.PushAsync(new Views.NovoProduto());

		} catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}

    }
}