using System;
using System.Collections.Generic;
namespace Library.Cms.DataModel 
{
	public partial class WebsiteSlideModel
	{
		public Guid slide_id { get; set; }
		public string slide_title_l { get; set; }
		public string slide_title { get; set; }
		public string slide_title_e { get; set; }
		public string slide_image { get; set; }
		public string slide_url { get; set; }
		public int slide_type { get; set; }
		public int? sort_order { get; set; }
		public bool is_show { get; set; }
		public Guid created_by_user_id { get; set; }
		public DateTime created_date_time { get; set; }
		public DateTime? lu_updated { get; set; }
		public Guid? lu_user_id { get; set; }
		public int active_flag { get; set; }
	}
}