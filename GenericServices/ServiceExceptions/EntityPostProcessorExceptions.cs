using System;

namespace GenericService.ServiceExceptions
{
    /// <summary>
    /// The entity post processor exception
    /// </summary>
    public class EntityPostProcessorExceptions : Exception
    {
        /// <summary>
        /// intializes an instance of the entity post processor exception
        /// </summary>
        public EntityPostProcessorExceptions() { }
        /// <summary>
        /// initializes an instance of The entity post processor exception with message
        /// </summary>
        /// <param name="message"></param>
        public EntityPostProcessorExceptions(string message) : base(message) { }
        /// <summary>
        /// Initializes an instance of The entity post processor exception with a message and an inner exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public EntityPostProcessorExceptions(string message, Exception innerException) : base(message, innerException) { }
    }
}
