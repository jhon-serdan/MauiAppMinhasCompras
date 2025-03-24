using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
		try
		{
			lista.Clear();

			List<Produto> tmp = await App.DB.GetAll();

			tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
	    {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

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
		try
		{

			string q = e.NewTextValue;

			lista.Clear();

			List<Produto> tmp = await App.DB.Search(q);

			tmp.ForEach(i => lista.Add(i));

        } catch (Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void Button_Clicked_Somar(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Toal dos Produtos", msg, "ok");
    }

    private async void SwipeItem_Remover(object sender, EventArgs e)
    {
		try
		{
			SwipeItem selecionado = sender as SwipeItem;

			Produto p = selecionado?.BindingContext as Produto;

			bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
			{
                await App.DB.delete(p.Id);
                lista.Remove(p);
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void SwipeItem_Editar(object sender, EventArgs e)
    {
        try
        {
            SwipeItem selecionado = sender as SwipeItem;
            Produto p = selecionado?.BindingContext as Produto;

            if (p != null)
            {
                await Navigation.PushAsync(new Views.EditarProduto
                {
                    BindingContext = p
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

}