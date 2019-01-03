using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    public class CourseCreateModel
    {
        public string CourseName { get; set; }

        public string CourseRemark { get; set; }

        public string CourseWareName { get; set; }

        public string CourseWareOriName { get; set; }

        public int CourseWareMinInterval { get; set; }

        public int CoursePassedScore { get; set; }

        public bool IsDraft { get; set; }
    }

    /// <summary>
    /// 课程列表查询模型
    /// </summary>
    public class CourseListQueryModel : LayTablePagerParameterQueryModel
    {
        public string CourseName { get; set; }
    }

    /// <summary>
    /// 随堂练习试题列表查询模型
    /// </summary>
    public class CourseQuestionListQueryModel : LayTablePagerParameterQueryModel
    {
        public string QuestionContent { get; set; }

        public int CourseId { get; set; }
    }
}
