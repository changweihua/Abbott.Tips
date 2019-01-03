using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class ShareCategoryListModel
    {
        public int CategoryId { get; set; }

        public int ShareType { get; set; }

        public string ShareTypeName { get; set; }

        public string CategoryName { get; set; }

        public int CategoryStatus { get; set; }

        public string CategoryStatusText { get; set; }

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
