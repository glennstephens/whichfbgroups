using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace XamarinFacebookGroups
{
	partial class UserProfileViewController : UIViewController
	{
		public UserProfileViewController (IntPtr handle) : base (handle)
		{
		}

		FacebookUserDetails UserDetails;

		public void StartLoadingFacebookDetails (Account account)
		{
			UserDetails = new FacebookUserDetails ();
			UserDetails.LoadDetails (account, KnownXamarinFacebookGroups.Groups);
		}

		public void SetUserDetails(FacebookUserDetails userDetails)
		{
			this.UserDetails = userDetails;
		}

		async Task LoadAllDetails ()
		{
			profileNameLabel.Text = UserDetails.Name;
			emailLabel.Text = UserDetails.Email;
			mainImage.Image = 
				await LoadImage (String.Format ("https://graph.facebook.com/v2.5/{0}/picture?type=large",  UserDetails.Id));

			UpdateVisibility ();
		}

		void UpdateVisibility ()
		{
			mainImage.Hidden = UserDetails.IsLoading;
			profileNameLabel.Hidden = UserDetails.IsLoading;
			emailLabel.Hidden = UserDetails.IsLoading;
			facebookIdLabel.Hidden = UserDetails.IsLoading;
			facebookMembershipLabel.Hidden = UserDetails.IsLoading;

			loadingLabel.Hidden = !UserDetails.IsLoading;
			loadingIndicator.Hidden = !UserDetails.IsLoading;

			showGroupsButton.Enabled = !UserDetails.IsLoading;
		}

		void UpdateGroupDisplay ()
		{
			if (UserDetails.GroupIds == null) {
				facebookMembershipLabel.Text = "No Membership Details found";
				return;
			}

			facebookMembershipLabel.Text = 
				string.Format ("Membership of {0} known Xamarin groups", UserDetails.GroupIds.Count);
		}

		public async Task<UIImage> LoadImage (string imageUrl)
		{
			var client = new HttpClient();
			var contentsTask = client.GetByteArrayAsync (imageUrl);
			var contents = await contentsTask;

			return UIImage.LoadFromData (NSData.FromArray (contents));
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Profile";

			UserDetails.PropertyChanged += async (sender, e) => {

				switch (e.PropertyName)
				{
				case nameof(UserDetails.Name):
					profileNameLabel.Text = UserDetails.Name;
					break;
				case nameof(UserDetails.Email):
					emailLabel.Text = UserDetails.Email;
					break;
				case nameof(UserDetails.Id):
					facebookIdLabel.Text = "Facebook Id: " + UserDetails.Id;
					mainImage.Image = 
						await LoadImage (String.Format ("https://graph.facebook.com/v2.5/{0}/picture?type=large",  UserDetails.Id));
					break;
				case nameof(UserDetails.GroupIds):
					UpdateGroupDisplay();
					break;
				case nameof(UserDetails.IsLoading):
					UpdateVisibility ();
					break;
				case "":
				case null:
					LoadAllDetails();

					break;
				}
			};

			LoadAllDetails();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.DestinationViewController is FacebookMembershipViewController) {
				var vc = segue.DestinationViewController as FacebookMembershipViewController;
				vc.UserDetails = this.UserDetails;
			}
		}
	}
}
