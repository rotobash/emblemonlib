using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Contracts
{
    public enum DialogueAction
    {
        Sequence,
        Branch,
        TextBlock,
        Conditional,
        Condition,
        Get,
        Set
    }
    public enum DialogueConditionOperator
    {
        Equal,
        NotEqual,
        NotSet,
        Set,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual
    }
}
