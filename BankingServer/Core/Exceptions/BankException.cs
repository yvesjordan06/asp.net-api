using BankingServer.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankingServer.Core.Exceptions
{
    public class BankException: Exception
    {
    
        /// <summary>
        ///  The error code
        /// </summary>
        private string Code { get; set; }

   

        /// <summary>
        ///  The error details
        /// </summary>
        private string Details { get; set; }
    
    
        public BankException(string message, string code="500", string details= "An error occured") : base(message)
        {
            this.Code = code;
            this.Details = details;

        }

   


        //Static Exceptions

        //Not Found - 404
        public static BankException NotFound(string message = "Not Found",
            string details = "The requested resource was not found")
        {
            return new BankException(code: "404", message: message, details: details);
        }
    
        //Not Implemented - 501
        public static BankException NotImplemented(string message = "Not Implemented",
            string details = "The requested resource is not implemented")
        {
            return new BankException(code: "501", message: message, details: details);
        }
    
    
        //Bank Exception to Error
        public  BankError ToError()
        {
            return new BankError(this.Message, this.Code, this.Details);
        }
    
        //Error to IActionResult
        public IActionResult ToActionResult()
        {
            return new ObjectResult(this.ToError())
            {
                StatusCode = int.Parse(this.Code)
            };
        }
    
    
    }
}