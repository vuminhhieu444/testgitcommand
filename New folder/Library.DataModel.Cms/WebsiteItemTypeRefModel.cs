using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsiteItemTypeRefModel
	{
		public string item_type_rcd { get; set; }
		public string item_type_icon { get; set; }
		public string item_type_name_l { get; set; }
		public string item_type_name { get; set; }
		public string item_type_name_e { get; set; }
		public int? item_type_size { get; set; }
		public int? sort_order { get; set; }
		public string item_type_description_l { get; set; }
		public string item_type_description { get; set; }
		public string item_type_description_e { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}