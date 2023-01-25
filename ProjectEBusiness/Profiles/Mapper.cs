using AutoMapper;
using ProjectEBusiness.DTOs.AccountDTOs;
using ProjectEBusiness.DTOs.CardDTOs;
using ProjectEBusiness.Models;

namespace ProjectEBusiness.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<CardPostDto, Card>();
            CreateMap<Card, CardGetDto>();
            CreateMap<CardPostDto, Card>();
            CreateMap<Card, CardGetDto>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
