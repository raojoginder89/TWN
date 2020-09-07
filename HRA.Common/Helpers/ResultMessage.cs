using System.Collections.Generic;
using System.Linq;

namespace HRA.Common.Helpers
{
    /// <summary>
    /// The result message.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    public class ResultMessage<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage{T}"/> class.
        /// </summary>
        public ResultMessage()
        {
            this.Messages = new List<Message>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage{T}"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public ResultMessage(Message msg)
        {
            this.Messages = new List<Message> { msg };
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Gets a value indicating whether this is error.
        /// </summary>
        public bool IsError => Messages.Any();
    }
}
