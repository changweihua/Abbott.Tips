using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Configuration")]
    public class ConfigurationModel : IdentityKeyEntity<int>
    {
        public int ConfigType { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigName { get; set; }
        public string ConfigDescription { get; set; }

        public int EditMode { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual UserModel CreatedUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual UserModel UpdatedUser { get; set; }
    }

    public enum ConfigType
    {
        SITE = 1
    }
}
