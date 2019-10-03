﻿using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ProftaakApplicatieDiabetes.ViewModels;
using Logic.Interface;
using Models;


namespace ProftaakApplicatieDiabetes.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageLogic _messageLogic;
        

        public MessageController(IMessageLogic messageLogic)
        {
            _messageLogic = messageLogic;
        }

        public IActionResult ViewMessage()
        {
            MessageViewModel messageViewModel = new MessageViewModel();
            messageViewModel.Messages = _messageLogic.GetMessages();
            return View(messageViewModel);
        }

        [HttpPost]
        public IActionResult ViewMessage(MessageViewModel messageViewModel)
        {
            MessageModel message = new MessageModel(messageViewModel.Title, messageViewModel.Content);
            _messageLogic.SendMessage(message);

            return ViewMessage();
        }
    }
}