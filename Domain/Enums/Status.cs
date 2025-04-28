using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum KYCStatus
    {
        NEW_USER,
        CLOSED,
        ACTIVE,
        PENDING,
        INACTIVE,
        ON_HOLD,
        SUSPENDED,
        APPROVE,
        REJECT,
        BLOCK,
        UNDER_MAINTENANCE,
        LOCKED,
        FROZEN 
    }
}
