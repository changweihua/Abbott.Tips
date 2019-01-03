using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Course")]
    public class CourseModel : IdentityKeyEntity<int>
    {
        public string CourseName { get; set; }

        public string CourseRemark { get; set; }

        public int CourseStatus { get; set; }

        public string CourseWareName { get; set; }

        public int CourseWareType { get; set; }

        public string CourseWareFileName { get; set; }

        public string CourseWareOriName { get; set; }

        public int CourseWareMinInterval { get; set; }

        public int CoursePassedScore { get; set; }

        public int PublishStatus { get; set; }

        public int? PublishType { get; set; }

        public bool IsDraft { get; set; }

        #region 导航属性

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
