using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEBusiness.DAL;
using ProjectEBusiness.DTOs.CardDTOs;
using ProjectEBusiness.Models;
using System.Diagnostics;

namespace ProjectEBusiness.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Card> Cards = _context.Cards.ToList();
            List<CardGetDto> cardGetDto = _mapper.Map<List<CardGetDto>>(Cards);
            return View(cardGetDto);
        }
    }
}