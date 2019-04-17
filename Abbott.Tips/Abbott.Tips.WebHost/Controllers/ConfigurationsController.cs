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
        public async Task<IActionResult> GetPager(ConfigurationPagerParameterQueryModel query)
        {

            //var pager = await iConfigurationService.GetPagerAsync();
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { SerializationFilter = new AConfigurationAttribute(), Code = 0, Items = cfgs.Items.Select(_ => _.ToConfigurationListModel()).ToList() });
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { SerializationFilter = new AConfigurationAttribute(), Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(cfgs.Items) });
            //return Ok(new ListJsonContractResultModel<ConfigurationListModel> { Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(cfgs.Items) });
            //return Ok(new JsonListResultModel<ConfigurationListModel> { Code = 0, Items = ObjectMapper.Map<List<ConfigurationListModel>>(pager.Items), PageIndex = pager.PageIndex, PageSize = pager.PageSize, TotalCount = pager.TotalCount, Result = pager });
            var pager = await iConfigurationService.GetPagerAsync(e => ObjectMapper.Map<ConfigurationListModel>(e), pageIndex: query.PageIndex, pageSize: query.PageSize);
            return Ok(new PagerResultModel<ConfigurationListModel> { Code = 0, Pager = pager });
        }

        /// <summary>
        /// 分页获取配置项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(NullableIdDto<int> input)
        {
            var cfg = await iConfigurationService.Get<ConfigurationListModel>(predicate: c => c.Id == input.Id.GetValueOrDefault());
            return Ok(new ObjectResultModel<ConfigurationListModel> { Code = ResultCode.SUCCESS, Entity = cfg });
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cfg = await iConfigurationService.Get<ConfigurationListModel>(predicate: c => c.Id == id);
            return Ok(new ObjectResultModel<ConfigurationListModel> { Code = ResultCode.SUCCESS, Entity = cfg });
        }

        /// <summary>
        /// 新增配置项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ConfigurationCreationModel model)
        {
            var cfg = await iConfigurationService.Add<ConfigurationListModel>(ObjectMapper.Map<ConfigurationModel>(model));

            return Ok(new ObjectResultModel<ConfigurationListModel> { Code = ResultCode.SUCCESS, Entity = cfg });
        }

        /// <summary>
        /// 更新配置项
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ConfigurationModel model)
        {
            model.UpdatedTime = DateTime.Now;
            var cfg = await iConfigurationService.Update<ConfigurationListModel>(model);

            return Ok(new ObjectResultModel<ConfigurationListModel> { Code = ResultCode.SUCCESS, Entity = cfg });
        }

        /// <summary>
        /// 更新配置项
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]ConfigurationModel model)
        {
            var cfg = await iConfigurationService.Delete<ConfigurationListModel>(model);

            return Ok(new ObjectResultModel<ConfigurationListModel> { Code = ResultCode.SUCCESS, Entity = cfg });
        }
    }
}