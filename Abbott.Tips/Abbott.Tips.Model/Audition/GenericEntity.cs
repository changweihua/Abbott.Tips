using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Audition
{
    /// <summary>
    /// Database Model 泛型基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class IdentityKeyEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 只包含新增
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CreationAuditedEntity<TKey> : IdentityKeyEntity<TKey>
    {
        protected CreationAuditedEntity()
        {
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        [NotMapped]
        public string CreatedUtcTime
        {
            get
            {
                return CreatedTime.ToUniversalTime().ToString("r");
            }
        }

        public int CreatedBy { get; set; }

        #region 重写

        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            CreationAuditedEntity<TKey> entity = obj as CreationAuditedEntity<TKey>;
            if (entity == null)
            {
                return false;
            }
            return Id.Equals(entity.Id) && CreatedTime.Equals(entity.CreatedTime);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ CreatedTime.GetHashCode();
        }

        #endregion

    }

    /// <summary>
    /// 增删改查全支持
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        [NotMapped]
        public string UpdatedUtcTime
        {
            get
            {
                if (!UpdatedTime.HasValue)
                {
                    return string.Empty;
                }
                return UpdatedTime.GetValueOrDefault().ToUniversalTime().ToString("r");
            }
        }

        public int? UpdatedBy { get; set; }

        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }

    /// <summary>
    /// 软删除标记接口
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 资源接口
    /// 实现该接口，表示资源只能有创建人进行编辑和删除，或者由指定角色进行操作
    /// </summary>
    public interface IResource
    {
        string Creator { get; }
    }
}
