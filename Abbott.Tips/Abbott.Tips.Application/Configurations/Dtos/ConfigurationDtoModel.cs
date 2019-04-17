using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Application.Configurations.Dtos
{
    public class ConfigurationCreationModel
    {
        [Required(ErrorMessage = "必须输入配置项名称")]
        public string ConfigurationName { get; set; }

        public int ConfigurationType { get; set; }

        public string ConfigurationValue { get; set; }

        public string ConfigurationDescription { get; set; }

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
