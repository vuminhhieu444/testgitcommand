using System;
using Library.Common.Response;
using Library.Common.Message;
using Library.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Library.Cms.DataModel;
using Library.Cms.BusinessLogicLayer;
using Microsoft.AspNetCore.Hosting;
using Library.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Digitizing.Api.Cms.Controllers
{
    [Route("api/website-item-type-ref")]
	 
    public class WebsiteItemTypeRefController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteItemTypeRefBusiness _websiteitemtyperefBUS;
        public WebsiteItemTypeRefController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteItemTypeRefBusiness websiteitemtyperefBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteitemtyperefBUS = websiteitemtyperefBUS;
        } 
		[Route("create-website-item-type-ref")]
    [HttpPost] 
	public async Task<ResponseMessage<WebsiteItemTypeRefModel>> CreateWebsiteItemTypeRef([FromBody] WebsiteItemTypeRefModel model) {
        var response = new ResponseMessage<WebsiteItemTypeRefModel>();
        try {
			
			model.created_by_user_id = CurrentUserId;
			
			
            var resultBUS = await Task.FromResult(_websiteitemtyperefBUS.Create(model));	 
			if (resultBUS)
            {
                response.Data = model; 
				response.MessageCode = MessageCodes.CreateSuccessfully;
				 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if(string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteItemTypeRef.Add(new DropdownOptionModel() { label= model.item_type_name_e, value= model.item_type_rcd.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                    }

                    readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteItemTypeRef.Add(new DropdownOptionModel() { label = model.item_type_name_l, value = model.item_type_rcd.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                    }  }).Start(); 
                    
				
            }
            else
            {
                response.MessageCode = MessageCodes.CreateFail;
            } 
				
        } catch (Exception ex) {
            response.MessageCode = ex.Message;
        }
        return response;
    }
[Route("update-website-item-type-ref")]
[HttpPost]
public async Task<ResponseMessage<WebsiteItemTypeRefModel>> UpdateWebsiteItemTypeRef([FromBody] WebsiteItemTypeRefModel model) {
	var response = new ResponseMessage<WebsiteItemTypeRefModel>();
	try {
		
		model.lu_user_id = CurrentUserId;
		
		var resultBUS = await Task.FromResult(_websiteitemtyperefBUS.Update(model)); 
		if (resultBUS)
		{
			response.Data = model; 
			response.MessageCode = MessageCodes.UpdateSuccessfully;
			 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteItemTypeRef.Count; ++i)
                            if (listWebsiteItemTypeRef[i].value ==  model.item_type_rcd.ToString())
                            { 
                                listWebsiteItemTypeRef[i].label = model.item_type_name_e; 
                                listWebsiteItemTypeRef[i].sort_order = model.sort_order;
                                result = true; break;  
                            }  
                        if(result)
                        { 
                            listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                        } 
                    }

                    readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteItemTypeRef.Count; ++i)
                            if (listWebsiteItemTypeRef[i].value ==  model.item_type_rcd.ToString())
                            { 
                                listWebsiteItemTypeRef[i].label = model.item_type_name_l; 
                                listWebsiteItemTypeRef[i].sort_order = model.sort_order;
                                result = true; break;  
                            }     
                        if (result)
                        { 
                            listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                        }
                    }
                    }).Start();
			
		}
		else
		{
			response.MessageCode = MessageCodes.UpdateFail;
		} 
	} catch (Exception ex) {
		response.MessageCode = ex.Message;
	}
	return response;
}
[Route("search")]
[HttpPost]
public async Task<ResponseListMessage<List<WebsiteItemTypeRefModel>>> Search([FromBody] Dictionary<string, object> formData) { 
            var response = new ResponseListMessage<List<WebsiteItemTypeRefModel>>();
            try {
			
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString()); 
				var item_type_rcd = formData.Keys.Contains("item_type_rcd") ? Convert.ToString(formData["item_type_rcd"]) : "";
var item_type_name = formData.Keys.Contains("item_type_name") ? Convert.ToString(formData["item_type_name"]) : "";
 
				long total = 0;
                var data = await Task.FromResult(_websiteitemtyperefBUS.Search(page, pageSize, CurrentLanguage,out total  , item_type_rcd , item_type_name));
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize; 				
				
    } catch (Exception ex) {
       response.MessageCode = ex.Message;
    }
    return response;
}
[Route("get-by-id/{id}")]

[HttpGet]
public async Task<ResponseMessage<WebsiteItemTypeRefModel>> GetById(string id) {
    var response = new ResponseMessage<WebsiteItemTypeRefModel>();
    try {
           response.Data = await Task.FromResult(_websiteitemtyperefBUS.GetById(id));
        } catch (Exception ex) {
        response.MessageCode = ex.Message;
	}
        return response;
}
[Route("delete-website-item-type-ref")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteItemTypeRef([FromBody] List<string> items) {
            var response = new ResponseListMessage<bool>();
            try {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { item_type_rcd = ds }).ToList());
				var listItem  = await Task.FromResult(_websiteitemtyperefBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem!=null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
				 new Task(() =>{ 
                //Save data dropdowns to file json
                string readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_en.json";
                string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('e')));
                else
                {
                    List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteItemTypeRef.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteItemTypeRef.Remove(result); 
                    }
                    listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                }

                readFileName = "upload/dropdowns/" + $"website_item_type_ref_d_local.json";
                jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websiteitemtyperefBUS.GetListDropdown('l')));
                else
                {
                    List<DropdownOptionModel> listWebsiteItemTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteItemTypeRef.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteItemTypeRef.Remove(result);
                    }
                    listWebsiteItemTypeRef = listWebsiteItemTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteItemTypeRef));
                } }).Start();  
				 
            } catch (Exception ex) {
                response.MessageCode = ex.Message;
            }
            return response;
        }
[Route("get-dropdown")]

[HttpGet]
public async Task<ResponseMessage<IList<DropdownOptionModel>>> GetListDropdown() {
	var response = new ResponseMessage<IList<DropdownOptionModel>>();
	try {
	    
		response.Data = await Task.FromResult(_websiteitemtyperefBUS.GetListDropdown(CurrentLanguage));
		 new Task(() =>{string relatedFileName = "upload/dropdowns/" + $"website_item_type_ref_d_"+ (CurrentLanguage=='e'?"en":"local")+".json";
FileHelper.SaveJsonFile(relatedFileName, MessageConvert.SerializeObject(response.Data)); }).Start();
	} catch (Exception ex) {
		response.MessageCode = ex.Message;
	}
	return response;
}

		
	} 
}