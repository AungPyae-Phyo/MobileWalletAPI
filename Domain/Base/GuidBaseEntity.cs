using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GuidBaseEntity:BaseEntity<string>
    {
        public GuidBaseEntity() : base()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
        }
    }
}
