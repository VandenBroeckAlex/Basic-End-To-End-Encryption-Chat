using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stamp_back.Interfaces;
using stamp_back.Models;
using System;
using stamp_back.Dto;
using stamp_back.Repository;
using Microsoft.AspNetCore.Authorization;

namespace stamp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IMapper _mapper;
    

        public ChatController(IChatRepository chatRepository, IMapper mapper, IUserRepository userRepository, IUserChatRepository userChatRepository)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userChatRepository = userChatRepository;
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
        [Authorize]
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateChat([FromBody] ChatDto.ChatCreateDto chatCreate)
        {
            if (chatCreate == null)
            {

                ModelState.AddModelError("", "Chat data is required.");
                return StatusCode(422, ModelState);
            }
            //Check if valid users
            bool usersExist = _userRepository.UserExist(chatCreate.User1) && _userRepository.UserExist(chatCreate.User2);
            if (!usersExist)
            {
                ModelState.AddModelError("", "One or both users do not exist.");
                return StatusCode(422, ModelState);
            }
                

            // Check if users have already a chat
            var existingChatId = _chatRepository.ChatExistsByUsers(chatCreate.User1, chatCreate.User2);
            if (existingChatId != null)
                return Ok(new { ChatId = existingChatId, Message = "Chat already exists." });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //create chat
            var chatMap = _mapper.Map<Chat>(chatCreate);
            var chatCreated = _chatRepository.CreateChat(chatMap);
            if (!chatCreated)
            {
                ModelState.AddModelError("", "Something went wrong while creating the chat.");
                return StatusCode(500, ModelState);
            }

            //TODO: create user chat relation

            var userChats = new List<UserChat>
            {
                new UserChat { ChatId = chatMap.Id, UserId = chatCreate.User1 },
                new UserChat { ChatId = chatMap.Id, UserId = chatCreate.User2 }
            };

            foreach(var userChat in userChats)
            {
                var _userChat = _userChatRepository.CreateUserChat(userChat);
            }

            return Ok($"chatId : {chatMap.Id}");
        }
    }
}
