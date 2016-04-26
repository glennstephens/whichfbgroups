using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinFacebookGroups
{
	public class Datum
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("administrator")]
		public bool Administrator { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }
	}

	public class Paging
	{
		[JsonProperty("next")]
		public string Next { get; set; }
	}

	public class FacebookGroupMembersListResponse
	{
		[JsonProperty("data")]
		public IList<Datum> Data { get; set; }

		[JsonProperty("paging")]
		public Paging Paging { get; set; }
	}
}

