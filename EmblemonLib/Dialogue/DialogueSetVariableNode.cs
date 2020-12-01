using Dialogue.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dialogue.Utils
{
    public class DialogueSetVariableNode : DialogueTreeNode
    {
        public string VariableName { get; set; }
        public string Value { get; set; }
        public override DialogueAction Action => DialogueAction.Set;

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            visitor.SetTextVariable(VariableName, Value);
        }

        public override void Deserialize(XmlReader fileReader)
        {
            VariableName = fileReader.GetAttribute(nameof(VariableName));
            fileReader.ReadStartElement();
            Value = fileReader.ReadContentAsString();
            fileReader.ReadEndElement();
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueSetVariableNode));
            fileWriter.WriteAttributeString(nameof(VariableName), VariableName);
            fileWriter.WriteString(Value);
            fileWriter.WriteEndElement();
        }
    }
}
