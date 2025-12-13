using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Enums.Denuncias
{
    public enum TipoDenuncia
    {
        [EnumMember(Value = "Aseo")]
        [Description("Aseo y Ornato")]
        AseoYOrnato = 0,
        [EnumMember(Value = "Transito")]
        [Description("Transito Vial")]
        TransitoVial = 1,
        [EnumMember(Value = "Delito")]
        [Description("Delito")]
        Delito = 2
    }
}
