using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace AppPan_PanaderoCliente.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {

        double i;
        Pin userPin;
        //List<Models.Usuario> vendedores;
        List<Pin> vendorsPins;
        public Mapa()
        {
            InitializeComponent();

            //Contador de prueba
            i = 0;
            vendorsPins = new List<Pin>();
            userPin = new Pin();

            userPin.Label = "Your position";
            userPin.Type = PinType.Place;
            userPin.Icon = BitmapDescriptorFactory.FromBundle("PickUpPin.png");
            DisplayCurLoc();
            DisplayCurVendors();
        }


        public async void DisplayCurLoc()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var position = new Position(location.Latitude, location.Longitude);
            userPin.Position = position;
            RefreshPin(userPin);
            // map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userPin.Position.Latitude, userPin.Position.Longitude), Distance.FromMeters(500)));
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(userPin.Position, Distance.FromMeters(500));
            map.MoveToRegion(mapSpan);
        }

        async void Miubi_Clicked(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var position = new Position(location.Latitude, location.Longitude);
            userPin.Position = position;
            RefreshPin(userPin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userPin.Position.Latitude, userPin.Position.Longitude), Distance.FromMeters(500)));
        }

        //async void getMyCurPosition()
        //{
        //    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
        //    var location = await Geolocation.GetLocationAsync(request);
        //    var position = new Position(location.Latitude, location.Longitude);
        //    userPin.Position = position;
        //}

        void GenerateVendorPins(List<Models.Usuario> vendedores)
        {
            this.vendorsPins.Clear();

            foreach (var vendedor in vendedores)
            {
                this.vendorsPins.Add(new Pin
                {
                    ZIndex = vendedor.Id,
                    Type = PinType.Place,
                    Label = vendedor.Nombre_negocio,
                    Icon = BitmapDescriptorFactory.FromBundle("CarPins.png"),
                    Position = vendedor.ubicacion,
                });
            }


        }

        void RefreshPin(Pin pin)
        {
            try
            {
                map.Pins.Remove(pin);
            }
            finally
            {
            }
            map.Pins.Add(pin);
        }

        List<Models.Usuario> getCurVendors()
        {
            //OBTENERLOS DE LA BD
            List<Models.Usuario> vendedores = new List<Models.Usuario>();

            vendedores.Add(new Models.Usuario
            {
                Id = 1,
                Nombre_negocio = "Pan Azteca",
                ubicacion = new Position(latitude: 32.632017 + i, longitude: -115.389143)
            });
            vendedores.Add(new Models.Usuario
            {
                Id = 2,
                Nombre_negocio = "Pan Rosa",
                ubicacion = new Position(latitude: 32.632948 + i, longitude: -115.388649)
            });
            vendedores.Add(new Models.Usuario
            {
                Id = 3,
                Nombre_negocio = "Pan Amigos",
                ubicacion = new Position(latitude: 32.632844 + i, longitude: -115.387165)
            });

            //Variable para pruebas
            i += .002;
            return vendedores;

        }
        void DisplayCurVendors()
        {
            GenerateVendorPins(getCurVendors());
            foreach (var pin in vendorsPins)
            {
                map.Pins.Add(pin);
            }
        }

        private async void BtnRefrescar_Clicked(object sender, EventArgs e)
        {
            map.Pins.Clear();
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var position = new Position(location.Latitude, location.Longitude);
            userPin.Position = position;
            RefreshPin(userPin);
            GenerateVendorPins(getCurVendors());
            foreach (var pin in vendorsPins)
            {
                RefreshPin(pin);
            }

        }

        private async void map_PinClicked(object sender, PinClickedEventArgs e)
        {
            Pin pin = new Pin();
            pin = e.Pin;
            if (pin != null)
            {
                if (pin != userPin)
                {
                    bool answer = await DisplayAlert(pin.Label, "¿Te gustaría consultar los productos " +
                        "disponibles de este negocio?", "Si", "No");
                    if (answer)
                    {
                        await Navigation.PushAsync(new Views.Detalles(pin));
                    }
                }
            }
        }



    }

}
