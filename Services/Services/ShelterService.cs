using Repository.Data.Entity;
using Repository.Interface;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository shelterRepository;
        private readonly IUserRepository userRepository;

        public ShelterService(IShelterRepository shelterRepository, IUserRepository userRepository)
        {
            this.shelterRepository = shelterRepository;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<ShelterRequest>> AddShelter(ShelterRequest shelterRequest)
        {
            try
            {
                var shelter = new Shelter()
                {
                    ShelterId = Guid.NewGuid().ToString(),
                    Address = shelterRequest.Address,
                    Email = shelterRequest.Email,
                    Description = shelterRequest.Description,
                    PhoneNumber = shelterRequest.PhoneNumber,
                    ShelterDate = DateTime.Now,
                    UserId = shelterRequest.UserId,
                    ShelterName = shelterRequest.ShelterName,
                    IsDeleted = false,
                };
                var result = await shelterRepository.AddShelter(shelter);
                if (result != 0)
                {
                    return ServiceResponse<ShelterRequest>.SuccessResponseWithMessage(shelterRequest);
                }
                else
                {
                    return ServiceResponse<ShelterRequest>.ErrorResponse("Add shelter failed");
                }
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DeleteShelter(string shelterId)
        {
            try
            {
                var checkShelter = await shelterRepository.GetShelters(shelterId);
                if (!checkShelter.Any())
                {
                    return ServiceResponse<string>.ErrorResponse("shelterId is not existed");
                }
                checkShelter.First().IsDeleted = true;
                int result = await shelterRepository.UpdateShelter(checkShelter.First());
                if (result != 0)
                {
                    return ServiceResponse<string>.SuccessResponseOnlyMessage();
                }
                else
                {
                    return ServiceResponse<string>.ErrorResponse("Delete shelter failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<ShelterResponse>>> GetShelters(string? shelterId)
        {
            try
            {
                var shelters = await shelterRepository.GetShelters(shelterId);
                List<ShelterResponse> shelterResponses = new List<ShelterResponse>();
                foreach (var shelter in shelters)
                {
                    var shelterResponse = new ShelterResponse()
                    {
                        ShelterId = shelter.ShelterId,
                        Address = shelter.Address,
                        Description = shelter.Description,
                        Email = shelter.Email,
                        PhoneNumber = shelter.PhoneNumber,
                        ShelterDate = shelter.ShelterDate.ToString("dd/MM/yyyy"),
                        ShelterName = shelter.ShelterName,
                        UserName = shelter.User.FullName,
                    };
                    shelterResponses.Add(shelterResponse);
                }
                return ServiceResponse<List<ShelterResponse>>.SuccessResponseWithMessage(shelterResponses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ServiceResponse<string>> UpdateShelter(UpdateShelterRequest shelterRequest)
        {
            try
            {
                DateTime shelterDate = DateTime.Now;
                if (!string.IsNullOrEmpty(shelterRequest.ShelterDate))
                {
                    string[] dateFormats = { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy" };
                    
                    bool isValidDate = DateTime.TryParseExact(shelterRequest.ShelterDate, dateFormats,
                                                              System.Globalization.CultureInfo.InvariantCulture,
                                                              System.Globalization.DateTimeStyles.None,
                                                              out shelterDate);

                    if (!isValidDate)
                    {
                        return ServiceResponse<string>.ErrorResponse("Date format is incorrect. Please enter the date in the format dd/MM/yyyy, d/MM/yyyy, dd/M/yyyy, d/M/yyyy.");
                    }
                }
                
                var checkShelter = await shelterRepository.GetShelters(shelterRequest.ShelterId);
                if (!checkShelter.Any())
                {
                    return ServiceResponse<string>.ErrorResponse("shelterId is not existed");
                }
                var shelter = checkShelter.First();

                // Gán giá trị cho các thuộc tính của shelter
                shelter.Address = shelterRequest.Address ?? shelter.Address;
                shelter.ShelterName = shelterRequest.ShelterName ?? shelter.ShelterName;
                shelter.Description = shelterRequest.Description ?? shelter.Description;
                shelter.Email = shelterRequest.Email ?? shelter.Email;
                shelter.PhoneNumber = shelterRequest.PhoneNumber ?? shelter.PhoneNumber;
                shelter.ShelterDate = !string.IsNullOrEmpty(shelterRequest.ShelterDate) ? shelterDate : shelter.ShelterDate;

                if (!string.IsNullOrEmpty(shelterRequest.UserId))
                {
                    var checkUser = await userRepository.GetUser(shelterRequest.UserId, null, null, null);
                    if (!checkUser.Any())
                    {
                        return ServiceResponse<string>.ErrorResponse("UserId does not exist");
                    }

                    // Nếu UserId tồn tại, gán lại UserId cho shelter
                    shelter.UserId = shelterRequest.UserId;
                }
                int result = await shelterRepository.UpdateShelter(checkShelter.First());
                if (result != 0)
                {
                    return ServiceResponse<string>.SuccessResponseOnlyMessage();
                }
                else
                {
                    return ServiceResponse<string>.ErrorResponse("Update shelter failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
