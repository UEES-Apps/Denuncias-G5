using FluentValidation;
using G5.Denuncias.BE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;

namespace G5.Denuncias.BE.Infraestructure.Extentions
{
    public static class FluentValidationExtensions
    {
        public static void ValidateAndThrowCustomException<T>(this IValidator<T> validator, T instance)
        {
            var res = validator.Validate(instance);

            if (!res.IsValid)
            {
                var ex = new ValidationException(res.Errors);
                var customEx = new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, ex.Errors.ElementAt(0).ErrorMessage);
                customEx._errores.AddRange(ex.Errors.Select(e => new Domain.Models.Error()
                {
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage,
                }));
                throw customEx;
            }
        }
    }
}
