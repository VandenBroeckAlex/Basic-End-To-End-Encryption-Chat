using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stamp_back.Interfaces;
using stamp_back.Models;
using System;

namespace stamp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatController(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Chat>))]
        public IActionResult GetChats()
        {
            var chats = _chatRepository.GetChats();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(chats);
        }

        [HttpGet("id/{chatId:guid}")]
        [ProducesResponseType(200,Type = typeof(Chat))]
        [ProducesResponseType(400)]
        public IActionResult GetChatById(Guid chatId)
        {
            var chat = _chatRepository.GetChatById(chatId);

            if (!ModelState.IsValid)
            {
                return NotFound(); ;
            }
            return Ok(chat);
        }

        [HttpGet("Userid/{userId:guid}")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(400)]
        public IActionResult GetChatByUserId(Guid userId)
        {
            var chat = _chatRepository.GetAllChatsByUserId(userId);

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(400)]
        public IActionResult GetChatByName(string name) 
        { 
        
            var chat = _chatRepository.GetChatByName(name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(chat);
        }
        //GetChatByName
    }
}
