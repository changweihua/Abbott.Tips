using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Question")]
    public class QuestionModel : IdentityKeyEntity<int>
    {
        public string QuestionContent { get; set; }
        public int QuestionStatus { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }

        #region 导航属性

        public int QuestionCategoryID { get; set; }

        public QuestionCategoryModel QuestionCategory { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
