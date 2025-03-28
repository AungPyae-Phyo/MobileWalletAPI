using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDTO;

public class UserRegistrationDto
{
    public string Name { get; set; }

    public string? PhoneNumber { get; set; } = default!;

    public string? Email { get; set; } = default!;

    public string? Role { get; set; } = Enum.GetName(typeof(Role), Domain.Enums.Role.None);

    public string? HashPassword { get; set; } = default!;
}
public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<UserRegistrationDto, User>()
           // .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
           .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.HashPassword));
    }
}
