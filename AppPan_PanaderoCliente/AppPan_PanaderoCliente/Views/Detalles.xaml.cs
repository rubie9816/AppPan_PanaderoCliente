using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace AppPan_PanaderoCliente.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalles : ContentPage
    {
        public Detalles(Pin pin)
        {
            InitializeComponent();
            tienda.Text = pin.Label;
        }

        private async void btnPedido_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Pedido realizado", "El vendedor ha sido notificado.", "OK");
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            List<Models.Usuario> vendedores = new List<Models.Usuario>();
            vendedores.Add(new Models.Usuario
            {
                ubicacion = new Position(latitude: 32.632017, longitude: -115.389143)
            });
            vendedores.Add(new Models.Usuario
            {
                ubicacion = new Position(latitude: 32.632948, longitude: -115.388649)
            });
            vendedores.Add(new Models.Usuario
            {
                ubicacion = new Position(latitude: 32.632844, longitude: -115.387165)
            });

            listView.ItemsSource = vendedores;

        }
    }
}