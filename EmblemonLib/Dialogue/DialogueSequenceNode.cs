using System.Collections.Generic;
using System.Xml;
using Dialogue.Contracts;

namespace Dialogue
{
    public class DialogueSequenceNode : DialogueTreeNode
    {
        public List<IDialogueTreeNode> Sequence { get; set; }
        public override DialogueAction Action => DialogueAction.Sequence;

        private int actionsPerformedCount;

        public DialogueSequenceNode(List<IDialogueTreeNode> sequence)
        {
            actionsPerformedCount = 0;
            Sequence = sequence ?? new List<IDialogueTreeNode>();
            foreach (var child in Sequence)
                child.SetParent(this);
        }

        public DialogueSequenceNode()
        {

        }

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            if (actionsPerformedCount < Sequence.Count)
            {
                var child = Sequence[actionsPerformedCount++];

                // visiting the child will set the next node to visit
                // it will be this node if the child is a leaf node
                visitor.Visit(child);
                if (!(child is DialogueSequenceNode || child is DialogueBranchNode || child is DialogueConditionalNode))
                    visitor.SetNext(this);
            }
            else
            {
                visitor.SetNext(parent);
            }
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueSequenceNode));
            fileWriter.WriteStartElement(nameof(Sequence));
            foreach (var step in Sequence)
                step.Serialize(fileWriter);
            fileWriter.WriteEndElement();
            fileWriter.WriteEndElement();
        }

        public override void Deserialize(XmlReader fileReader)
        {
            Sequence = new List<IDialogueTreeNode>();
            fileReader.ReadStartElement();
            fileReader.ReadStartElement();
            while (fileReader.IsStartElement())
            {
                IDialogueTreeNode node = CreateNode(NameToAction(fileReader.Name));
                node.Deserialize(fileReader);
                node.SetParent(this);
                Sequence.Add(node);
            }
            fileReader.ReadEndElement();
            fileReader.ReadEndElement();
        }
    }
}
