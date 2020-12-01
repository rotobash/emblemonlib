using System;
using System.Runtime.Serialization;
using Dialogue.Contracts;

namespace Dialogue.Exceptions
{
    [Serializable]
    public class InvalidParentDialogueTreeNodeException : Exception
    {
        private const string message = "Invalid parent at node:\n\n{0}\n\nAction nodes cannot have Action nodes as parents and everything else must have an Action node as a parent.";
        public InvalidParentDialogueTreeNodeException(IDialogueTreeNode nodeType) : base(string.Format(message, nodeType))
        {
        }

        public InvalidParentDialogueTreeNodeException(IDialogueTreeNode nodeType, Exception innerException) : base(string.Format(message, nodeType), innerException)
        {
        }

        protected InvalidParentDialogueTreeNodeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
