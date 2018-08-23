using Acr.UserDialogs;
using DoeCalor.Helpers;
using DoeCalor.Models;
using DoeCalor.Pages;
using DoeCalor.Service;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoeCalor.ViewModel
{
    public class MasterViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand PhotoCommand { get; set; }

        private Usuario _usuario;
        public Usuario Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
                NotifyPropertyChanged("Usuario");
            }
        }

        public MasterViewModel()
        {
            this.LogoutCommand = new Command(Logout);
            this.PhotoCommand = new Command(Photo);
            GetUsuario();
        }

        private void GetUsuario()
        {
            Usuario = new Usuario()
            {
               
                Email = Settings.Email,
                Name = Settings.Name,
                UrlPhoto = string.IsNullOrEmpty(Settings.UrlPhoto) ? "profile_default.png" : Settings.UrlPhoto
            };
        }

        private void Logout()
        {
            Settings.Email = string.Empty;
            Settings.Name = string.Empty;
            App.Current.MainPage = new NavigationPage(new IntroductionPage());
        }

        private async void Photo()
        {
            var op = await UserDialogs.Instance.ActionSheetAsync("Deseja tirar uma foto ou pega-la na galeria?", "Cancelar", "", null, "Tirar foto", "Galeria de fotos");
            if (op == "Tirar foto")
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    AllowCropping = true,
                    CompressionQuality = 90,
                    DefaultCamera = CameraDevice.Rear,
                    SaveToAlbum = false
                });

                if (file == null)
                    return;

                await UsuarioService.SaveOrUpdatePhoto(file.GetStream(), Settings.Email);
                GetUsuario();
            }
            else if (op == "Galeria de fotos")
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 80,
                });

                if (file == null)
                    return;

                await UsuarioService.SaveOrUpdatePhoto(file.GetStream(), Settings.Email);
                GetUsuario
                    ();
            }
        }
    }
}
