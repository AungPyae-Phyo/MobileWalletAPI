using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum BankStatus
    {
        //Basic statuses
        NEW_USER,
        CLOSED,

        //Verification statuses
        ACTIVE,
        PENDING,
        INACTIVE,   
        ON_HOLD,
        SUSPENDED,
        APPROVE,
        REJECT,
        BLOCK,

        // Technical statuses
        UNDER_MAINTENANCE,

        // Security-related statuses
        LOCKED,
        FROZEN,
    }
}
