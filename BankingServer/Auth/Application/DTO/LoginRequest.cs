namespace BankingServer.Auth.Application.DTO
{
    /// <summary>
    /// Defines the login request values.
    /// </summary>
    public struct LoginRequest
    {
        /// <summary>
        /// The Login Email.
        /// </summary>
        public string Email { get; set; }
    
        /// <summary>
        ///  The Login Password.
        /// </summary>
        public string Password { get; set; }
    
    }
}