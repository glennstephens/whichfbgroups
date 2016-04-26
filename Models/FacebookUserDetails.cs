using System;
using Phoneword.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;
using Newtonsoft.Json;

namespace XamarinFacebookGroups
{
	public class FacebookUserDetails : ViewModel
	{
		string _id;
		public string Id {
			get {
				return _id;
			}
			set {
				if (_id != value) {
					_id = value;
					RaisePropertyChanged ();
				}
			}
		}

		string _name;
		public string Name {
			get {
				return _name;
			}
			set {
				if (_name != value) {
					_name = value;
					RaisePropertyChanged ();
				}
			}
		}

		string _email;
		public string Email {
			get {
				return _email;
			}
			set {
				if (_email != value) {
					_email = value;
					RaisePropertyChanged ();
				}
			}
		}

		bool _isLoading;
		public bool IsLoading {
			get { return _isLoading; }
			set {
				if (_isLoading != value) {
					_isLoading = value;
					RaisePropertyChanged ();
				}
			}
		}

		List<string> _GroupIds = new List<string> ();
		public List<string> GroupIds {
			get { return _GroupIds; }
			set {
				if (_GroupIds != value) {
					_GroupIds = value;
					RaisePropertyChanged ();
				}
			}
		}

		async Task<bool> IsAMemberOfGroup(string groupId, Account account)
		{
			var request = new OAuth2Request("GET", 
				new Uri (String.Format ("https://graph.facebook.com/v2.5/{0}/members?limit=100", groupId)), null, account);
			var response = await request.GetResponseAsync();

			var content = response.GetResponseText();
			var groupMembers = JsonConvert.DeserializeObject <FacebookGroupMembersListResponse> (content);

			while (groupMembers.Data.Count > 0) {
				foreach (var member in groupMembers.Data) {
					if (member.Id == this.Id)
						return true;
				}

				request = new OAuth2Request("GET", 
					new Uri (groupMembers.Paging.Next), null, account);
				response = await request.GetResponseAsync();

				content = response.GetResponseText();
				groupMembers = JsonConvert.DeserializeObject <FacebookGroupMembersListResponse> (content);
			}

			return false;
		}


		async Task<List<string>> GetFacebookUserGroups(Account account, IEnumerable<XamarinFacebookGroup> groups)
		{
			var MembershipGroups = new List<string> ();

			// Get the personal information for the 
			try
			{
				foreach (var group in groups)
				{
					var isMember = await IsAMemberOfGroup(group.Id, account);
					if (isMember)
						MembershipGroups.Add (group.Id);
				}
			}
			catch (Exception ex)
			{
			}

			return MembershipGroups;
		}

		/// <summary>
		/// Loads the information for the current user from Facebook and then updates the ViewModel
		/// </summary>
		/// <returns>The details.</returns>
		/// <param name="account">Account.</param>
		/// <param name="groups">Group identifiers.</param>
		public async Task LoadDetails(Account account, IEnumerable<XamarinFacebookGroup> groups)
		{
			this.IsLoading = true;

			var fields = new string[] {
				"name", "email", "picture", "id"
			};
			var request = new OAuth2Request("GET", new Uri ("https://graph.facebook.com/me?fields=name,email"), null, account);
			var response = await request.GetResponseAsync();

			var parser = Newtonsoft.Json.Linq.JObject.Parse (response.GetResponseText());
			this.Name = (string)parser ["name"];
			this.Id = (string)parser ["id"];
			this.Email = (string)parser ["email"];

			// Load the Facebook Groups
			this.GroupIds = await GetFacebookUserGroups (account, KnownXamarinFacebookGroups.Groups);

			this.IsLoading = false;
		}
	}
}

