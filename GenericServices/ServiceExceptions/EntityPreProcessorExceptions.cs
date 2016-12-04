using System;

namespace GenericService.ServiceExceptions
{
    /// <summary>
    /// The entity pre processor exception
    /// </summary>
    public class EntityPreProcessorExceptions : Exception
    {
        /// <summary>
        /// intializes an instance of the entity pre processor exception
        /// </summary>
        public EntityPreProcessorExceptions() { }
        /// <summary>
        /// initializes an instance of The entity pre processor exception with message
        /// </summary>
        /// <param name="message">The message</param>
        public EntityPreProcessorExceptions(string message) : base(message) { }
        /// <summary>
        /// Initializes an instance of The entity pre processor exception with a message and an inner exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public EntityPreProcessorExceptions(string message, Exception innerException) : base(message, innerException) { }
    }
}
