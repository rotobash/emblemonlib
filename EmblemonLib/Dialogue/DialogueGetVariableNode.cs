using Dialogue.Contracts;
using System.Xml;

namespace Dialogue
{
    public class DialogueGetVariableNode : DialogueTreeNode
    {
        public string VariableName { get; set; }
        public override DialogueAction Action => DialogueAction.Get;

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            visitor.CurrentText = visitor.GetTextVariable(VariableName);
        }

        public override void Deserialize(XmlReader fileReader)
        {
            VariableName = fileReader.GetAttribute(nameof(VariableName));
            fileReader.ReadStartElement();
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueGetVariableNode));
            fileWriter.WriteAttributeString(nameof(VariableName), VariableName);
            fileWriter.WriteEndElement();
        }
    }
}
