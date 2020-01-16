How to set up the loading page (for android only)

1. In the main Android project (TravelRecordApp.Android)
	1. There is a file called "LoadingPageServiceDroid.cs" - 
	   This sets up what is effectively a new "contentPage" which it is but it looks like a popup.

2. In the main project (TravelRecordApp)
	1. There is a folder called "Common" where I keep my "loading" pages, they are content pages which have 
	   an opacity set on them and use a "activityindicator" 
	   E.g. 
			<ActivityIndicator IsRunning="True" Color="White" />
	2. You set up your basic page in C# like this:
		    
		[XamlCompilation(XamlCompilationOptions.Compile)]
		public partial class LoadingIndicatorPage : ContentPage
		{
			public LoadingIndicatorPage()
			{
				InitializeComponent();
			}
		}

	3. You can set up as many pages as you want and set them up to look how you want!

3. How to initialise your loading page.
	1. In order to initialise it is really simple:
		
		// Just pass in the page you have just set up in step to to the "InitLoadingPage" method from the android loading page service
		DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());

	2. Once you have done this you can simply call

		// Show the loading page you have just set up
		DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

		// Hide the loading page you have just set up
		DependencyService.Get<ILoadingPageService>().HideLoadingPage();

	3. Enjoy!