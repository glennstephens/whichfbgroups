using System;
using System.Linq;
using UIKit;
using Xamarin.Auth;
using System.Threading.Tasks;

namespace XamarinFacebookGroups
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		AccountStore store;
		Account account;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			store = AccountStore.Create ();

			var accounts = store.FindAccountsForService (FacebookServerInfo.ServiceId);
			if (accounts == null)
				return;
			
			account = accounts.FirstOrDefault ();
			if (account == null)
				return;

			// Load the Account Details
			ShowAccessScreen ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void HandleFacebookLogin (UIButton sender)
		{
			var auth = new OAuth2Authenticator(
				FacebookServerInfo.ClientId,
				FacebookServerInfo.Scope,
				new Uri(FacebookServerInfo.AuthorizationEndPoint),
				new Uri(FacebookServerInfo.RedirectionEndPoint)
			);
			auth.Completed += OnAuthCompleted;
			auth.Error += OnAuthError;

			PresentViewController (auth.GetUI (), true, null);
		}

		void OnAuthCompleted (object sender, AuthenticatorCompletedEventArgs e)
		{
			DismissViewController (true, null);

			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null) 
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error 	-= OnAuthError;
			}

			if (e.IsAuthenticated)
			{
				if (this.account != null)
					store.Delete (this.account, FacebookServerInfo.ServiceId);

				store.Save(account = e.Account, FacebookServerInfo.ServiceId);

				ShowAccessScreen ();
			}
			else
			{
				ShowFailedMessage ("Authentication failed");
			}
		}

		void OnAuthError (object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null) 
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error 	-= OnAuthError;
			}

			ShowFailedMessage ($"Authentication error: {e.Message}");
		}

		void ShowAccessScreen ()
		{
			var vc = Storyboard.InstantiateViewController("UserProfileViewController") as UserProfileViewController;
			vc.StartLoadingFacebookDetails (account);
			var nav = new UINavigationController (vc);

			UIApplication.SharedApplication.Windows[0].RootViewController = nav;
		}

		void ShowFailedMessage(string message = "")
		{

		}
	}
}

