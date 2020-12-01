using Dialogue.Contracts;
using Dialogue.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Dialogue.Utils
{
    public class DialogueTextBlockNode : DialogueTreeNode
    {
        public string DialogueBlock { get; set; }
        public int? PortraitIndex { get; set; }
        public int? ActorIndex { get; set; }

        public bool EndsBlock { get; set; }

        public override DialogueAction Action => DialogueAction.TextBlock;

        public DialogueTextBlockNode()
        {

        }

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            visitor.CurrentText = DialogueBlock;
            visitor.EndBlock = EndsBlock;
        }

        public override string ToString()
        {
            return DialogueBlock;
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueTextBlockNode));
            fileWriter.WriteAttributeString(nameof(PortraitIndex), PortraitIndex.ToString());
            fileWriter.WriteAttributeString(nameof(ActorIndex), ActorIndex.ToString());
            fileWriter.WriteAttributeString(nameof(EndsBlock), EndsBlock.ToString());
            fileWriter.WriteStartElement(nameof(DialogueBlock));
            fileWriter.WriteString(DialogueBlock);
            fileWriter.WriteEndElement();
            fileWriter.WriteEndElement();
        }

        public override void Deserialize(XmlReader fileReafer)
        {
            PortraitIndex = int.TryParse(fileReafer.GetAttribute(nameof(PortraitIndex)), out var index)
                ? (int?)index
                : null;
            ActorIndex = int.TryParse(fileReafer.GetAttribute(nameof(ActorIndex)), out index)
                ? (int?)index
                : null;
            EndsBlock = bool.TrueString == fileReafer.GetAttribute(nameof(EndsBlock));
            fileReafer.ReadStartElement();
            DialogueBlock = fileReafer.ReadElementContentAsString(nameof(DialogueBlock), fileReafer.BaseURI);
            fileReafer.ReadEndElement();
        }
    }

}