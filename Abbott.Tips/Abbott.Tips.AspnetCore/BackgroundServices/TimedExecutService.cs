using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.BackgroundServices
{
    /// <summary>
    /// 简单的定时任务执行
    /// 基于后台服务类 BackgroundService 实现
    /// 类所在程序集为Microsoft.Extensions.Hosting
    /// </summary>
    public class TimedExecutService : BackgroundService
    {
        public ILogger _logger;
        public TimedExecutService(ILogger<TimedExecutService> logger)
        {
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：启动");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(5000, stoppingToken); //启动后5秒执行一次 (用于测试)
                    _logger.LogInformation(DateTime.Now.ToString() + " 执行逻辑");
                }

                _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：停止");
            }
            catch (Exception ex)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：异常" + ex.Message + ex.StackTrace);
                }
                else
                {
                    _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：停止");
                }
            }
        }
    }

}
