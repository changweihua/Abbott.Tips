using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class QuestionListModel
    {
        public int ID { get; set; }

        public int QuestionCategoryID { get; set; }

        public string QuestionCategoryName { get; set; }

        public string QuestionContent { get; set; }
        public int QuestionStatus { get; set; }
        public string QuestionStatusText { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }

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
