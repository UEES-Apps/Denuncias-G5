using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;

namespace G5.Denuncias.BE.Domain.Models
{
    public class CustomException : Exception
    {

        public TipoErrorEnum _tipoError;

        public object _obj;

        public List<string?> _stringFormat; 

        public List<Error> _errores = new List<Error>();

        public CustomException(string Base) : base(Base)
        {
        }

        public CustomException(TipoErrorEnum tipoError, string Base) : base(Base)
        {
            _tipoError = tipoError;
        }

        public CustomException(TipoErrorEnum tipoError, string Base, List<string?> strings) :base(Base) 
        {
            _tipoError = tipoError;
            _stringFormat = strings;
            _errores = strings.Select((s,i) => new Error {  Code = (i+1).ToString("0900"), Message = s }).ToList();
        }

        public CustomException(TipoErrorEnum tipoError, string Base, List<Error> errores) : base(Base)
        {
            _tipoError = tipoError;
            _errores = errores;
        }
    }
}
