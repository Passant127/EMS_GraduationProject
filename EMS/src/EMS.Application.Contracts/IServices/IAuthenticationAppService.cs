using EMS.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMS.IServices
{
    /// <summary>
    /// Interface for authentication-related operations.
    /// </summary>
    public interface IAuthenticationAppService : IApplicationService
    {

        /// <summary>
        /// Logs in a user with the provided input.
        /// </summary>
        /// <param name="input">The login input containing username and password.</param>
        /// <returns>A <see cref="LoginResultDto"/> containing the result of the login attempt.</returns>
        Task<LoginResultDto> LoginAsync(LoginDto input);

        /// <summary>
        /// Registers a new user with the provided input.
        /// </summary>
        /// <param name="input">The registration input containing user details.</param>
        /// <returns>A <see cref="RegisterResultDto"/> containing the result of the registration attempt.</returns>
        Task<RegisterResultDto> RegisterAsync(RegisterInputDto input);

        Task<UserDataDto> GetCurrentUserDetailsAsync();


    }
}
