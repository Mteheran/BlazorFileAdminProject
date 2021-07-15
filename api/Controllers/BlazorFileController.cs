using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Context;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using shared;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlazorFileController : ControllerBase
    {
        BlazorFileContext context;
        IAzureStorageService azureStorageService;
        public BlazorFileController(BlazorFileContext fileContext, IAzureStorageService azureStorageService)
        {
            context = fileContext;
            this.azureStorageService = azureStorageService;
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
        public async Task<ActionResult<BlazorFile>> PostBlazorFile(BlazorFileModel model)
        {
            FileData fileData = new FileData();
            fileData.FileInfo = model.FileData;
            fileData.FileName = model.FileInfo.FileName;
            await azureStorageService.SaveFileAsync(fileData);

            context.BlazorFiles.Add(model.FileInfo);
            await context.SaveChangesAsync();

            return model.FileInfo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlazorFile(string id, BlazorFileModel model)
        {
           FileData fileData = new FileData();
           fileData.FileInfo = model.FileData;
           fileData.FileName = model.FileInfo.FileName;
           await azureStorageService.SaveFileAsync(fileData);
           context.Update(model.FileInfo);
           await context.SaveChangesAsync();

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