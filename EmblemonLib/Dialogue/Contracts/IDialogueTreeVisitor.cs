using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dialogue.Utils;

namespace Dialogue.Contracts
{
    public interface IDialogueTreeVisitor
    {
        int Choice { get; set; }
        bool Prompt { get; set; }
        bool End { get; set; }
        bool EndBlock { get; set; }

        string CurrentText { get; set; }

        void Visit(IDialogueTreeNode node);
        void SetNext(IDialogueTreeNode node);

        string GetTextVariable(string variableName);
        void SetTextVariable(string variableName, string value);
    }
}
