using FluentValidation;
using SignalR.DtoLayer.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.ValidationRules.BookingValidations
{
    public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon alanı boş geçilemez!");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş geçilemez!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih alanı boş geçilemez!");
            RuleFor(x => x.PersonCount).NotEmpty().WithMessage("Kişi Sayısı alanı boş geçilemez!");

            RuleFor(x => x.Name).MinimumLength(5).WithMessage("İsim alanı minimum 5 karakter olmalıdır!").MaximumLength(50).WithMessage("İsim alanı maksimum 50 karakter olmalıdır!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıklama alanı maksimum 500 karakter olmalıdır!");
            RuleFor(x => x.Mail).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz!");
        }
    }
}
