
namespace Dialogue
{
    /// <summary>
    /// Simulates text being typed out character by character.
    /// Pauses for punctuation marks for added effect.
    /// How to use: Set() your dialogue block, UpdateTimers to update pauses, Type() to get the current sting block, Reset() when finished and start over
    /// You can also force the entire text block to appear early by calling Finish()
    /// </summary>
    public class TextTyper
    {
        public bool IsTyping { get; private set; }

        private const double pauseDelay = .3f;
        private const double stopDelay = .8f;
        private const double characterDelay = .03f;
        bool pause, stop, character;
        double timer;
        string currentTextBlock = string.Empty;
        int currentCharIndex;


        public void Finish()
        {
            IsTyping = false;
            currentCharIndex = currentTextBlock.Length;
        }

        public void Reset()
        {
            currentCharIndex = 0;
            timer = 0;
            pause = stop = character = false;
            IsTyping = true;
            currentTextBlock = string.Empty;
        }
        
        public void Set(string dialogue)
        {
            currentTextBlock += dialogue;
            IsTyping = currentCharIndex < currentTextBlock.Length;
        }

        public void UpdateTimers(double elapsedTime)
        {
            if (character || pause || stop)
            {
                timer += elapsedTime;
                if (character && timer >= characterDelay)
                {
                    character = false;
                    timer = 0;
                }
                else if (pause && timer >= pauseDelay)
                {
                    pause = false;
                    timer = 0;
                }
                else if (stop && timer >= stopDelay)
                {
                    stop = false;
                    timer = 0;
                }
            }
        }


        public string Type()
        {
            IsTyping = currentCharIndex < currentTextBlock.Length;
            if (IsTyping && !(character || pause || stop))
            {
                var nextChar = currentTextBlock[currentCharIndex++];
                switch (nextChar)
                {
                    case ',':
                    case '"':
                    case '!':
                    case '\n':
                        pause = true;
                        break;
                    case '.':
                    case '?':
                        stop = true;
                        break;
                    default:
                        character = true;
                        break;
                }
            }
            return currentTextBlock.Substring(0, currentCharIndex);
        }
    }
}
