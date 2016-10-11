using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Sample.Domain.Dto.Mapping
{
    public sealed class RoleConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(RoleDto);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var concreteValue = (T_Role)value;
            var result = new RoleDto
            {
                Id=concreteValue.Id,
                Name=concreteValue.Name,
                Remark=concreteValue.Remark,
                IsDisabled=concreteValue.IsDisabled,
                Sort=concreteValue.Sort,
                IsDisabledName  = concreteValue.IsDisabled?"否":"是",
            };
            return result;
        }
    }
}
