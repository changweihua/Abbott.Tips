using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.Application.Configurations;
using Abbott.Tips.Application.Configurations.Dtos;
using Abbott.Tips.Core.Mapper;
using Abbott.Tips.Model.Dtos.Query;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    /// <summary>
    /// Tips 配置项 API
    /// </summary>
    public class ConfigurationsController : TipsApiController
    {
        public IConfigurationService iConfigurationService { get; set; }

        public IObjectMapper ObjectMapper { get; set; }

        /// <summary>
        /// 获取全部配置项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var cfgs = await iConfigurationService.GetConfigurationList("");
            return Ok(new { Code = 0, Items = cfgs });
        }

        /// <summary>
        /// 分页获取配置项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pager")]
        public async Task<IActionResult> GetPager()
        {
            var pager = await iConfigurationService.GetPagerAsync();
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { SerializationFilter = new AConfigurationAttribute(), Code = 0, Items = cfgs.Items.Select(_ => _.ToConfigurationListModel()).ToList() });
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { SerializationFilter = new AConfigurationAttribute(), Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(cfgs.Items) });
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(cfgs.Items) });
            return Ok(new JsonListResultModel<ConfigurationListModel> { Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(pager.Items), PageIndex = pager.PageIndex, PageSize = pager.PageSize, TotalCount = pager.TotalCount });
        }

        /// <summary>
        /// 分页获取配置项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(NullableIdDto<int> input)
        {
            var cfgs = await iConfigurationService.GetPagerAsync();
            return Ok(new { Code = 0, Items = cfgs });
        }
    }
}