using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abbott.Tips.Model.Result
{
    public class ConfigurationListModel
    {
        [Column("ConfigName")]
        public string ConfigurationName { get; set; }

        [NotMapped]
        public string CreatedUser { get; set; }

        [NotMapped]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string UpdatedUser { get; set; }

        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}
