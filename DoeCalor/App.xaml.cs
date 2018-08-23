using DoeCalor.Helpers;
using DoeCalor.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace DoeCalor
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            if (string.IsNullOrEmpty(Settings.Email) || string.IsNullOrEmpty(Settings.Name))
                MainPage = new NavigationPage(new IntroductionPage());
            else
                MainPage = new IndexPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
