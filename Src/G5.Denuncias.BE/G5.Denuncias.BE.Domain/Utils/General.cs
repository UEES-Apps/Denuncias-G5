using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Utils
{
    public static class General
    {
        public static string GetEnumMemberValue<T>(this T enumValue) where T : Enum
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((EnumMemberAttribute)attributes[0]).Value;
                }
            }

            // Return default value if no EnumMemberAttribute is found
            return enumValue.ToString();
        }

        public static bool Compare<T>(this T enumValue, string code) where T : Enum
        {
            var value = enumValue.GetEnumMemberValue();
            var valueCode = code ?? "";
            if (int.TryParse(valueCode, out _))
                valueCode = Convert.ToInt32(valueCode.Trim()).ToString();
            // Return default value if no EnumMemberAttribute is found
            return valueCode.Equals(value);


        }
    }
}
