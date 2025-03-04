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
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.DB.Insert(p);
            await DisplayAlert("Sucesso", "Registro Inserido", "OK");



        } catch(Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
}