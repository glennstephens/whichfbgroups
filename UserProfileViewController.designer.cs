// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamarinFacebookGroups
{
	[Register ("UserProfileViewController")]
	partial class UserProfileViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel emailLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel facebookIdLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel facebookMembershipLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView loadingIndicator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel loadingLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint loadingLabelConstraint { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView mainImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel profileNameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem showGroupsButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (emailLabel != null) {
				emailLabel.Dispose ();
				emailLabel = null;
			}
			if (facebookIdLabel != null) {
				facebookIdLabel.Dispose ();
				facebookIdLabel = null;
			}
			if (facebookMembershipLabel != null) {
				facebookMembershipLabel.Dispose ();
				facebookMembershipLabel = null;
			}
			if (loadingIndicator != null) {
				loadingIndicator.Dispose ();
				loadingIndicator = null;
			}
			if (loadingLabel != null) {
				loadingLabel.Dispose ();
				loadingLabel = null;
			}
			if (loadingLabelConstraint != null) {
				loadingLabelConstraint.Dispose ();
				loadingLabelConstraint = null;
			}
			if (mainImage != null) {
				mainImage.Dispose ();
				mainImage = null;
			}
			if (profileNameLabel != null) {
				profileNameLabel.Dispose ();
				profileNameLabel = null;
			}
			if (showGroupsButton != null) {
				showGroupsButton.Dispose ();
				showGroupsButton = null;
			}
		}
	}
}
