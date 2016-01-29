using System;
using System.Windows.Input;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;
using RedContactos.Util;
using RedContactos.ViewModel.Contactos;
using Xamarin.Forms;

namespace RedContactos.ViewModel
{
    public class AltaViewModel : GeneralViewModel
    {
        private UsuarioModel _usuario;
        public UsuarioModel Usuario
        {
            get { return _usuario; }

            set { SetProperty(ref _usuario, value); }
        }

        public ICommand cmdAlta { get; set; }

        public AltaViewModel(INavigator navigator, IServicioMovil servicio,IPage page) : base(navigator, servicio,page)
        {
            _usuario = new UsuarioModel();
            cmdAlta = new Command(RunAlta);
        }

        private async void RunAlta()
        {
            try
            {
                IsBusy = true;
                var noesta = await _servicio.UsuarioNuevo(Usuario.login);
                if (noesta)
                {
                    var r = await _servicio.AddUsuario(Usuario);
                    if (r != null)
                    {
                        Cadenas.Session["usuario"] = r;
                        await _navigator.PushAsync<PrincipalViewModel>(viewModel =>
                        {
                            viewModel.Titulo = "Tus Contactos";
                        });
                    }
                    else
                    {
                        await _page.MostrarAlerta("Error", "Error al crear el usuario", "Aceptar");
                    }
                }
                else
                {
                    await _page.MostrarAlerta("Error", "Error usuario ya registrado", "Aceptar");
                }
            }
            catch (Exception e)
            {
                await _page.MostrarAlerta("Error", "El usuario no existe", "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}