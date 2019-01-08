using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Event")]
    public class TEventModel : TipsEntity
    {
        public Guid EventId { get; set; }

        public string EventPayload { get; set; }

        public DateTime EventTimestamp { get; set; }
    }
}
