using Dialogue.Contracts;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dialogue
{
    public class DialogueConditionalNode : DialogueTreeNode
    {
        public override DialogueAction Action => DialogueAction.Conditional;

        public List<DialogueConditionNode> IfConditions { get; set; }
        public DialogueConditionNode ElseCondition { get; set; }

        private int conditionIndex;
        private bool checkPassed;

        public override void Advance(IDialogueTreeVisitor visitor)
        {
            if (!checkPassed && conditionIndex < IfConditions.Count)
            {
                var child = IfConditions[conditionIndex++];
                if (TestCondition(visitor, child))
                {
                    // visiting the child will set the next node to visit
                    // it will be this node if the child is a leaf node
                    visitor.SetNext(child);
                    checkPassed = true;
                }
                else
                {
                    // check all conditions
                    visitor.Visit(this);
                }
            }
            else
            {
                if (!checkPassed && ElseCondition != null && TestCondition(visitor, ElseCondition))
                {
                    checkPassed = true;
                    visitor.SetNext(ElseCondition);
                }
                else
                {
                    visitor.SetNext(parent);
                }
            }
        }

        public override void Deserialize(XmlReader fileReader)
        {
            IfConditions = new List<DialogueConditionNode>();

            fileReader.ReadStartElement();
            fileReader.ReadStartElement();
            while (fileReader.IsStartElement())
            {
                var condition = new DialogueConditionNode();
                condition.Deserialize(fileReader);
                condition.SetParent(this);
                IfConditions.Add(condition);
            };
            fileReader.ReadEndElement();

            if (fileReader.IsStartElement(nameof(ElseCondition)))
            {
                fileReader.ReadStartElement(nameof(ElseCondition));
                ElseCondition = new DialogueConditionNode();
                ElseCondition.Deserialize(fileReader);
                ElseCondition.SetParent(this);
                fileReader.ReadEndElement();
            }
            fileReader.ReadEndElement();
        }

        public override void Serialize(XmlWriter fileWriter)
        {
            fileWriter.WriteStartElement(nameof(DialogueConditionalNode));
            fileWriter.WriteStartElement(nameof(IfConditions));
            foreach (var condition in IfConditions)
                condition.Serialize(fileWriter);
            fileWriter.WriteEndElement();

            if (ElseCondition != null)
            {
                fileWriter.WriteStartElement(nameof(ElseCondition));
                ElseCondition.Serialize(fileWriter);
                fileWriter.WriteEndElement();
            }
            fileWriter.WriteEndElement();
        }

        private bool TestCondition(IDialogueTreeVisitor visitor, DialogueConditionNode condition)
        {
            float a, b;
            string variable = string.IsNullOrEmpty(condition.VariableName) ? string.Empty : visitor.GetTextVariable(condition.VariableName);
            string value = condition.Value;
            switch (condition.Operator)
            {
                case DialogueConditionOperator.GreaterThan:
                    return
                        float.TryParse(variable, out a) &&
                        float.TryParse(value, out b) &&
                        a > b;
                case DialogueConditionOperator.GreaterThanEqual:
                    return
                        float.TryParse(variable, out a) &&
                        float.TryParse(value, out b) &&
                        a >= b;
                case DialogueConditionOperator.LessThan:
                    return
                        float.TryParse(variable, out a) &&
                        float.TryParse(value, out b) &&
                        a < b;
                case DialogueConditionOperator.LessThanEqual:
                    return
                        float.TryParse(variable, out a) &&
                        float.TryParse(value, out b) &&
                        a <= b;
                case DialogueConditionOperator.Set:
                    return !string.IsNullOrEmpty(variable);
                case DialogueConditionOperator.NotSet:
                    return string.IsNullOrEmpty(variable);
                case DialogueConditionOperator.NotEqual:
                    return variable != value;
                case DialogueConditionOperator.Equal:
                default:
                    return variable == value;
            }
        }
    }
}
