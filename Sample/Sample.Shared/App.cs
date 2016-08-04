using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Xamarin.Forms;

namespace Cross2D.Sample
{
	public class App : Application
	{
		private Xamarin.Forms.View currentTestCase = null;

		public App()
		{
			Picker selectTestCase = new Picker()
			{
				HorizontalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
			};

			Grid testCase = new Grid()
			{
				HorizontalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
				VerticalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true }
			};

			MainPage = new ContentPage
			{
				Padding = Device.OnPlatform(new Thickness(0, 20, 0, 0), new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 0)),
				Content = new StackLayout
				{
					HorizontalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
					VerticalOptions = new LayoutOptions { Alignment = LayoutAlignment.Fill, Expands = true },
					Children =
					{
						selectTestCase,
						testCase
					}
				}
			};

			List<Type> tests = typeof(App).GetTypeInfo().Assembly.GetTypes().Where(t => t.GetTypeInfo().IsDefined(typeof(SampleAttribute), false)).ToList();
			tests.Sort((t1, t2) =>
			{
				SampleAttribute[] a1s = t1.GetTypeInfo().GetCustomAttributes(typeof(SampleAttribute), false) as SampleAttribute[];
				SampleAttribute[] a2s = t2.GetTypeInfo().GetCustomAttributes(typeof(SampleAttribute), false) as SampleAttribute[];
				if (a1s.Length > 0 && a2s.Length > 0)
				{
					SampleAttribute a1 = a1s[0] as SampleAttribute;
					SampleAttribute a2 = a2s[0] as SampleAttribute;
					return String.Compare(a1.Name != null ? a1.Name : t1.Name, a2.Name != null ? a2.Name : t2.Name, StringComparison.OrdinalIgnoreCase);
				}

				return 0;
			});

			Dictionary<string, Xamarin.Forms.View> testCaseMap = new Dictionary<string, Xamarin.Forms.View>();

			foreach (Type type in tests)
			{
				SampleAttribute[] attribs = type.GetTypeInfo().GetCustomAttributes(typeof(SampleAttribute), false) as SampleAttribute[];
				if (attribs.Length > 0)
				{
					SampleAttribute attrib = attribs[0] as SampleAttribute;
					if (attrib != null)
					{
						Xamarin.Forms.View test = Activator.CreateInstance(type) as Xamarin.Forms.View;
						if (test != null)
						{
							SetVisibility(test, false);

							string name = attrib.Name != null ? attrib.Name : type.Name;

							testCaseMap[name] = test;

							selectTestCase.Items.Add(name);

							testCase.Children.Add(test);
						}
					}
				}
			}

			selectTestCase.SelectedIndexChanged += (sender, args) =>
			{
				if (selectTestCase.SelectedIndex >= 0)
				{
					if (currentTestCase != null)
						SetVisibility(currentTestCase, false);

					currentTestCase = testCaseMap[selectTestCase.Items[selectTestCase.SelectedIndex]];
					SetVisibility(currentTestCase, true);
				}
			};

			selectTestCase.SelectedIndex = 2;
		}

		private void SetVisibility(Xamarin.Forms.View view, bool isVisible)
		{
#if __IOS__
			view.Opacity = isVisible ? 1d : 0d;
#else
			view.IsVisible = isVisible;
#endif
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
