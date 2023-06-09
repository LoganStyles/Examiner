using System.Linq.Expressions;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using Examiner.Application.Users.Interfaces;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Dtos;
using Examiner.Common;
using AutoMapper.Internal.Mappers;
using System.Globalization;
using Examiner.Domain.Entities.Content;
using Examiner.Domain.Dtos.Content;

namespace Examiner.Application.Users.Services;

/// <summary>
/// Implements contract for fetching user details
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Fetches a user by email
    /// </summary>
    /// <param name="email">The email of the user to fetch</param>
    /// <returns>An object holding data indicating the success or failure of fetching the users</returns>
    public async Task<UserIdentity?> GetUserByEmail(string email)
    {
        // var response = new UserResponse(false,$"{AppMessages.USER} {AppMessages.NOT_EXIST}");
        try
        {
            Func<IQueryable<UserIdentity>, IOrderedQueryable<UserIdentity>>? orderBy = null;
            Expression<Func<UserIdentity, bool>>? filter = (u => u.Email == email);

            var users = await _unitOfWork.UserRepository.Get(filter, orderBy, "CodeVerification", null, null);
            if (users.Count() > 0)
            {
                return users.FirstOrDefault();
            }
            return null;

        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Removes a user by email
    /// </summary>
    /// <param name="email">The email of the user to remove</param>
    /// <returns>An object holding data indicating the success or failure of removing the user</returns>
    public async Task<GenericResponse> RemoveUserByEmail(string email)
    {
        var response = new GenericResponse(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}");
        try
        {

            var existingUserList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(email), null, "CodeVerification,UserProfile", null, null);
            var userFound = existingUserList.FirstOrDefault();
            if (userFound is null)
                return response;

            if (userFound.UserProfile is not null)
                await _unitOfWork.UserRepository.DeleteAsync(userFound.UserProfile.Id);
            if (userFound.CodeVerification is not null)
                await _unitOfWork.UserRepository.DeleteAsync(userFound.CodeVerification.Id);

            await _unitOfWork.UserRepository.DeleteAsync(userFound.Id);

            await _unitOfWork.CompleteAsync();

            response.ResultMessage = $"{AppMessages.USER} {AppMessages.REMOVAL} {AppMessages.SUCCESSFUL}";
            response.Success = true;

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            response.ResultMessage = AppMessages.EXCEPTION_ERROR + "removing " + AppMessages.USER;
            return response;
            // throw;
        }
    }

    /// <summary>
    /// Fetches a user by Id
    /// </summary>
    /// <param name="Id">An Id representing the user to be fetched</param>
    /// <returns>An object holding data indicating the success or failure of fetching the user</returns>
    public async Task<UserIdentity?> GetByIdAsync(Guid Id)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(Id);
            if (user is not null)
            {
                return user;
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            throw;
        }
    }


    // /// <summary>
    // /// Updates a user's phone number
    // /// </summary>
    // /// <param name="request">An object holding email & mobile phone request data</param>
    // /// <returns>An object holding data indicating the success or failure of a user's phone update</returns>
    // public async Task<GenericResponse> PhoneUpdateAsync(PhoneUpdateRequest request, Guid userId)
    // {

    //     try
    //     {
    //         var userProfileList = await _unitOfWork.UserProfileRepository.Get(u => u.UserId.Equals(userId), null, "", null, null);

    //         var userProfileFound = userProfileList.FirstOrDefault();
    //         if (userProfileFound is null)
    //             return GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}");

    //         // check if country code is among supported countries
    //         // confirm phone number pattern
    //         userProfileFound.CountryCode = request.CountryCode;
    //         userProfileFound.MobilePhone = request.MobilePhone;

    //         await _unitOfWork.UserProfileRepository.Update(userProfileFound);
    //         await _unitOfWork.CompleteAsync();

    //         return GenericResponse.Result(true, $"{AppMessages.MOBILE_PHONE} {AppMessages.UPDATE} {AppMessages.SUCCESSFUL}");
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError("Error updating phone - ", ex.Message);
    //         return GenericResponse.Result(false, ex.Message);
    //     }
    // }

    /// <summary>
    /// Updates a user's profile
    /// </summary>
    /// <param name="request">An object holding profile request data</param>
    /// <returns>An object holding data indicating the success or failure of a user's profile update</returns>
    public async Task<GenericResponse> ProfileUpdateAsync(ProfileUpdateRequest request, Guid userId)
    {

        try
        {
            var userProfileList = await _unitOfWork.UserProfileRepository.Get(u => u.UserId.Equals(userId), null, "", null, null);

            var userProfileFound = userProfileList.FirstOrDefault();
            if (userProfileFound is null)
                return GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}");

            // check if country code is among supported countries
            userProfileFound.CountryCode = request.CountryCode;
            // confirm phone number pattern
            userProfileFound.MobilePhone = request.MobilePhone;
            userProfileFound.FirstName = request.FirstName;
            userProfileFound.LastName = request.LastName;
            userProfileFound.Location = request.Location;
            userProfileFound.ShortDescription = request.ShortDescription;
            userProfileFound.DateOfBirth = new DateOnly(request.DateOfBirth.Year, request.DateOfBirth.Month, request.DateOfBirth.Day);

            if (userProfileFound.Subjects is null)
                userProfileFound.Subjects = new HashSet<Subject>();

            var subjectList = await _unitOfWork.SubjectRepository.Get(s => request.SubjectIds.Contains(s.Id), null, "", null, null);
            // prevent duplicate entries
            userProfileFound.Subjects.UnionWith(subjectList);
            // foreach (var item in subjectList)
            // {
            //     userProfileFound.Subjects.Add(item);
            // }

            await _unitOfWork.UserProfileRepository.Update(userProfileFound);
            await _unitOfWork.CompleteAsync();

            return GenericResponse.Result(true, $"{AppMessages.USER} {AppMessages.UPDATE} {AppMessages.SUCCESSFUL}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating phone - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    public async Task<GenericResponse> ProfilePhotoUpdateAsync(string filePath, Guid userId)
    {
        try
        {
            var userProfileList = await _unitOfWork.UserProfileRepository.Get(u => u.UserId.Equals(userId), null, "", null, null);

            var userProfileFound = userProfileList.FirstOrDefault();
            if (userProfileFound is null)
                return GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}");

            userProfileFound.ProfilePhotoPath = filePath;

            await _unitOfWork.UserProfileRepository.Update(userProfileFound);
            await _unitOfWork.CompleteAsync();

            return GenericResponse.Result(true, $"{AppMessages.PROFILE_PHOTO} {AppMessages.UPDATE} {AppMessages.SUCCESSFUL}");

        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating profile photo - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }
}