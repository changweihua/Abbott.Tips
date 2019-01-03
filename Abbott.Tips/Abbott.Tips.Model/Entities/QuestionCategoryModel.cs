using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_QuestionCategory")]
    public class QuestionCategoryModel : IdentityKeyEntity<int>
    {
        public string CategoryName { get; set; }

        #region 导航属性

        public int? ParentID { get; set; }

        public QuestionCategoryModel ParentCategory { get; set; }

        public IEnumerable<QuestionCategoryModel> SubCategories { get; set; }

        [NotMapped]
        public int QuestionID { get; set; }

        public IEnumerable<QuestionModel> Questions { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
