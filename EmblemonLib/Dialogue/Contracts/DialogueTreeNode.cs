using Dialogue.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dialogue.Contracts
{
    public abstract class DialogueTreeNode : IDialogueTreeNode
    {
        protected IDialogueTreeNode parent;
        public DialogueTreeNode()
        {

        }

        public abstract DialogueAction Action { get; }

        public abstract void Advance(IDialogueTreeVisitor visitor);
        public abstract void Serialize(XmlWriter fileWriter);
        public abstract void Deserialize(XmlReader fileReader);

        public static IDialogueTreeNode CreateNode(DialogueAction action)
        {
            switch (action)
            {
                case DialogueAction.Branch:
                    return new DialogueBranchNode();
                case DialogueAction.Sequence:
                    return new DialogueSequenceNode();
                case DialogueAction.Conditional:
                    return new DialogueConditionalNode();
                case DialogueAction.Condition:
                    return new DialogueConditionNode();
                case DialogueAction.Get:
                    return new DialogueGetVariableNode();
                case DialogueAction.Set:
                    return new DialogueSetVariableNode();
                case DialogueAction.TextBlock:
                default:
                    return new DialogueTextBlockNode();
            }
        }

        public static DialogueAction NameToAction(string name)
        {
            switch (name)
            {
                case nameof(DialogueBranchNode):
                    return DialogueAction.Branch;
                case nameof(DialogueSequenceNode):
                    return DialogueAction.Sequence;
                case nameof(DialogueConditionalNode):
                    return DialogueAction.Conditional;
                case nameof(DialogueConditionNode):
                    return DialogueAction.Condition;
                case nameof(DialogueGetVariableNode):
                    return DialogueAction.Get;
                case nameof(DialogueSetVariableNode):
                    return DialogueAction.Set;
                case nameof(DialogueTextBlockNode):
                default:
                    return DialogueAction.TextBlock;
            }
        }

        public IDialogueTreeNode GetParent()
        {
            return parent;
        }
        public virtual void SetParent(IDialogueTreeNode value)
        {
            parent = value;
        }
    }
}
