using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectEBusiness.DAL;
using ProjectEBusiness.DTOs.CardDTOs;
using ProjectEBusiness.Extensions;
using ProjectEBusiness.Models;
using System.Reflection.Metadata;

namespace ProjectEBusiness.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CardController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CardPostDto cardPostDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Can't be null");
                return View();
            }

            Card card = _mapper.Map<Card>(cardPostDto);
            card.Image = cardPostDto.File.CreateFile(_env.WebRootPath, "assets/img");
            _context.Add(card);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int Id)
        {
            Card card = _context.Cards.Find(Id);
            CardUpdateDto cardUpdateDto = new CardUpdateDto()
            {
                cardGetDto = _mapper.Map<CardGetDto>(card)
            };
            return View(cardUpdateDto);
        }
        [HttpPost]
        public IActionResult Update(CardUpdateDto cardUpdateDto)
        {
            Card card = _context.Cards.Find(cardUpdateDto.cardGetDto.Id);
            card.Name = cardUpdateDto.cardPostDto.Name;
            card.Position = cardUpdateDto.cardPostDto.Position;
            if (cardUpdateDto.cardPostDto.File is not null)
            {
                card.Image = cardUpdateDto.cardPostDto.File.CreateFile(_env.WebRootPath, "assets/img");
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            Card card=_context.Cards.Find(Id);
            _context.Remove(card);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
