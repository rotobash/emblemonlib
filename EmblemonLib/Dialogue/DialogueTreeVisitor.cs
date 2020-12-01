using System.Collections.Generic;
using Dialogue.Contracts;

namespace Dialogue
{
    public partial class DialogueTreeVisitor : IDialogueTreeVisitor
    {
        private Dictionary<string, string> textVariables;

        /// <summary>
        /// 
        /// </summary>
        public string CurrentText { get; set; }
        public bool Prompt { get; set; }
        public int Choice { get; set; } = -1;
        /// <summary>
        /// Signifies the end of the dialogue, 
        /// </summary>
        public bool End { get; set; }

        /// <summary>
        /// Signifies the start of a new text box.
        /// If true, the text being displayed should be reset and
        /// the next visited node should be the start of a new block of text.
        /// </summary>
        public bool EndBlock { get; set; }

        internal IDialogueTreeNode nextNode;
        public int? CurrentActorId { get; private set; }
        public int? CurrentPortraitId { get; private set; }
        string oldText;

        public string GetTextVariable(string name)
        {
            var value = string.Empty;
            if (textVariables.ContainsKey(name))
                value = textVariables[name];
            else
                textVariables.Add(name, value);

            return value;
        }

        public void SetTextVariable(string name, string value)
        {
            if (textVariables.ContainsKey(name))
                textVariables[name] = value;
            else
                textVariables.Add(name, value);
        }

        public DialogueTreeVisitor()
        {
            textVariables = new Dictionary<string, string>();
        }

        public DialogueTreeVisitor(Dictionary<string, string> state)
        {
            textVariables = state;
        }

        public void ResetBlock()
        {
            CurrentText = string.Empty;
            oldText = string.Empty;
            EndBlock = false;
        }

        public void Advance()
        {
            if (EndBlock)
                ResetBlock();

            while (CurrentText == oldText &&!EndBlock && nextNode != null)
            {
                Visit(nextNode);
            }

            oldText = CurrentText;
            if (nextNode == null)
            {
                End = true;
            }
        }

        public void SetNext(IDialogueTreeNode node)
        {
            nextNode = node;
        }

        public void Visit(IDialogueTreeNode node)
        {
            switch(node.Action)
            {
                case DialogueAction.Sequence:
                    (node as DialogueSequenceNode).Advance(this);
                    break;
                case DialogueAction.Branch:
                    (node as DialogueBranchNode).Advance(this);
                    break;
                case DialogueAction.Conditional:
                    (node as DialogueConditionalNode).Advance(this);
                    break;
                case DialogueAction.Condition:
                    (node as DialogueConditionNode).Advance(this);
                    break;
                case DialogueAction.Get:
                    (node as DialogueGetVariableNode).Advance(this);
                    break;
                case DialogueAction.Set:
                    (node as DialogueSetVariableNode).Advance(this);
                    break;
                case DialogueAction.TextBlock:
                {
                    if (node is DialogueTextBlockNode text)
                    {
                        CurrentActorId = text.ActorIndex ?? CurrentActorId;
                        CurrentPortraitId = text.PortraitIndex ?? CurrentPortraitId;
                        text.Advance(this);
                    }
                    break;
                }
            }
        }
    }
}
