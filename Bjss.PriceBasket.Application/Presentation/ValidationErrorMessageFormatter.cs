using Bjss.PriceBasket.Application.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bjss.PriceBasket.Console.View
{
    /// <summary>
    /// Formats the validation errors produced while processing the proce basket
    /// </summary>
    public class ValidationErrorMessageFormatter : IValidationErrorsMessageFormatter
    {
        /// <summary>
        /// Processes an enumeration of validation errors and converts them to a string repsentation
        /// </summary>
        /// <param name="validationErrors">Enumeraion of validation errors</param>
        /// <returns>String representation of the erros</returns>
        public string Format(IEnumerable<ValidationError> validationErrors)
        {
            if(validationErrors == null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            var messageBuilder = new StringBuilder();

            foreach(var validationError in validationErrors)
            {
                if(messageBuilder.Length == 0)
                {
                    messageBuilder.Append("Please correct the following errors and try again:" + Environment.NewLine);
                }

                messageBuilder.Append(GetMessageForError(validationError.Error, validationError.Product) + Environment.NewLine);
            }

            return messageBuilder.ToString();
        }

        private string GetMessageForError(ValidationErrors validationError, string product)
        {
            var message = string.Empty;

            switch(validationError)
            {
                case ValidationErrors.NoProductsSelected:
                    message = string.Format(ErrorMessageTemplates.NoProductsSelected, product);
                    break;

                case ValidationErrors.OutOfStock:
                    message = string.Format(ErrorMessageTemplates.ProductOutOfStock, product);
                    break;

                case ValidationErrors.ProductNotFound:
                    message = string.Format(ErrorMessageTemplates.ProductNotFound, product);
                    break;

                default:
                    throw new NotImplementedException($"Validation error {validationError} is currently not supported.");
            }

            return message;
        }
    }
}
