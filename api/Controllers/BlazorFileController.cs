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
    public class BlazorFileController : ControllerBase
    {
        BlazorFileContext context;
        public BlazorFileController(BlazorFileContext fileContext)
        {
            context = fileContext;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<BlazorFile>> GetBlazorFiles()
        {
            return context.BlazorFiles;
        }

        [HttpGet("{id}")]
        public ActionResult<BlazorFile> GetBlazorFileById(string id)
        {
            return context.BlazorFiles.SingleOrDefault(p=> p.BlazorFileId == Guid.Parse(id));
        }

        [HttpPost("")]
        public ActionResult<BlazorFile> PostBlazorFile(BlazorFile model)
        {
            context.BlazorFiles.Add(model);
            context.SaveChanges();

            return model;
        }

        [HttpPut("{id}")]
        public IActionResult PutBlazorFile(int id, BlazorFile model)
        {
           context.Update(model);
           context.SaveChanges();

           return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<BlazorFile> DeleteBlazorFileById(string id)
        {
            var currentBlazorFile = context.BlazorFiles.SingleOrDefault(p=> p.BlazorFileId == Guid.Parse(id));
            context.Remove(currentBlazorFile);
            context.SaveChanges();
            return Ok();
        }
    }
}