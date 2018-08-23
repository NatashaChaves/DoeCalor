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
	public partial class IntroductionPage : CarouselPage
    {
		public IntroductionPage ()
		{
			InitializeComponent ();
		}
	}
}