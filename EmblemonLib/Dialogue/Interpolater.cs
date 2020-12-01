using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dialogue.Contracts;
using Dialogue.Utils;

namespace Dialogue
{
    public class Interpolater
    {
        Dictionary<string, bool> boolVariables;
        Dictionary<string, string> textVariables;
        Regex tokenSplit;

        string[] currentDialogue;
        string currentWord;
        int currentWordIndex;

        public Interpolater()
        {
            tokenSplit = new Regex(" ");
            currentWordIndex = 0;
            currentWord = currentDialogue[currentWordIndex];
        }

        private DialogueSequenceNode BuildDialogueTree(int position = 0)
        {
            
            if (position < currentDialogue.Length)
            {
                var word = currentDialogue[position + 1];
                switch (word)
                {
                    case "{{":
                        break;
                    case "??":
                        break;
                    case ":?":
                        break;
                    case "::":
                        break;
                    case "##":
                        break;
                    case "->":
                        break;
                    default:
                        break;
                }
            }

            return null;
        }
    }
}
