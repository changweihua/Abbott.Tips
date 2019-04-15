using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Dtos.Query
{
    public class NullableIdDto<TId> where TId : struct
    {
        public NullableIdDto() { }
        public NullableIdDto(TId? id) {
            Id = id;
        }
        public TId? Id { get; set; }
    }
}
