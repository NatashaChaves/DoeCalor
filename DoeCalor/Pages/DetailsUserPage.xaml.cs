using DoeCalor.Models;
using DoeCalor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoeCalor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsUserPage : ContentPage
	{
        public DetailsUserPage(FacebookUser facebookUser, GoogleUser googleUser)
        {
            InitializeComponent();
            this.BindingContext = new DetailsUserViewModel(facebookUser, googleUser);
        }
    }
}