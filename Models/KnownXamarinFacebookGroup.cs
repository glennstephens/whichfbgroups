using System;

namespace XamarinFacebookGroups
{
	public class XamarinFacebookGroup
	{
		public string Name {
			get;
			set;
		}

		public string Id {
			get;
			set;
		}

		public string Url {
			get;
			set;
		}
	}

	public static class KnownXamarinFacebookGroups
	{
		public static XamarinFacebookGroup[] Groups = new XamarinFacebookGroup[] {
			new XamarinFacebookGroup() { Name = "Xamarin Developers", Id = "406595582819693", Url = "https://www.facebook.com/groups/xamarin.developers/" },
			new XamarinFacebookGroup() { Name = "Elite Xamarin Developers", Id = "1531348050498724", Url = "https://www.facebook.com/groups/1531348050498724/" },
			new XamarinFacebookGroup() { Name = "Xamarin University", Id = "764154226929755", Url = "https://www.facebook.com/groups/xamarinuniversity/" },
			new XamarinFacebookGroup() { Name = "Xamarin Developers", Id = "1464012803837795", Url = "https://www.facebook.com/Xamarinx" },
			new XamarinFacebookGroup() { Name = "Xamarin Doers", Id="1375617496073063", Url = "https://www.facebook.com/groups/1375617496073063/" }
		};
	}
}
