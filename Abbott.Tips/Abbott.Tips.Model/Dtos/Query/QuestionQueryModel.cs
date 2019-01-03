using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    public class QuestionCreateModel
    {
        public string QuestionContent { get; set; }

        public int CategoryId { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        public string CorrectAnswer { get; set; }
    }

    /// <summary>
    /// 试题库试题列表查询模型
    /// </summary>
    public class QuestionListQueryModel : LayTablePagerParameterQueryModel
    {
        public string QuestionContent { get; set; }

        public int CategoryId { get; set; }
    }
}
