using DoeCalor.Helpers;
using DoeCalor.Interfaces;
using DoeCalor.Models;
using DoeCalor.Pages;
using DoeCalor.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoeCalor.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand EntrarCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginFacebookCommand { get; set; }
        public ICommand LoginGoogleCommand { get; set; }
        private readonly IFacebookManager _facebookManager;
        private readonly IGoogleManager _googleManager;

        private FacebookUser _facebookUser;
        public FacebookUser FacebookUser
        {
            get { return _facebookUser; }
            set
            {
                _facebookUser = value;
                NotifyPropertyChanged("FacebookUser");
            }
        }

        private string _erro;
        public string Erro
        {
            get { return _erro; }
            set
            {
                _erro = value;
                NotifyPropertyChanged("Erro");
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        private string _error;
        public string Error
        {
            get
            {
                return _error;
            }

            set
            {
                _error = value;
                NotifyPropertyChanged("Error");
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public string Name { get; private set; }
        public string UrlPhoto { get; private set; }
        


        public LoginViewModel()
        {
            this.EntrarCommand = new Command(Entrar);
            this.RegisterCommand = new Command(Register);
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
            LoginFacebookCommand = new Command(LoginFacebook);
            LoginGoogleCommand = new Command(LoginGoogle);


        }
        private void LoginFacebook()
        {
            _facebookManager.Login(OnLoginCompleteFacebook);
        }

        private void LoginGoogle()
        {
            _googleManager.Login(OnLoginCompleteGoogle);
        }
        //FACEBOOK
        private async void OnLoginCompleteFacebook(FacebookUser facebookUser, string message)
        {
            if (facebookUser != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new DetailsUserPage(facebookUser, null));
            }
            else
            {
                Erro = message;
            }
        }

        //GOOGLE
        private async void OnLoginCompleteGoogle(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new DetailsUserPage(null, googleUser));
            }
            else
            {
                Erro = message;
            }
        }


        private void Entrar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                Error = "Por favor, digite um e-mail";
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                Error = "Por favor, digite uma senha";
                return;
            }

            var usuario = UsuarioService.Authenticate(Email, Password);
            if (usuario == null)
            {
                Error = "E-mail ou senha inválidos.";
                return;
            }
           
            Email = Settings.Email;
            Name = Settings.Name;
            UrlPhoto = Settings.UrlPhoto;
            App.Current.MainPage = new IndexPage();
        }

        private async void Register()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
