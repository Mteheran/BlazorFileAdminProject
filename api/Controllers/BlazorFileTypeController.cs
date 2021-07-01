using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Context;
using Microsoft.AspNetCore.Mvc;
using shared;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlazorFileTypeController : ControllerBase
    {
        BlazorFileContext context;
        public BlazorFileTypeController(BlazorFileContext fileContext)
        {
            context = fileContext;
            context.Database.EnsureCreated();
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<BlazorFileType>> GetBlazorFileTypes()
        {
            return context.BlazorFileTypes;
        }

        [HttpGet("{id}")]
        public ActionResult<BlazorFileType> GetBlazorFileTypeById(string id)
        {
            return context.BlazorFileTypes.SingleOrDefault(p=> p.BlazorFileTypeId == Guid.Parse(id));
        }

        [HttpPost("")]
        public ActionResult<BlazorFileType> PostBlazorFileType(BlazorFileType model)
        {
            context.BlazorFileTypes.Add(model);
            context.SaveChanges();

            return model;
        }

        [HttpPut("{id}")]
        public IActionResult PutBlazorFileType(int id, BlazorFileType model)
        {
           context.Update(model);
           context.SaveChanges();

           return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<BlazorFileType> DeleteBlazorFileTypeById(string id)
        {
            var currentBlazorFileType = context.BlazorFileTypes.SingleOrDefault(p=> p.BlazorFileTypeId == Guid.Parse(id));
            context.Remove(currentBlazorFileType);
            context.SaveChanges();
            return Ok();
        }
    }
}