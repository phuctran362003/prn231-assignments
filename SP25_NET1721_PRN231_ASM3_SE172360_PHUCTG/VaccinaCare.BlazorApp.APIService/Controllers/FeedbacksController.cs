﻿using Microsoft.AspNetCore.Mvc;
using VaccinaCare.Application.Interface;

namespace VaccinaCare.BlazorApp.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching feedbacks. Please try again later.");
            }
        }

        // GET: api/Feedbacks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackDetail(Guid id)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackDetails(id);
                if (feedback == null)
                {
                    return NotFound("Feedback not found.");
                }
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the feedback details. Please try again later.");
            }
        }
    }
}
