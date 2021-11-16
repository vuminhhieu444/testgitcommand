using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsiteGroupTypeRefModel
	{
		public string group_type_rcd { get; set; }
		public string group_type_name { get; set; }
		public string group_type_name_e { get; set; }
		public string group_type_name_l { get; set; }
		public int? sort_order { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}