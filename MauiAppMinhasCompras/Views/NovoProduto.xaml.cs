using MauiAppMinhasCompras.Models;
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

    private async void Button_adc_item(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = txt_categoria.Text
            };

            await App.DB.Insert(p);

            // Exibir alerta com opções
            bool telaInicial = await DisplayAlert("Sucesso", "Registro Inserido", "Tela Inicial", "Novo Produto");

            if (telaInicial)
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                txt_descricao.Text = "";
                txt_quantidade.Text = "";
                txt_preco.Text = "";
                txt_categoria.Text = "";
                txt_descricao.Focus();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

}