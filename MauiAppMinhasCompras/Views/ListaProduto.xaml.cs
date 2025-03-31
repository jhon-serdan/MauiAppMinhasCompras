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
		
		lst_produtos.ItemsSource = lista;

    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.DB.GetAll();
            tmp.ForEach(i => lista.Add(i));

            // Carrega as categorias e define o ItemsSource do Picker
            var produtos = await App.DB.GetAll();
            var categorias = produtos.Select(p => p.Categoria).Distinct().ToList();
            categorias.Insert(0, "Todos"); // Adiciona a opção "Todos" no início da lista
            picker_categoria.ItemsSource = categorias;

            picker_categoria.SelectedItem = "Todos";
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

            lst_produtos.IsRefreshing = true;

			lista.Clear();

			List<Produto> tmp = await App.DB.Search(q);

			tmp.ForEach(i => lista.Add(i));

        } catch (Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private void Button_Clicked_Somar(object sender, EventArgs e)
    {
        if (lista.Count == 0)
        {
            DisplayAlert("Atenção", "Não há produtos para somar.", "OK");
            return;
        }

        var somaPorCategoria = lista
            .GroupBy(p => p.Categoria) // Agrupa os produtos pela categoria
            .Select(g => new { Categoria = g.Key, Total = g.Sum(p => p.Total) }) // Soma os valores de cada categoria
            .ToList();

        double totalGeral = lista.Sum(p => p.Total); // Calcula o total geral

        string mensagem = "Totais por Categoria:\n";

        foreach (var item in somaPorCategoria)
        {
            mensagem += $"{item.Categoria}: {item.Total:C}\n";
        }

        mensagem += $"\nTotal Geral: {totalGeral:C}"; // Adiciona o total geral no final

        DisplayAlert("Total por Categoria", mensagem, "OK");
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

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
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
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void picker_categoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Verifica se uma categoria foi selecionada
            string categoriaSelecionada = picker_categoria.SelectedItem as string;
            if (string.IsNullOrEmpty(categoriaSelecionada))
                return;

            lst_produtos.IsRefreshing = true;
            lista.Clear();

            List<Produto> produtosFiltrados;
            if (categoriaSelecionada == "Todos")
            {
                // Se a categoria selecionada for "Todos", carrega todos os produtos
                produtosFiltrados = await App.DB.GetAll();
            }
            else
            {
                // Caso contrário, filtra os produtos pela categoria selecionada
                produtosFiltrados = await App.DB.Categorizar(categoriaSelecionada);
            }
            produtosFiltrados.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

}