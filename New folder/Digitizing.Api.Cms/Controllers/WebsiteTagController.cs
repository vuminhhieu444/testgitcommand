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
    [Route("api/website-tag")]
	 
    public class WebsiteTagController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteTagBusiness _websitetagBUS;
        public WebsiteTagController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteTagBusiness websitetagBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websitetagBUS = websitetagBUS;
        } 
		[Route("create-website-tag")]
    [HttpPost] 
	public async Task<ResponseMessage<WebsiteTagModel>> CreateWebsiteTag([FromBody] WebsiteTagModel model) {
        var response = new ResponseMessage<WebsiteTagModel>();
        try {
			
			model.created_by_user_id = CurrentUserId;
			
			
            var resultBUS = await Task.FromResult(_websitetagBUS.Create(model));	 
			if (resultBUS)
            {
                response.Data = model; 
				response.MessageCode = MessageCodes.CreateSuccessfully;
				 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_tag_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if(string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteTag.Add(new DropdownOptionModel() { label= model.tag_name_e, value= model.tag_id.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
                    }

                    readFileName = "upload/dropdowns/" + $"website_tag_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        listWebsiteTag.Add(new DropdownOptionModel() { label = model.tag_name_l, value = model.tag_id.ToString(),level= null,sort_order= model.sort_order});
                        listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
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
[Route("update-website-tag")]
[HttpPost]
public async Task<ResponseMessage<WebsiteTagModel>> UpdateWebsiteTag([FromBody] WebsiteTagModel model) {
	var response = new ResponseMessage<WebsiteTagModel>();
	try {
		
		model.lu_user_id = CurrentUserId;
		
		var resultBUS = await Task.FromResult(_websitetagBUS.Update(model)); 
		if (resultBUS)
		{
			response.Data = model; 
			response.MessageCode = MessageCodes.UpdateSuccessfully;
			 new Task(() =>{ 
                    //Save data dropdowns to file json
                    string readFileName = "upload/dropdowns/" + $"website_tag_d_en.json";
                    string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('e')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteTag.Count; ++i)
                            if (listWebsiteTag[i].value ==  model.tag_id.ToString())
                            { 
                                listWebsiteTag[i].label = model.tag_name_e; 
                                listWebsiteTag[i].sort_order = model.sort_order;
                                result = true; break;  
                            }  
                        if(result)
                        { 
                            listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
                        } 
                    }

                    readFileName = "upload/dropdowns/" + $"website_tag_d_local.json";
                    jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                    if (string.IsNullOrEmpty(jsonDropdowns))
                        FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('l')));
                    else
                    {
                        List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                        var result = false;
                        for (int i = 0; i < listWebsiteTag.Count; ++i)
                            if (listWebsiteTag[i].value ==  model.tag_id.ToString())
                            { 
                                listWebsiteTag[i].label = model.tag_name_l; 
                                listWebsiteTag[i].sort_order = model.sort_order;
                                result = true; break;  
                            }     
                        if (result)
                        { 
                            listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                            FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
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
public async Task<ResponseListMessage<List<WebsiteTagModel>>> Search([FromBody] Dictionary<string, object> formData) { 
            var response = new ResponseListMessage<List<WebsiteTagModel>>();
            try {
			
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString()); 
				var tag_name = formData.Keys.Contains("tag_name") ? Convert.ToString(formData["tag_name"]) : "";
 
				long total = 0;
                var data = await Task.FromResult(_websitetagBUS.Search(page, pageSize, CurrentLanguage,out total  , tag_name));
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
public async Task<ResponseMessage<WebsiteTagModel>> GetById(Guid? id) {
    var response = new ResponseMessage<WebsiteTagModel>();
    try {
           response.Data = await Task.FromResult(_websitetagBUS.GetById(id));
        } catch (Exception ex) {
        response.MessageCode = ex.Message;
	}
        return response;
}
[Route("delete-website-tag")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteTag([FromBody] List<string> items) {
            var response = new ResponseListMessage<bool>();
            try {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { tag_id = ds }).ToList());
				var listItem  = await Task.FromResult(_websitetagBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem!=null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
				 new Task(() =>{ 
                //Save data dropdowns to file json
                string readFileName = "upload/dropdowns/" + $"website_tag_d_en.json";
                string jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('e')));
                else
                {
                    List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteTag.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteTag.Remove(result); 
                    }
                    listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
                }

                readFileName = "upload/dropdowns/" + $"website_tag_d_local.json";
                jsonDropdowns = FileHelper.ReadFileFromAuthAccessFolder(readFileName);
                if (string.IsNullOrEmpty(jsonDropdowns))
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(_websitetagBUS.GetListDropdown('l')));
                else
                {
                    List<DropdownOptionModel> listWebsiteTag = MessageConvert.DeserializeObject<List<DropdownOptionModel>>(jsonDropdowns);
                    foreach (var item in items)
                    {
                        var result = listWebsiteTag.FirstOrDefault(s => s.value == item);
                        if (result != null)
                            listWebsiteTag.Remove(result);
                    }
                    listWebsiteTag = listWebsiteTag.OrderBy(s => s.sort_order==null).ThenBy(s => s.sort_order).ThenBy(s=>s.label).ToList(); 
                    FileHelper.SaveJsonFile(readFileName, MessageConvert.SerializeObject(listWebsiteTag));
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
	    
		response.Data = await Task.FromResult(_websitetagBUS.GetListDropdown(CurrentLanguage));
		 new Task(() =>{string relatedFileName = "upload/dropdowns/" + $"website_tag_d_"+ (CurrentLanguage=='e'?"en":"local")+".json";
FileHelper.SaveJsonFile(relatedFileName, MessageConvert.SerializeObject(response.Data)); }).Start();
	} catch (Exception ex) {
		response.MessageCode = ex.Message;
	}
	return response;
}

		
	} 
}