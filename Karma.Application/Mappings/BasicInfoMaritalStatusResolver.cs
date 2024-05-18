using AutoMapper;
using Karma.Application.DTOs;
using Karma.Application.Helpers;
using Karma.Core.Entities;

namespace Karma.Application.Mappings
{
    public class BasicInfoMaritalStatusResolver : IValueResolver<User, BasicInfoDTO, string>
    {
        public string Resolve(User source, BasicInfoDTO destination, string destMember, ResolutionContext context)
        {
            return source.MaritalStatus.GetDescription();
        }
    }
    
    public class MilitaryServiceStatusResolver : IValueResolver<User, BasicInfoDTO, string>
    {
        public string Resolve(User source, BasicInfoDTO destination, string destMember, ResolutionContext context)
        {
            return source.MilitaryServiceStatus.GetDescription();
        }
    }

    public class GenderResolver : IValueResolver<User, BasicInfoDTO, string>
    {
        public string Resolve(User source, BasicInfoDTO destination, string destMember, ResolutionContext context)
        {
            return source.Gender.GetDescription();
        }
    }
}
