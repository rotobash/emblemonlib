using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Dialogue.Contracts
{
    public interface IDialogueTreeNode
    {
        DialogueAction Action { get; }
        IDialogueTreeNode GetParent();
        void SetParent(IDialogueTreeNode value);

        void Advance(IDialogueTreeVisitor visitor);

        void Serialize(XmlWriter fileWriter);

        void Deserialize(XmlReader fileReader);
    }
}
