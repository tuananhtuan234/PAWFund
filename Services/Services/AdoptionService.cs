using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Services.Helper;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AdoptionService : IAdoptionService
    {
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IPetRepository _petRepository;
        private readonly IShelterRepository _shelterRepository;
        private readonly IEmailService _emailService;
        public AdoptionService(IAdoptionRepository adoptionRepository, IPetRepository petRepository, IShelterRepository shelterRepository, IEmailService emailService) 
        {
            _adoptionRepository = adoptionRepository;
            _petRepository = petRepository;
            _shelterRepository = shelterRepository;
            _emailService = emailService;
        }

        public async Task<string> AddAdoption(AdoptionRequest adoptionRequest)
        {
            var petDetailsBuilder = new StringBuilder();
            Adoption adoption = new Adoption()
            {
                AdoptionDate = DateTime.Now,
                AdoptionId = Guid.NewGuid().ToString(),
                AdoptionStatus = AdoptionStatus.Pending,
                UserId = adoptionRequest.UserId,
                Reason = null,
            };

            var distinctShelterId = adoptionRequest.ListPet.Select(s => s.ShelterId).Distinct();

            foreach (var shelterDistince in distinctShelterId)
            {
                var shelter = await _shelterRepository.GetShelters(shelterDistince);
                if (shelter != null)
                {
                    List<PetsRequest> listPet = adoptionRequest.ListPet.Where(s => s.ShelterId == shelter.First().ShelterId).ToList();

                    if (listPet.Count > 0) 
                    {
                        // Tạo bảng HTML thay cho text-based table với căn giữa
                        petDetailsBuilder.AppendLine("<table style='border-collapse: collapse; width: 100%;'>");
                        petDetailsBuilder.AppendLine("<tr style='background-color: #f2f2f2;'>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Id</th>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Name</th>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Age</th>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Gender</th>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Species</th>");
                        petDetailsBuilder.AppendLine("<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Breed</th>");
                        petDetailsBuilder.AppendLine("</tr>");

                        foreach (var item in listPet)
                        {
                            Pet pet = await _petRepository.GetPetById(item.PetId);
                            if (pet != null) 
                            {
                                if (pet.ShelterStatus == ShelterStatus.Waiting || pet.ShelterStatus == ShelterStatus.Approved)
                                {
                                    return pet.PetId + " " + pet.Name + " are in the process of being adopted or have been adopted";
                                }
                                else
                                {
                                    pet.AdoptionId = adoption.AdoptionId;
                                    pet.ShelterStatus = ShelterStatus.Waiting;
                                    pet.Reason = null;

                                    bool check = await _petRepository.UpdatePet(pet);
                                    if (!check)
                                    {
                                        return "Update pet failed";
                                    }

                                    petDetailsBuilder.AppendLine("<tr>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.PetId}</td>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.Name}</td>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.Ages}</td>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{(pet.Gender ? "Male" : "Female")}</td>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.Species}</td>");
                                    petDetailsBuilder.AppendLine($"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; vertical-align: middle;'>{pet.Breed}</td>");
                                    petDetailsBuilder.AppendLine("</tr>");
                                }
                            }
                            else
                            {
                                return "Pet is not exist";
                            }
                        }

                        petDetailsBuilder.AppendLine("</table>");
                    }
                    else
                    {
                        return "You haven't adopted any pets yet";
                    }
                }
                else
                {
                    return "Shelter is not exist";
                }

                var emailBody = $@"
        <p>Dear {shelter.First().ShelterName},</p>
        <p>We are happy to inform you that the following pets have been adopted:</p>
        {petDetailsBuilder.ToString().Trim()}
        <p>Please confirm information about the adoption process so we can complete the procedure and send notification to the customer.</p>
        <p>Best regards,<br/>PAWFund</p>
        ";

                await _emailService.SendEmailAsync(shelter.First().Email, "Confirm your adoption", emailBody, isHtml: true);
                petDetailsBuilder = new StringBuilder();
            }

            int result = await _adoptionRepository.AddAdoption(adoption);
            return result == 0 ? "Add adoption failed" : "Add adoption success. Please access this code " + adoption.AdoptionId + " to track your adoption process";
        }


        public async Task<string> DeleteAdoption(string adoptionId)
        {
            var checkAdoption = await _adoptionRepository.GetAdoption(adoptionId);
            if (checkAdoption.Count == 0)
            {
                return "Adoption is not existed";
            }
            if (checkAdoption.Count == 1)
            {
                if (checkAdoption.First().AdoptionStatus == AdoptionStatus.Pending || checkAdoption.First().AdoptionStatus == AdoptionStatus.Accepted)
                {
                    return "The adoption cannot be removed because it is in process or has been responded to";
                }
            }
            int result = await _adoptionRepository.DeleteAdoption(checkAdoption.First());
            if (result == 0)
            {
                return "Delete adoption failed";
            }
            else
            {
                return "Delete adoption success";
            }
        }

        public async Task<string> FollowAdoption(string adoptionId, string? response, string? reason)
        {
            var adoptions = await _adoptionRepository.GetAdoption(adoptionId);
            if (adoptions.Count == 0)
            {
                return "Adoption is not exist";
            }
            else
            {
                Adoption adoption = adoptions.First();
                if (string.IsNullOrEmpty(response))
                {
                    return adoption.AdoptionStatus.ToString();
                }
                else
                {
                    if (response.ToLower() == "confirm")
                    {
                        adoption.AdoptionStatus = AdoptionStatus.Confirm;
                    }
                    if(response.ToLower() == "cancel")
                    {
                        if(adoption.AdoptionStatus == AdoptionStatus.Pending)
                        {
                            if (string.IsNullOrEmpty(reason)) 
                            {
                                return "Please enter reason you want to cancel this adoption";
                            }
                            else
                            {
                                adoption.AdoptionStatus = AdoptionStatus.Cancel;
                                adoption.Reason = reason;
                            }
                        }
                        else if(adoption.AdoptionStatus == AdoptionStatus.Cancel)
                        {
                            return "This adoption canceled";
                        }
                        else
                        {
                            return "Can not cancel adoption because this adoption ongoing";
                        }
                    }
                }
                int rs = await _adoptionRepository.UpdateAdoption(adoption);
                if(response.ToLower() == "confirm")
                {
                    return rs > 0 ? "Confirm success" : "Confirm failed";
                }
                return rs > 0 ? "Cancel success" : "Confirm failed";
            }
        }

        public async Task<List<AdoptionResponse>> GetAllAdoption(string? adoptionId)
        {
            List<AdoptionResponse> result = new List<AdoptionResponse>();
            var adoptions = await _adoptionRepository.GetAdoption(adoptionId);
            foreach (var adoption in adoptions) 
            {
                AdoptionResponse adoptionResponse = new AdoptionResponse()
                {
                    AdoptionId = adoption.AdoptionId,
                    AdoptionDate = adoption.AdoptionDate.ToString("dd/MM/yyyy"),
                    AdoptionStatus = adoption.AdoptionStatus.ToString(),
                    Reason = adoption.Reason,
                    UserId = adoption.UserId,
                };
                result.Add(adoptionResponse);
            }
            return result;
        }

        public async Task<string> UpdateAdoption(UpdateAdoptionRequest adoptionRequest, string id)
        {
            var checkAdoption = await _adoptionRepository.GetAdoption(id);
            if (checkAdoption.Count == 0)
            {
                return "Adoption is not existed";
            }
            Adoption adoption = checkAdoption.First();
            adoption.AdoptionStatus = (AdoptionStatus)adoptionRequest.AdoptionStatus;
            adoption.Reason = adoptionRequest.Reason;   
            int result = await _adoptionRepository.UpdateAdoption(adoption);
            if (result == 0)
            {
                return "Update adoption failed";
            }
            else
            {
                return "Update adoption success";
            }
        }

        public async Task<string> UpdateStatusAdoption(string adoptionId, int count)
        {
            var adoptions = await _adoptionRepository.GetAdoption(adoptionId);
            if (adoptions == null) 
            {
                return "Adoption is not exist";
            }
            else
            {
                Adoption adoption = adoptions.First();
                if (adoption.AdoptionStatus == AdoptionStatus.Pending)
                {
                    return "Shelter need to accept adoption";
                }
                if (adoption.AdoptionStatus == AdoptionStatus.Rejected)
                {
                    return "Shelter rejected can not update status";
                }
                if (adoption.AdoptionStatus == AdoptionStatus.Accepted)
                {
                    if(count == 0)
                    {
                        return "Adoption accepted";
                    }
                    if (count == 3)
                    {
                        adoption.AdoptionStatus = AdoptionStatus.Preparing;
                        int result = await _adoptionRepository.UpdateAdoption(adoption);
                        if (result == 0)
                        {
                            return "Update status failed";
                        }
                    }
                    return "Prepare for delivery";
                }
                if (adoption.AdoptionStatus == AdoptionStatus.Preparing)
                {
                    if (count == 4)
                    {
                        adoption.AdoptionStatus = AdoptionStatus.Delivered;
                        int result = await _adoptionRepository.UpdateAdoption(adoption);
                        if (result == 0)
                        {
                            return "Update status failed";
                        }
                        else
                        {
                            return "Delivered to the driver";
                        }
                    }
                    return "Prepare for delivery";
                }
                if (adoption.AdoptionStatus == AdoptionStatus.Delivered)
                {
                    if (count == 5)
                    {
                        adoption.AdoptionStatus = AdoptionStatus.Finish;
                        int result = await _adoptionRepository.UpdateAdoption(adoption);
                        if (result == 0)
                        {
                            return "Update status failed";
                        }
                        else
                        {
                            return "Adoption completed";
                        }
                    }
                    return "Delivered to the driver";
                }
                if (adoption.AdoptionStatus == AdoptionStatus.Finish)
                {
                    return "Adoption completed";
                }
                if(count == -1)
                {
                    return "Cannot update status";
                }
            }
            return null;
        }
    }
}
