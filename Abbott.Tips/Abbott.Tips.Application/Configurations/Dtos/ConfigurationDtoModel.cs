using Abbott.Tips.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Application.Configurations.Dtos
{
    [ElForm(FormName = "ConfigurationCreation")]
    public class ConfigurationCreationModel
    {
        [ElFormItemRule(Required = true, Message = "动态Rule校验", Trigger = "blur")]
        [ElFormItemRule(Max = 20, Min = 5, Message = "长度5-20", Trigger = "blur")]
        [Required(ErrorMessage = "必须输入配置项名称")]
        public string ConfigurationName { get; set; }

        [Required]
        public int ConfigurationType { get; set; }

        public string ConfigurationValue { get; set; }

        public string ConfigurationDescription { get; set; }
    }

    [ElForm(FormName = "ConfigurationEdition")]
    public class ConfigurationEditionModel
    {
        [ElFormItem(Hidden = true)]
        public int Id { get; set; }

        [ElFormItem]
        [Required(ErrorMessage = "必须输入配置项名称")]
        public string ConfigurationName { get; set; }

        [ElFormItem]
        public int ConfigurationType { get; set; }

        [ElFormItem]
        public string ConfigurationValue { get; set; }

        [ElFormItem]
        public string ConfigurationDescription { get; set; }

        [ElFormItem(Hidden = true)]
        public byte[] Timestamp { get; set; }
    }

    public class ConfigurationListModel
    {
        public int Id { get; set; }

        [AConfiguration]
        public string ConfigurationName { get; set; }

        public int ConfigurationType { get; set; }

        public string ConfigurationValue { get; set; }

        public string ConfigurationDescription { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
