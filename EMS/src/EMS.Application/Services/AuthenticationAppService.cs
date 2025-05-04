using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using Volo.Abp.Application.Services;
using System.Linq;
using System.IO;
using Volo.Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Polly;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Data;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.TenantManagement;
using static Volo.Abp.TenantManagement.TenantManagementPermissions;

using Microsoft.Extensions.Options;

using Volo.Abp.MultiTenancy;
using System.Xml.Linq;

using EMS.IServices;
using EMS.Shared;
using Volo.Abp.Users;
using EMS.IServices;
using Microsoft.AspNetCore.Http;
using OpenIddict.Server;
using EMS.DTO;
using EMS.Entities;
using Volo.Abp.Account;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace EMS.Services;
// Service Implementation 

/// <inheritdoc/>
public class AuthenticationAppService : ApplicationService, IAuthenticationAppService
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IOpenIddictTokenManager _tokenManager;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IRepository<IdentityUser, Guid> _userRepository;
    IIdentityRoleRepository _roleRepository;
    private readonly IConfiguration _config;
    private readonly IRepository<Tenant, Guid> _tenantRepository;
    private readonly IOptionsMonitor<OpenIddictServerOptions> _oidcOptions;
    private readonly IRepository<AppUsers, Guid> _appUserRepository;

    public AuthenticationAppService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOpenIddictTokenManager tokenManager,
        IHttpContextAccessor httpContext,
        IRepository<IdentityUser, Guid> userRepository,
        IIdentityRoleRepository roleRepository,
        IConfiguration config,
        IOptionsMonitor<OpenIddictServerOptions> oidcOptions, IRepository<AppUsers, Guid> appUserRepository

        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenManager = tokenManager;
        _httpContext = httpContext;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _config = config;
        _oidcOptions = oidcOptions;
         _appUserRepository = appUserRepository;
    }
    /// <inheritdoc/>
    //public async Task<Tenant> GetTenantByNameDtoAsync(string name)
    //{
    //    var query = await _tenantRepository.GetQueryableAsync();
    //   query=query.Where(t => t.Name == name);
    //    var tenant = await AsyncExecuter.FirstOrDefaultAsync(query);
    //    return tenant;

    //}
    /// <inheritdoc/>
    //public async Task<Result<forgetpasswordDto>> ForgetPasswordAsync(string phone)
    //{
    //    try
    //    {
    //        var user = await CheckPhoneISFound(phone);
    //        return Result<forgetpasswordDto>.Success(new forgetpasswordDto
    //        {
    //            UserId = user.Id.ToString(),
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return  Result<forgetpasswordDto>.Error(errorMessages : ex.Message);
    //    }

    //}

    //private async Task<IdentityUser> CheckPhoneISFound(string phone)
    //{
    //    var user = await _userRepository.FirstOrDefaultAsync(u => u.PhoneNumber == phone);

    //    if (user == null)
    //    {
    //        throw new ValidationException("User with this phone number does Not exist.");
    //    }
    //    return user;
    //}

    /// <inheritdoc/>
    //public async Task<Result<UserDataDto>> GetUserDataAsync()
    //{


    //    var userName = CurrentUser.UserName;
    //    if(userName.IsNullOrEmpty())
    //    {
    //        return Result<UserDataDto>.NotFound();
    //    }
    //    var user = await _userManager.FindByNameAsync(userName);
    //    if(user is not null)
    //    {
    //        return Result<UserDataDto>.Success(new UserDataDto
    //        {
    //            FullName = user.UserName,
    //            Email = user.Email,
    //            Phone = user.PhoneNumber,
    //            Image = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}{_httpContext.HttpContext.Request.PathBase}/images/users/149071.png"
    //        });
    //    }
    //    return Result<UserDataDto>.NotFound();

    //}
    /// <inheritdoc/>
    public async Task<LoginResultDto> LoginAsync(LoginDto input)
    {


        // Step 1: Validate the user's credentials
        var user = await _userRepository.FirstOrDefaultAsync(u => u.UserName == input.PhoneNumber);
        if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
        {
            return new LoginResultDto()
            {

                Message = "Invalid credentials."
            };

        }

        await _signInManager.SignInAsync(user, isPersistent: false);


        var roles = await _userManager.GetRolesAsync(user);
        return new LoginResultDto()
        {
            AccessToken = await GenerateTokenAsync(user) ?? string.Empty,
            Role = roles.FirstOrDefault(),
            Message = "Success"

        };

    }
    ///// <inheritdoc/>
    //[RemoteService(IsEnabled = false)]
    //public async Task<IdentityUser> LoginForWebAsync(LoginInputDto input)
    //{
    //    IdentityUser user;
    //    using (DataFilter.Disable<IMultiTenant>())
    //    {

    //        if (input.Username.Contains("@"))
    //        {
    //            user = await _userManager.FindByEmailAsync(input.Username);

    //        }
    //        else
    //        {
    //             user = await _userManager.FindByNameAsync(input.Username);

    //        }

    //        var organizationId = user.GetProperty<int>("OrganizationId");
    //        if (organizationId != 0)
    //        {
    //            var organization = await _organizationRepository.FirstOrDefaultAsync(o => o.Id == organizationId);
    //            if (organization == null)
    //            {
    //                return null;

    //            }
    //            if (organization != null)
    //            {
    //                Console.WriteLine(organization.Status.ToString());
    //                Console.WriteLine(Enums.Status.Deleted.ToString());
    //                Console.WriteLine(Enums.Status.Inactive.ToString());


    //                if (organization.Status.ToString() == Enums.Status.Inactive.ToString() || organization.Status.ToString() == Enums.Status.Deleted.ToString())
    //                {
    //                    return null;

    //                }
    //            }
    //        }

    //    if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
    //    {
    //        return user;
    //    }
    //    if (user.IsActive == false)
    //    {
    //        return null;
    //    }

    //    }


    //    await _signInManager.SignInAsync(user, isPersistent: false);


    //    return user;
    //}
    /// <inheritdoc/>
    public async Task<RegisterResultDto> RegisterAsync(RegisterInputDto input)
    {

        try
        {

            var user = new AppUsers(GuidGenerator.Create(), input.UserName, input.Email,input.Height,input.Weight,input.BOD, null,input.Address);

            var createdUser = await _userManager.CreateAsync(user, input.Password);
            await _userManager.SetPhoneNumberAsync(user, input.Phone);
            if (!createdUser.Succeeded)
            {
                throw new Exception(message: createdUser.Errors.FirstOrDefault()?.Description);
            }

            await _userManager.AddToRoleAsync(user, RolesType.Customer.ToString());

            var AccessToken = await GenerateTokenAsync(user) ?? string.Empty;
            return new RegisterResultDto()
            {
                AccessToken = AccessToken,
                Message = "Success"

            };
        }
        catch (Exception ex)
        {
            return new RegisterResultDto()
            {
                Message = ex.Message

            };

        }

    }

    //}
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token will be generated.</param>
    /// <returns>A string representing the generated JWT token.</returns>
    private async Task<string> GenerateTokenAsync(IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("given_name", user.UserName),
            new Claim("email", user.Email),
            new Claim("role", string.Join(",", roles)),
            new Claim("tenantid", user.TenantId?.ToString() ?? ""),
            new Claim("scope", "address email phone roles profile offline_access EMS") //replace Qa with yours

        };
        var options = _oidcOptions.CurrentValue;
        var descriptor = new SecurityTokenDescriptor
        {
            Audience = "EMS", // replace with yours,
            EncryptingCredentials = options.DisableAccessTokenEncryption
                ? null
                : options.EncryptionCredentials.First(),
            Expires = null,
            Subject = new ClaimsIdentity(claims, TokenValidationParameters.DefaultAuthenticationType),
            IssuedAt = DateTime.UtcNow,
            Issuer = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}{_httpContext.HttpContext.Request.PathBase}", // replace with yours,
            SigningCredentials = options.SigningCredentials.First(),
            TokenType = OpenIddictConstants.JsonWebTokenTypes.AccessToken,
        };

        var accessToken = options.JsonWebTokenHandler.CreateToken(descriptor);

        return accessToken;
    }
    /// <summary>
    /// Checks if the provided phone number is unique and associated with a user.
    /// </summary>
    /// <param name="phone">The phone number to check for uniqueness.</param>
    /// <returns>The <see cref="IdentityUser"/> associated with the phone number if it exists.</returns>
    /// <exception cref="ValidationException">Thrown when the phone number is not associated with any user.</exception>
    private async Task<IdentityUser> CheckPhoneIsUnique(string phone)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.PhoneNumber == phone);

        if (user != null)
        {
            throw new ValidationException("User with this phone number does  exist.");
        }
        return user;

    }
    /// <inheritdoc/>

    //public async Task<Result> ChangePassword(ChangePasswordInputDto input)
    //{

    //    var Id = CurrentUser.FindClaim(OpenIddictConstants.Claims.Subject)?.Value;
    //    var user = await _userManager.FindByIdAsync(Id);
    //    if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
    //    {
    //        await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);
    //    }
    //    else
    //    {
    //        return Result.Error("Incorrect password.");

    //    }

    //    return Result.Success();
    //}
    /// <inheritdoc/>
    //public async Task LogOutAsync()
    //{
    //    await _signInManager.SignOutAsync();
    //}

    public async Task<Tenant> GetTenantByPhoneNumberDtoAsync(string phoneNum)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNum);
        var query = await _tenantRepository.GetQueryableAsync();
        query = query.Where(t => t.Name == user.UserName);
        var tenant = await AsyncExecuter.FirstOrDefaultAsync(query);
        return tenant;
    }



    public async Task<UserDataDto> GetCurrentUserDetailsAsync()
    {
        var userId = CurrentUser.Id;

        if (!userId.HasValue)
        {
            throw new UserFriendlyException("User is not authenticated.");
        }

        var user = await _appUserRepository.GetAsync(userId.Value);

        if (user == null)
        {
            throw new EntityNotFoundException("User not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDataDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            BOD = user.BOD.HasValue ? user.BOD.Value.ToDateTime(TimeOnly.MinValue) : null,
            Height = user.Height,
            Weight = user.Weight,
            Address = user.Address,

    
        };
    }


    public async Task<bool> UpdateProfileAsync(DTO.UpdateProfileDto input)
    {
        var userId = CurrentUser.Id;

        if (!userId.HasValue)
            return false; // User not authenticated

        // Get identity user (for UserName, Email, Phone)
        var identityUser = await _userManager.FindByIdAsync(userId.Value.ToString());
        if (identityUser == null)
            throw new EntityNotFoundException("Identity user not found.");

        // Get app user (for custom fields)
        var appUser = await _appUserRepository.GetAsync(userId.Value);
        if (appUser == null)
            throw new EntityNotFoundException("App user not found.");

        // ✅ Update UserName if changed
        if (!string.IsNullOrWhiteSpace(input.UserName) &&
            !string.Equals(identityUser.UserName, input.UserName, StringComparison.Ordinal))
        {
            var result = await _userManager.SetUserNameAsync(identityUser, input.UserName);
            if (!result.Succeeded)
                throw new UserFriendlyException(result.Errors.FirstOrDefault()?.Description);
        }

        // ✅ Update Email if changed
        if (!string.IsNullOrWhiteSpace(input.Email) &&
            !string.Equals(identityUser.Email, input.Email, StringComparison.OrdinalIgnoreCase))
        {
            var result = await _userManager.SetEmailAsync(identityUser, input.Email);
            if (!result.Succeeded)
                throw new UserFriendlyException(result.Errors.FirstOrDefault()?.Description);
        }

        // ✅ Update PhoneNumber if changed
        if (!string.IsNullOrWhiteSpace(input.PhoneNumber) &&
            !string.Equals(identityUser.PhoneNumber, input.PhoneNumber, StringComparison.Ordinal))
        {
            var result = await _userManager.SetPhoneNumberAsync(identityUser, input.PhoneNumber);
            if (!result.Succeeded)
                throw new UserFriendlyException(result.Errors.FirstOrDefault()?.Description);
        }

        // ✅ Update custom fields in AppUsers
        appUser.Height = input.Height ?? appUser.Height;
        appUser.Weight = input.Weight ?? appUser.Weight;
        appUser.Address = input.Address ?? appUser.Address;
        appUser.BOD = input.BOD != null ? DateOnly.FromDateTime(input.BOD) : appUser.BOD;

        await _appUserRepository.UpdateAsync(appUser);

        return true;
    }



}

