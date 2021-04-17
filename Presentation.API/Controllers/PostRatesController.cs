﻿using System;
using System.Threading.Tasks;
using Application.CQRS.PostRates.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostRatesController : MyBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreatePostRateAsync(CreatePostRateCommand request)
        {
            Guid createdPostRateId = await Mediator.Send(request);
            return Ok(createdPostRateId);
        }
    }
}