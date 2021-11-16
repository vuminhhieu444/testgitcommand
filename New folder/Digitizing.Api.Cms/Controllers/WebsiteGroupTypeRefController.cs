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
    [Route("api/website-group-type-ref")]
	 
    public class WebsiteGroupTypeRefController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteGroupTypeRefBusiness _websitegrouptyperefBUS;
        public WebsiteGroupTypeRefController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteGroupTypeRefBusiness websitegrouptyperefBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websitegrouptyperefBUS = websitegrouptyperefBUS;
        } 
		[Route("create-website-group-type-ref")]
    [HttpPost] 
	public async Task<ResponseMessage<WebsiteGroupTypeRefModel>> CreateWebsiteGroupTypeRef([FromBody] WebsiteGroupTypeRefModel model) {
        var response = new ResponseMessage<WebsiteGroupTypeRefModel>();
        try {
			
			model.created_by_user_id = CurrentUserId;
			
			
            var resultBUS = await Task.FromResult(_websitegrouptyperefBUS.Create(model));	 
			if (resultBUS)
            {
                response.Data = model; 
				response.MessageCode = MessageCodes.CreateSuccessfully;
				 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if(string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteGroupTypeRef.Add(new DropdownOptionModel() { label= model.group_type_name_e, value= model.group_type_rcd.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
                    }

                    readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteGroupTypeRef.Add(new DropdownOptionModel() { label = model.group_type_name_l, value = model.group_type_rcd.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
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
[Route("update-website-group-type-ref")]
[HttpPost]
public async Task<ResponseMessage<WebsiteGroupTypeRefModel>> UpdateWebsiteGroupTypeRef([FromBody] WebsiteGroupTypeRefModel model) {
	var response = new ResponseMessage<WebsiteGroupTypeRefModel>();
	try {
		
		model.lu_user_id = CurrentUserId;
		
		var resultBUS = await Task.FromResult(_websitegrouptyperefBUS.Update(model)); 
		if (resultBUS)
		{
			response.Data = model; 
			response.MessageCode = MessageCodes.UpdateSuccessfully;
			 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteGroupTypeRef.Count; ++i)
                            if (listWebsiteGroupTypeRef[i].value ==  model.group_type_rcd.ToString())
                            { 
                                listWebsiteGroupTypeRef[i].label = model.group_type_name_e; 
                                listWebsiteGroupTypeRef[i].sort_order = model.sort_order;
                                result = true; break;  
                            }  
                        if(result)
                        { 
                            listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
                        } 
                    }

                    readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteGroupTypeRef.Count; ++i)
                            if (listWebsiteGroupTypeRef[i].value ==  model.group_type_rcd.ToString())
                            { 
                                listWebsiteGroupTypeRef[i].label = model.group_type_name_l; 
                                listWebsiteGroupTypeRef[i].sort_order = model.sort_order;
                                result = true; break;  
                            }     
                        if (result)
                        { 
                            listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
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
public async Task<ResponseListMessage<List<WebsiteGroupTypeRefModel>>> Search([FromBody] Dictionary<string, object> formData) { 
            var response = new ResponseListMessage<List<WebsiteGroupTypeRefModel>>();
            try {
			
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString()); 
				var group_type_name = formData.Keys.Contains("group_type_name") ? Convert.ToString(formData["group_type_name"]) : "";
 
				long total = 0;
                var data = await Task.FromResult(_websitegrouptyperefBUS.Search(page, pageSize, CurrentLanguage,out total  , group_type_name));
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
public async Task<ResponseMessage<WebsiteGroupTypeRefModel>> GetById(string id) {
    var response = new ResponseMessage<WebsiteGroupTypeRefModel>();
    try {
           response.Data = await Task.FromResult(_websitegrouptyperefBUS.GetById(id));
        } catch (Exception ex) {
        response.MessageCode = ex.Message;
	}
        return response;
}
[Route("delete-website-group-type-ref")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteGroupTypeRef([FromBody] List<string> items) {
            var response = new ResponseListMessage<bool>();
            try {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { group_type_rcd = ds }).ToList());
				var listItem  = await Task.FromResult(_websitegrouptyperefBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem!=null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
				 new Task(() =>{ 
                //Save data dropdowns to file json
                string readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_en.json";
                string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('e')));
                else
                {
                    List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteGroupTypeRef.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteGroupTypeRef.Remove(result); 
                    }
                    listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
                }

                readFileName = "upload/dropdowns/" + $"website_group_type_ref_d_local.json";
                jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitegrouptyperefBUS.GetListDropdown('l')));
                else
                {
                    List<DropdownOptionModel> listWebsiteGroupTypeRef = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteGroupTypeRef.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteGroupTypeRef.Remove(result);
                    }
                    listWebsiteGroupTypeRef = listWebsiteGroupTypeRef.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteGroupTypeRef));
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
	    
		response.Data = await Task.FromResult(_websitegrouptyperefBUS.GetListDropdown(CurrentLanguage));
		 new Task(() =>{string relatedFileName = "upload/dropdowns/" + $"website_group_type_ref_d_"+ (CurrentLanguage=='e'?"en":"local")+".json";
FileHelper.SaveJsonFile(relatedFileName, MessageConvert.SerializeObject(response.Data)); }).Start();
	} catch (Exception ex) {
		response.MessageCode = ex.Message;
	}
	return response;
}

		
	} 
}