using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FluentValidation;

namespace Application.Features.DoctorProfiles.DTOs.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, DateTime> YearMonthDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder.Must(dateTime => dateTime.ToString("yyyy-MM-dd") == dateTime.ToString("yyyy-MM-dd"))
                .WithMessage("{PropertyName} must be in the format 'YYYY-MM-DD'");
        }

        public static IRuleBuilderOptions<T, Photo> IsValidPhotoExtension<T>(this IRuleBuilder<T, Photo> ruleBuilder)
        {
            return ruleBuilder.Must(photo => IsValidPhotoExtension(photo.Url))
                .WithMessage("{PropertyName} must have a valid file extension");
        }

        private static bool IsValidPhotoExtension(string photoFileName)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(photoFileName)?.ToLower();

            return validExtensions.Contains(fileExtension);
        }
    }


}