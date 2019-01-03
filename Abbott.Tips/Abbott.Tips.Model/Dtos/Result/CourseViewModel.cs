using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class CourseListModel
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CourseStatus { get; set; }

        public string CourseStatusText { get; set; }

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
