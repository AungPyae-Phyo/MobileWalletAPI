using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }

       // string? DeletedBy { get; set; }

        bool? IsDeleted { get; set; }
    }
}
