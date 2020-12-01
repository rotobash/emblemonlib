using Dialogue.Contracts;
using System.Collections.Generic;
using System.Xml;

namespace Dialogue
{
    public class DialogueBranchNode : DialogueTreeNode
    {
        public Dictionary<string, IDialogueTreeNode> Choices { get; set; }

        public override DialogueAction Action => DialogueAction.Branch;

        public DialogueBranchNode()
        {
            Choices = new Dictionary<string, IDialogueTreeNode>();
        }

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            if (!visitor.Prompt && visitor.Choice < 0)
            {
                visitor.Prompt = true;
                visitor.EndBlock = true;
                visitor.Choice = -1;
                visitor.CurrentText = string.Join("\n", Choices.Keys);
                visitor.SetNext(this);
            }
            else if (visitor.Prompt && visitor.Choice >= 0)
            {
                visitor.Prompt = false;
                var choices = 0;
                foreach (var value in Choices.Values)
                {
                    if (visitor.Choice != choices++)
                        continue;
                    else
                    {
                        visitor.SetNext(value);
                        break;
                    }
                }
            }
            else
            {
                visitor.SetNext(parent);
            }
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueBranchNode));
            fileWriter.WriteStartElement(nameof(Choices));
            foreach (var choice in Choices)
            {
                fileWriter.WriteStartElement("Choice");
                fileWriter.WriteStartElement("Prompt");
                fileWriter.WriteString(choice.Key);
                fileWriter.WriteEndElement();
                fileWriter.WriteStartElement("Branch");
                choice.Value.Serialize(fileWriter);
                fileWriter.WriteEndElement();
                fileWriter.WriteEndElement();
            }
            fileWriter.WriteEndElement();
            fileWriter.WriteEndElement();

        }

        public override void Deserialize(XmlReader fileReader)
        {
            fileReader.ReadStartElement();
            fileReader.ReadStartElement();
            while (fileReader.IsStartElement())
            {
                // Choice
                fileReader.ReadStartElement();
                // Prompt
                fileReader.ReadStartElement();
                var key = fileReader.ReadContentAsString();
                fileReader.ReadEndElement();
                // Branch
                fileReader.ReadStartElement();
                IDialogueTreeNode node = CreateNode(NameToAction(fileReader.Name));
                node.Deserialize(fileReader);
                node.SetParent(this);
                fileReader.ReadEndElement();
                fileReader.ReadEndElement();

                Choices.Add(key, node);
            } 
            fileReader.ReadEndElement();
        }
    }
}
