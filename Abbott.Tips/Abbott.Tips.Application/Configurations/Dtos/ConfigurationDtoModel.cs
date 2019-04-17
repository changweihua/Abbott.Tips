﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Application.Configurations.Dtos
{
    public class ConfigurationListModel
    {
        public int Id { get; set; }

        [AConfiguration]
        public string ConfigurationName { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
