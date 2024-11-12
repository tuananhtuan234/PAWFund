using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;
        public AdoptionService(IAdoptionRepository adoptionRepository, IPetRepository petRepository, IShelterRepository shelterRepository, IEmailService emailService, IMapper mapper, IUserRepository userRepository, IImageRepository imageRepository) 
        {
            _adoptionRepository = adoptionRepository;
            _petRepository = petRepository;
            _shelterRepository = shelterRepository;
            _emailService = emailService;
            _mapper = mapper;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }



        public async Task<string> AddAdoption(AdoptionRequest adoptionRequest)
        {
            var petDetailsBuilder = new StringBuilder();
            string petId = adoptionRequest.ListPet.FirstOrDefault()?.PetId;
            Adoption adoption = new Adoption()
            {
                AdoptionDate = DateTime.Now,
                AdoptionId = Guid.NewGuid().ToString(),
                AdoptionStatus = AdoptionStatus.Pending,
                UserId = adoptionRequest.UserId,
                PetId = petId,
                Reason = null,
            };
            int result = await _adoptionRepository.AddAdoption(adoption);
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

            return result == 0 ? "Add adoption failed" : "Add adoption success";
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

        public async Task<string> FollowAdoption(string adoptionId)
        {
            var adoptions = await _adoptionRepository.GetAdoption(adoptionId);
            if (adoptions.Count == 0)
            {
                return "Adoption is not exist";
            }
            Adoption adoption = adoptions.First();
            if(adoption.AdoptionStatus == AdoptionStatus.Rejected)
            {
                return "Shelter rejected";
            }
            if(adoption.AdoptionStatus == AdoptionStatus.Accepted)
            {
                return "Shelter accepted";
            }
            return null;
        }

        public async Task<List<AdoptionUserResponse>> GetAdoptionByUserId(string userId)
        {
            var checkUser = await _userRepository.GetUserById(userId);
            if (checkUser == null)
            {
                throw new Exception("User does not exist");
            }
            List<AdoptionUserResponse> result = new List<AdoptionUserResponse>();
            List<Adoption> adoptions = await _adoptionRepository.GetAdoptionByUserId(userId);
            if(adoptions.Count > 0)
            {
                foreach (var item in adoptions)
                {
                    Pet pet = await _petRepository.GetPetById(item.PetId);
                    if (pet != null)
                    {
                        Shelter shelter = await _shelterRepository.GetShelterById(pet.ShelterId);
                        string urlImage = null;
                        Image image = await _imageRepository.GetImageByPetId(pet.PetId);
                        if (image != null) 
                        {
                            urlImage = image.UrlImage;
                        }
                        AdoptionUserResponse user = new AdoptionUserResponse()
                        {
                            AdoptionDate = item.AdoptionDate.ToString("dd/MM/yyyy"),
                            AdoptionId = item.AdoptionId,
                            AdoptionStatus = item.AdoptionStatus.ToString(),
                            Ages = pet.Ages,
                            Breed = pet.Breed,
                            CreateDate = pet.CreateDate.ToString("dd/MM/yyyy"),
                            Description = pet.Description,
                            Gender = pet.Gender,
                            Image = urlImage,
                            Name = pet.Name,
                            PetId = pet.PetId,
                            Reason = pet.Reason,
                            ShelterName = shelter.ShelterName,
                            ShelterStatus = pet.ShelterStatus.ToString(),
                            Species = pet.Species,
                            Status = pet.Status.ToString(),
                            UpdateDate = pet.UpdateDate.ToString()
                        };
                        result.Add(user);
                    }
                    else
                    {
                        throw new Exception("Pet does not exist");
                    }
                }
            }
            
            return result;
        }

        public Task<List<AdoptionResponse>> GetAllAdoption(string? adoptionId)
        {
            throw new NotImplementedException();
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
    }
}
