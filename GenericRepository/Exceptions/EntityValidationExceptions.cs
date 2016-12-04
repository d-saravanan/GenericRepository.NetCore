using System;

namespace GenericRepository.Entities
{
    /// <summary>
    /// The entity validation exception
    /// </summary>
    public class EntityValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the System.Exception class.
        /// </summary>
        public EntityValidationException() { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class. with the message
        /// </summary>
        /// <param name="message">the message</param>
        public EntityValidationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with the message and the inner exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">the inner exception</param>
        public EntityValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
