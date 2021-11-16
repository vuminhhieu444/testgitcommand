using System;
using Library.Common.Message;
using Library.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using Library.Cms.BusinessLogicLayer;
using Library.Common.Caching;
using Library.Common.Response;
using Library.Cms.DataModel;
using UTEHY.Common.Helper;

namespace Digitizing.Api.Cms.Controllers
{
    [Route("api/item-group")]
    public class ItemGroupController : BaseController
    {
        private IWebHostEnvironment _env;
        private IItemGroupBusiness _itemGroupBusiness;
        public ItemGroupController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IItemGroupBusiness itemGroupBusiness) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _itemGroupBusiness = itemGroupBusiness;
        }

        [Route("create-item-group")]
        [HttpPost]
        public  ResponseMessage<ItemGroupModel> CreateItemGroup([FromBody] ItemGroupModel model)
        {
            var response = new ResponseMessage<ItemGroupModel>();
            try
            { 
                model.created_by_user_id = CurrentUserId;
                if (model.image_url != null)
                {
                    var arrData = model.image_url.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $@"upload/item_group/{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}";
                        model.image_url = $"{savePath};{arrData[1]}";
                        FileHelper.SaveFileFromBase64String(savePath, arrData[2]);
                    }
                }
                var resultBUS = _itemGroupBusiness.Create(model);
                if (resultBUS)
                {
                    response.Data = model;
                    response.MessageCode = MessageCodes.CreateSuccessfully;
                }
                else
                {
                    response.MessageCode = MessageCodes.CreateFail;
                }

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("update-item-group")]
        [HttpPost]
        public  ResponseMessage<ItemGroupModel> UpdateItemGroup([FromBody] ItemGroupModel model)
        {
            var response = new ResponseMessage<ItemGroupModel>();
            try
            {

                model.lu_user_id = CurrentUserId;
                if (model.image_url != null)
                {
                    var arrData = model.image_url.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $@"upload/item_group/{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}";
                        model.image_url = $"{savePath};{arrData[1]}";
                        FileHelper.SaveFileFromBase64String(savePath, arrData[2]);
                    }
                }
                var resultBUS = _itemGroupBusiness.Update(model);
                if (resultBUS)
                {
                    response.Data = model;
                    response.MessageCode = MessageCodes.UpdateSuccessfully;
                }
                else
                {
                    response.MessageCode = MessageCodes.UpdateFail;
                }
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("search")]
        [HttpPost]
        public ResponseListMessage<List<ItemGroupModel>> Search([FromBody] Dictionary<string, object> formData)
        {

            var response = new ResponseListMessage<List<ItemGroupModel>>();
            try
            {
                var data = _itemGroupBusiness.GetDataTree(CurrentLanguage); 
                response.Data = data; 
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        } 

        [Route("get-by-id/{id}")]
        [HttpGet]
        public ResponseMessage<ItemGroupModel> GetById(Guid id)
        {
            var response = new ResponseMessage<ItemGroupModel>();
            try
            {
                response.Data = _itemGroupBusiness.GetById(id);
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-item-group")]
        [HttpPost]
        public  ResponseListMessage<bool> DeleteItemGroup([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { item_group_id = ds }).ToList());
                var listItem = _itemGroupBusiness.Delete(json_list_id, CurrentUserId);
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-dropdown")]
        [HttpGet]
        public ResponseMessage<IList<DropdownOptionModel>> GetListDropdown()
        {
            var response = new ResponseMessage<IList<DropdownOptionModel>>();
            try
            {
                response.Data = _itemGroupBusiness.GetListDropdown(CurrentLanguage);
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("export-to-excel")]
        [HttpPost]
        public IActionResult ExportToExcel()
        {
            try
            {
                var data = _itemGroupBusiness.ExportExcel(CurrentLanguage);
                DataTable dataExport = new DataTable();
                dataExport.Columns.Add("item_group_code_parent");
                dataExport.Columns.Add("item_group_code");
                dataExport.Columns.Add("item_group_name");
                dataExport.Columns.Add("sort_order");
                List<ExcelDataExtention> staticDataValue = new List<ExcelDataExtention>();
                staticDataValue.Add(new ExcelDataExtention
                {
                    IsEnd = true,
                    StartColumnName = "D",
                    EndColumnName = "E",
                    StartRowIndex = 1,
                    EndRowIndex = 1,
                    IsMerge = true,
                    AlignmentCenter = true,
                    FontItalic = true,
                    Value = "Hưng Yên, ngày " + DateTime.Now.ToString("dd") + " tháng " + DateTime.Now.ToString("MM") + " năm " + DateTime.Now.ToString("yyyy")
                });
                int start_row = 5;
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        DataRow row = dataExport.NewRow();
                        row["item_group_code_parent"] = item.item_group_code_parent;
                        row["item_group_code"] = item.item_group_code;
                        row["item_group_name"] = item.item_group_name;
                        row["sort_order"] = item.sort_order;
                        dataExport.Rows.Add(row);
                    }
                }
                else
                {
                    dataExport.Rows.Add();
                }
                var webRoot = _env.ContentRootPath;
                var tempPath = Path.Combine(webRoot + @"\ExcelTemplates\", "item_group_temp.xlsx");
                var exportPath = Path.Combine(webRoot + @"\Export\Excel\", "item_group_" + Guid.NewGuid().ToString() + ".xlsx");
                string result = ExportExcel.ExportDataTableToExcel(exportPath, tempPath, dataExport, 1, start_row + 1, staticDataValue, true, "", "", false, "");
                if (string.IsNullOrEmpty(result))
                {
                    var stream = new FileStream(exportPath, FileMode.Open, FileAccess.Read);
                    return File(stream, "application/octet-stream");
                }
                else
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        } 
    }
}