using System;
using System.Collections.Generic;
using Library.Cms.DataModel; 

namespace Library.Cms.BusinessLogicLayer {
    public partial interface IWebsiteProductBusiness { 
		bool Create(WebsiteProductModel model);
bool Update(WebsiteProductModel model );
List<WebsiteProductModel> Search(int pageIndex, int pageSize, char lang,out long total  , string product_name);
WebsiteProductModel GetById(Guid? id);
List<WebsiteProductModel> Delete(string json_list_id, Guid updated_by);

    }
}