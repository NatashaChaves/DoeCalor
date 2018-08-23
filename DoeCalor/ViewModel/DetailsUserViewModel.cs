using DoeCalor.Interfaces;
using DoeCalor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoeCalor.ViewModel
{
    public class DetailsUserViewModel : BaseViewModel
    {
        private readonly IFacebookManager _facebookManager;
        private readonly IGoogleManager _googleManager;
        public ICommand LogoutCommand { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private Uri _photo;
        public Uri Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                NotifyPropertyChanged("Photo");
            }
        }

        public bool IsFacebook { get; set; }

        public DetailsUserViewModel(FacebookUser facebookUser, GoogleUser googleUser)
        {
            this.LogoutCommand = new Command(Logout);
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();

            if (facebookUser != null)
            {
                Name = $"{facebookUser.FirstName} {facebookUser.LastName}";
                Photo = new Uri(facebookUser.Picture);
                IsFacebook = true;
            }
            else
            {
                Name = googleUser.Name;
                Photo = googleUser.Picture;
                IsFacebook = false;
            }
        }

        private async void Logout()
        {
            if (IsFacebook)
                _facebookManager.Logout();
            else
                _googleManager.Logout();

            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
