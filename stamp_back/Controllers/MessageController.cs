using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stamp_back.Dto;
using stamp_back.Interfaces;
using stamp_back.Models;

namespace stamp_back.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller 
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageController(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Message>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllMessages() 
        { 
            var messages = _messageRepository.GetAllMessages();

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            if (messages == null) 
            {
                return NoContent();
            }
            return Ok(messages);
        }

        [HttpGet("{chatID:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Message>))]
        [ProducesResponseType(400)]
        public IActionResult GetMessageByChat(Guid chatId)
        {
            var messages = _messageRepository.GetAllChatMessages(chatId);
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(messages);
        }

        //Check if user in chat before sending or reciving message
    }
}
