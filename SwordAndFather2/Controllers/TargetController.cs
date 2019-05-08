using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SwordAndFather2.Data;
using SwordAndFather2.Models;

namespace SwordAndFather2.Controllers
{
    // BEST PRACTICES: CONTROLLER = WHAT IT NEEDS, NOT HOW TO BUILDSTUFF. WE WANT TO TELL ASP.NET HOW TO BUILD STUFF (STARTUP FILE)
    // THEN IN THE CONTROLLER, WE CAN TELLIT WHAT WE NEED


    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private TargetRepository _repo;

        public TargetController(TargetRepository repo) //constructor
        {
            _repo = repo;
        }

        [HttpPost]
        public ActionResult AddTarget(CreateTargetRequest createRequest)
        {
            var repository = new TargetRepository();

            var newTarget = repository.AddTarget(
                createRequest.Name,
                createRequest.Location,
                createRequest.FitnessLevel,
                createRequest.UserId);

            return Created($"/api/target/{newTarget.Id}", newTarget);
        }

        [HttpGet]
        public ActionResult GetAllTargets()
        {
            var repository = new TargetRepository();
            var targets = repository.GetAll();
            return Ok(targets);
        }
    }
}