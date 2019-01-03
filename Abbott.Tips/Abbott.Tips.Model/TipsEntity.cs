using Abbott.Tips.Framework.Audition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model
{
    public abstract class TipsEntity : AuditedEntity<int>, ISoftDelete, IResource
    {
        public TipsEntity()
        {
            IsDeleted = false;
        }

        public bool IsDeleted { get; set; }

        public string Creator => CreatedBy.ToString();
    }
}
