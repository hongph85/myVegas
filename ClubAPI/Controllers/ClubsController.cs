using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClubAPI.DataAccess;
using ClubAPI.Models;
using ClubAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        IClubService service;
        public ClubsController(IClubService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            var model = service.GetClubByName(name);
            if (model == null)
            {
                model = service.AddClub(name);
            }
            else
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict);
            }

            return CreatedAtAction(nameof(Get), new { clubId = model.ClubId }, model);
        }

        [HttpGet]
        public IActionResult Get(Guid clubId)
        {
            var club = service.GetClubById(clubId);
            if (club == null)
            {
                return NotFound();
            }

            var members = service.GetMembersByClub(clubId);
            return Ok(new { Id = club.ClubId, Members = members ?? new List<int>() });
        }

        public class PlayerModel
        {
            public int PlayerId { get; set; }
        }

        [HttpPost("{clubId}/members")]
        public IActionResult AddMember([FromRoute] Guid clubId, [FromBody] PlayerModel model)
        {
            var club = service.GetClubById(clubId);
            if (club == null)
            {
                return NotFound(clubId);
            }

            service.AddMember(clubId, model.PlayerId);
            return Ok(new { PlayerId = model.PlayerId });
        }
    }
}
