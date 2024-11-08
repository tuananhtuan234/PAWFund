using Azure;
using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Repository.Repository;
using Services.Helper;
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
        private readonly IPetRepository petRepository;
        private readonly IAdoptionRepository adoptionRepository;
        private readonly IEmailService emailService;

        public ShelterService(IShelterRepository shelterRepository, IUserRepository userRepository, IPetRepository petRepository, IAdoptionRepository adoptionRepository, IEmailService emailService)
        {
            this.shelterRepository = shelterRepository;
            this.userRepository = userRepository;
            this.petRepository = petRepository;
            this.adoptionRepository = adoptionRepository;
            this.emailService = emailService;
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
                var pets = await petRepository.GetAllPetByShelter(shelterId);
                if (pets.Count > 0)
                {
                    return ServiceResponse<string>.ErrorResponse("Shelter has pets, cannot delete");
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


        public async Task<List<UserAdoptionResponse>> GetAllPetByShelterStatus(string shelterId)
        {
            List<UserAdoptionResponse> listUser = new List<UserAdoptionResponse>();
            List<Pet> pets = await petRepository.GetAllPetByShelterStatus(shelterId);
            if (pets.Count > 0)
            {
                foreach (Pet pet in pets)
                {
                    Adoption adoption = await adoptionRepository.GetAdoptionIncludeUser(pet.AdoptionId);
                    var userAdoptionResponse = new UserAdoptionResponse()
                    {
                        AdoptionId = pet.AdoptionId,
                        AdoptionDate = adoption.AdoptionDate.ToString("dd/MM/yyyy"),
                        PetId = pet.AdoptionId,
                        PetName = pet.Name,
                        Ages = pet.Ages,
                        Breed = pet.Breed,
                        Description = pet.Description,
                        Gender = pet.Gender,
                        Species = pet.Species,
                        Status = pet.Status.ToString(),
                    };
                    listUser.Add(userAdoptionResponse);
                }
            }
            return listUser;
        }

        public async Task<string> ResponseAdoption(string adoptionId, string shelterId, string response, string? reason)
        {
            var listPet = await petRepository.GetPetByAdoptionIdAndShelterId(adoptionId, shelterId);
            int rs = 0;
            if (listPet == null)
            {
                return "No pets are available for adoption";
            }
            else
            {
                if (listPet.ShelterStatus == ShelterStatus.Approved)
                {
                    return "Pet has been adopted";
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<table style='border-collapse: collapse; width: 100%;'>");
                sb.AppendLine("<tr style='background-color: #f2f2f2;'>");
                sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>AdoptionId</th>");
                sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Pet</th>");
                sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Shelter</th>");
                sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Response</th>");
                sb.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Reason</th>");
                sb.AppendLine("</tr>");

                Pet pet = await petRepository.GetPetById(listPet.PetId);
                if (pet == null)
                {
                    return "Pet is not exist";
                }
                else
                {
                    if (response.ToLower() == "accept" && string.IsNullOrEmpty(reason))
                    {
                        pet.ShelterStatus = ShelterStatus.Approved;
                        listPet.Adoption.AdoptionStatus = AdoptionStatus.Accepted;
                    }
                    if (response.ToLower() == "reject" && !string.IsNullOrEmpty(reason))
                    {
                        pet.AdoptionId = null;
                        pet.ShelterStatus = null;
                        listPet.Adoption.AdoptionStatus = AdoptionStatus.Rejected;
                    }
                    rs = await adoptionRepository.UpdateAdoption(listPet.Adoption);
                    bool result = await petRepository.UpdatePet(pet);
                    if (!result)
                    {
                        return "Update pet failed";
                    }
                    else
                    {
                        var shelter = await shelterRepository.GetShelters(pet.ShelterId);
                        sb.AppendLine("<tr>");
                        sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{adoptionId}</td>");
                        sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.Name}</td>");
                        sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{shelter.First().ShelterName}</td>");
                        sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{response}</td>");
                        sb.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{reason}</td>");
                        sb.AppendLine("</tr>");
                    }
                }
                sb.AppendLine("</table>");
                var user = await adoptionRepository.GetUserByAdoptionId(adoptionId);
                var emailBody = $@"
                    <p>Dear {user.User.FullName},</p>
                    <p>We are pleased to send you the response from the center regarding the pet you have adopted.:</p>
                    {sb.ToString().Trim()}
                    <p>Have a good day</p>
                    <p>Best regards,<br/>PAWFund</p>
                    ";

                await emailService.SendEmailAsync(user.User.Email, "Confirm your adoption", emailBody, isHtml: true);
                return rs > 0 ? "Confirmed successfully" : "Confirmed failed";
            }
        }


        public async Task<ServiceResponse<List<ShelterResponse>>> GetAllShelters()
		{
			List<ShelterResponse> shelterResponses = shelterRepository.GetAllShelters().Result.Select(s => new ShelterResponse
            {
				ShelterId = s.ShelterId,
				Address = s.Address,
				Description = s.Description,
				Email = s.Email,
				PhoneNumber = s.PhoneNumber,
				ShelterDate = s.ShelterDate.ToString("dd/MM/yyyy"),
				ShelterName = s.ShelterName,
				UserName = s.User.FullName,
			}).ToList();
            return ServiceResponse<List<ShelterResponse>>.SuccessResponseWithMessage(shelterResponses);
		}
		public async Task<Shelter> GetShelterById(string shelterId)
	    {
	        return await shelterRepository.GetShelterById(shelterId);
	    }
			public async Task<ServiceResponse<PagingResult<ShelterResponse>>> GetSheltersPaging(int currentPage, int pageSize, string search)
			{
				// Get all shelters from the repository
				var allShelters = await shelterRepository.GetAllShelters();

				// Apply the search filter if provided (case-insensitive search for ShelterName)
				if (!string.IsNullOrWhiteSpace(search))
				{
					allShelters = allShelters
						.Where(s => s.ShelterName.Contains(search, StringComparison.OrdinalIgnoreCase))
						.ToList();
				}

				// Calculate the total number of pages
				int totalShelters = allShelters.Count;
				int totalPages = (int)Math.Ceiling((double)totalShelters / pageSize);

				// Ensure the currentPage is within valid bounds
				currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

				// Get the subset of shelters for the current page
				var paginatedShelters = allShelters
					.Skip((currentPage - 1) * pageSize)  // Skip the number of items for previous pages
					.Take(pageSize)                      // Take the number of items for this page
					.Select(s => new ShelterResponse
					{
						ShelterId = s.ShelterId,
						Address = s.Address,
						Description = s.Description,
						Email = s.Email,
						PhoneNumber = s.PhoneNumber,
						ShelterDate = s.ShelterDate.ToString("dd/MM/yyyy"),
						ShelterName = s.ShelterName,
						UserName = s.User.FullName,
					})
					.ToList();

				// Prepare the paging result
				var pagingResult = new PagingResult<ShelterResponse>(
		            totalShelters,    // totalItems
		            totalPages,       // totalPages
		            currentPage,      // currentPage
		            pageSize,         // pageSize
		            search,           // search
		            paginatedShelters // Items
	            );

				// Return the result wrapped in a ServiceResponse
				return new ServiceResponse<PagingResult<ShelterResponse>>
				{
					Data = pagingResult,
					Success = true,
					SuccessMessage = "Shelters retrieved successfully."
				};
			}
        public async Task<List<PetResponse>> GetPetsByUserId(string userId)
        {
            try
            {
                var response = new List<PetResponse>();
                var petResponse = new PetResponse();
                var pets = await petRepository.GetPetsByUserId(userId);

                foreach (var pet in pets)
                {
                    petResponse = new PetResponse()
                    {
                        PetId = pet.PetId,
                        Name = pet.Name,
                        Gender = pet.Gender,
                        Ages = pet.Ages,
                        Description = pet.Description,
                        Species = pet.Species,
                        Breed = pet.Breed
                    };
                    response.Add(petResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
