using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsitePartnerModel
	{
		public Guid partner_id { get; set; }
		public string partner_logo { get; set; }
		public string partner_link { get; set; }
		public string partner_name { get; set; }
		public int? sort_order { get; set; }
		public bool is_show { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}