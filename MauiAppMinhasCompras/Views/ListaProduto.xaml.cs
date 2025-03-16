using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;



public partial class ListaProduto : ContentPage
{

	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();
		
		col_produtos.ItemsSource = lista;

    }

    protected async override void OnAppearing()
    {
		lista.Clear();

        List<Produto> tmp = await App.DB.GetAll();

		tmp.ForEach(i => lista.Add(i));
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

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.DB.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Toal dos Produtos", msg, "ok");
    }
}