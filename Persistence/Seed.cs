using Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(HakimHubDbContext context
            )
        {
            if (!context.InstitutioProfiles.Any())
            {

                string json = File.ReadAllText("HakimHubExtractedSeed.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var institutions = JsonSerializer.Deserialize<InstitutionProfile[]>(json, options);
                var institutionsToCreate = new List<InstitutionProfile>();

                // Seed the data
                foreach (var institution in institutions)
                {
                    var institutionProfile = new InstitutionProfile
                    {
                        Id = Guid.NewGuid(),
                        InstitutionName = institution.InstitutionName,
                        BranchName = "",
                        Website = institution.Website,
                        PhoneNumber = institution.PhoneNumber,
                        Summary = institution.Summary,
                        EstablishedOn = institution.EstablishedOn,
                        Rate = institution.Rate,
                        Address = new Address
                        {
                            Id = Guid.NewGuid(),
                            Country = institution.Address.Country,
                            Region = institution.Address.Region,
                            Zone = institution.Address.Zone,
                            Woreda = institution.Address.Woreda,
                            SubCity = institution.Address.SubCity,
                            Longitude = institution.Address.Longitude,
                            Latitude = institution.Address.Latitude,
                            Summary = institution.Address.Summary,
                            City = institution.Address.City,
                        },

                    };

                    var logo = await context.Photos.FindAsync(institution.Logo.Id);
                    if (logo == null)
                    {
                        logo = new Photo
                        {
                            Id = institution.Logo.Id,
                            Url = institution.Logo.Url,
                            LogoId = institutionProfile.Id
                        };
                        context.Photos.Add(logo);
                    }
                    institutionProfile.LogoId = logo.Id;
                    institutionProfile.Logo = logo;

                    // Create and assign the Banner (if it doesn't exist)
                    var banner = await context.Photos.FindAsync(institution.Banner.Id);
                    if (banner == null)
                    {
                        banner = new Photo
                        {
                            Id = institution.Banner.Id,
                            Url = institution.Banner.Url,
                            BannerId = institutionProfile.Id

                        };
                        context.Photos.Add(banner);
                    }
                    institutionProfile.BannerId = banner.Id;
                    institutionProfile.Banner = banner;


                    var photos = new List<Photo>();
                    foreach (var photoData in institution.Photos)
                    {
                        var existingPhoto = await context.Photos.FindAsync(photoData.Id);
                        if (existingPhoto == null)
                        {
                            var photo = new Photo
                            {
                                Id = photoData.Id,
                                Url = photoData.Url,
                                DoctorProfileId = institutionProfile.Id
                            };
                            context.Photos.Add(photo);
                            photos.Add(photo);
                        }
                        else
                        {
                            photos.Add(existingPhoto);
                        }
                    }

                    // Save changes to the context before setting the InstitutionProfile reference on the Photo entities
                    await context.SaveChangesAsync();

                    // Set the InstitutionProfile reference on the Photo entities
                    foreach (var photo in photos)
                    {
                        photo.InstitutionProfileId = institutionProfile.Id;
                    }

                    institutionProfile.Photos = photos;

                    var services = new List<Services>();
                    foreach (var serviceData in institution.Services)
                    {
                        // Check if the service with the same name already exists
                        var existingService = context.Services.FirstOrDefault(s => s.ServiceName == serviceData.ServiceName);

                        if (existingService != null)
                        {
                            // Service already exists, skip creating a new one
                            services.Add(existingService);
                            continue;
                        }

                        var service = new Services
                        {
                            Id = Guid.NewGuid(),
                            ServiceName = serviceData.ServiceName,
                            ServiceDescription = serviceData.ServiceDescription,
                        };

                        // Add the doctor to the institution
                        service.Institutions.Add(institutionProfile);

                        services.Add(service);
                    }

                    institutionProfile.Services = services;


                    var institutionAvailabilities = new List<InstitutionAvailability>();
                    foreach (var availableData in institution.InstitutionAvailabilities)
                    {
                        var available = new InstitutionAvailability
                        {
                            Id = Guid.NewGuid(),
                            StartDay = availableData.StartDay,
                            EndDay = availableData.EndDay,
                            Opening = availableData.Opening,
                            Closing = availableData.Closing,
                            TwentyFourHours = availableData.TwentyFourHours,
                            // Add the doctor to the institution
                            Institution = institutionProfile
                        };

                        institutionAvailabilities.Add(available);
                    }
                    institutionProfile.InstitutionAvailabilities = institutionAvailabilities;

                    var doctors = new List<DoctorProfile>();
                    foreach (var doctorData in institution.Doctors)
                    {
                        var existingDoctor = await context.DoctorProfiles.FirstOrDefaultAsync(d => d.FullName == doctorData.FullName);

                        if (existingDoctor != null)
                        {

                            // Assign the existing doctor to the institution
                            existingDoctor.Institutions.Add(institutionProfile);
                            existingDoctor.MainInstitution = institutionProfile;

                            doctors.Add(existingDoctor);
                        }
                        else
                        {
                            var newDoctor = new DoctorProfile
                            {
                                Id = Guid.NewGuid(),
                                FullName = doctorData.FullName,
                                About = doctorData.About,
                                Gender = doctorData.Gender,
                                Email = doctorData.Email,
                                CareerStartTime = doctorData.CareerStartTime,
                                Photo = doctorData.Photo,
                                MainInstitution = institutionProfile
                            };

                            // Assign the new doctor to the institution
                            newDoctor.Institutions.Add(institutionProfile);

                            doctors.Add(newDoctor);
                        }
                    }

                    institutionProfile.Doctors = doctors;

                    context.InstitutioProfiles.Add(institutionProfile);
                    await context.SaveChangesAsync();
                    context.ChangeTracker.Clear();
                }
            }
        }
    }
}