using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsiteImageModel
	{
		public Guid image_id { get; set; }
		public bool is_show { get; set; }
		public string image_name { get; set; }
		public string image_src { get; set; }
		public int? sort_order { get; set; }
		public int type { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}