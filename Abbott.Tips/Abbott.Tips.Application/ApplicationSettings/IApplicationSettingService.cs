using Abbott.Tips.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.ApplicationSettings
{
    public interface IApplicationSettingService : ISingletonDependency
    {
        string GetSettingValue(string settingKey);

        string SetSettingValue(string settingKey, string settingValue);

        int AddSettings(Dictionary<string, string> dictionary);
    }
}
