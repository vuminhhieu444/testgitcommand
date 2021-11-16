using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteImageBusiness { 
		bool Create(WebsiteImageModel model);
bool Update(WebsiteImageModel model );
List<WebsiteImageModel> Search(int pageIndex, int pageSize, char lang,out long total  , string image_name);
WebsiteImageModel GetById(Guid? id);
List<WebsiteImageModel> Delete(string json_list_id, Guid updated_by);

    }
}