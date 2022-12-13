using Microsoft.AspNetCore.Mvc;

namespace BankingServer.Core.Entities
{
    /// <summary>
    ///  Describe a Classic Error used by the application
    /// </summary>
    public class BankError
    {
        public string Message { get; private set; }
        public string Details { get; private set; }
        public string Code { get; private set; }

        public BankError(string message="An error occured", string code="500", string details= "An error occured")
        {
            this.Code = code;
            this.Details = details;
            this.Message = message;
        }
    
        //Static Errors
    
        //Not Found - 404
        public static BankError NotFound(string message = "Not Found",
            string details = "The requested resource was not found")
        {
            return new BankError(code: "404", message: message, details: details);
        }
    
        //Not Implemented - 501
        public static BankError NotImplemented(string message = "Not Implemented",
            string details = "The requested resource is not implemented")
        {
            return new BankError(code: "501", message: message, details: details);
        }
    
    
        //Bank Errors to Response IActionResult
        public  IActionResult ToResponse()
        {
            return new ObjectResult(this)
            {
                StatusCode = int.Parse(this.Code)
            };
        }
    }
}