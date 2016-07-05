using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Uni2D.UnitTest
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new ContentPage
			{
				Content = new StackLayout
				{
					HorizontalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
					VerticalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
					Children =
					{
						new Label
						{
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						new TestView
						{
							WidthRequest = 200, HeightRequest = 200,
							HorizontalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
							VerticalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true }
						}
					}
				}
			};
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
