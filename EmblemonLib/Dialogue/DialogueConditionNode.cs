using Dialogue.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dialogue.Utils
{
    public class DialogueConditionNode : DialogueTreeNode
    {
        public string VariableName { get; set; }
        public DialogueConditionOperator Operator { get; set; }
        public string Value { get; set; }

        public IDialogueTreeNode IfTrue { get; set; }

        private bool hasChecked;

        public DialogueConditionNode()
        {
        }

        public override DialogueAction Action => DialogueAction.Condition;

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            if (!hasChecked)
            {
                visitor.Visit(IfTrue);
                hasChecked = true;
            }
            else
            {
                visitor.SetNext(parent);
            }
        }

        public override void Deserialize(XmlReader fileReader)
        {
            VariableName = fileReader.GetAttribute(nameof(VariableName));
            Operator = Enum.TryParse<DialogueConditionOperator>(fileReader.GetAttribute(nameof(Operator)), out var op)
                ? op
                : DialogueConditionOperator.Equal;
            Value = fileReader.GetAttribute(nameof(Value));
            fileReader.ReadStartElement(nameof(DialogueConditionNode));
            fileReader.ReadStartElement(nameof(IfTrue));
            IfTrue = CreateNode(NameToAction(fileReader.Name));
            IfTrue.Deserialize(fileReader);
            IfTrue.SetParent(this);
            fileReader.ReadEndElement();
            fileReader.ReadEndElement();
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueConditionNode));
            fileWriter.WriteAttributeString(nameof(VariableName), VariableName);
            fileWriter.WriteAttributeString(nameof(Operator), Operator.ToString());
            fileWriter.WriteAttributeString(nameof(Value), Value);
            fileWriter.WriteStartElement(nameof(IfTrue));
            IfTrue.Serialize(fileWriter);
            fileWriter.WriteEndElement();
            fileWriter.WriteEndElement();
        }
    }
}
