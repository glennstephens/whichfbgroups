using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamarinFacebookGroups
{
	partial class FacebookMembershipViewController : UITableViewController
	{
		public FacebookMembershipViewController (IntPtr handle) : base (handle)
		{
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return KnownXamarinFacebookGroups.Groups.Length;
		}

		public FacebookUserDetails UserDetails {
			get;
			set;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("groupMembership");

			var group = KnownXamarinFacebookGroups.Groups [indexPath.Row];

			cell.TextLabel.Text = group.Name;
			cell.DetailTextLabel.Text = group.Url;
			cell.Accessory = UserDetails.GroupIds.Contains (group.Id) ?
				UITableViewCellAccessory.Checkmark :
				UITableViewCellAccessory.None;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var group = KnownXamarinFacebookGroups.Groups [indexPath.Row];
			UIApplication.SharedApplication.OpenUrl (new NSUrl(group.Url));
		}
	}
}
