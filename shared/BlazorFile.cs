using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared
{
    public class BlazorFile
    {
        public Guid BlazorFileId { get;set;} = Guid.NewGuid();
        public string Name {get;set;}
        public string Description {get;set;}
        public string FileName {get;set;}
        public DateTime DateCreated {get;set;}
        public DateTime DateModified {get;set;}
        public bool Active {get;set;}
        public Guid BlazorFileTypeId {get;set;}
        public virtual BlazorFileType BlazorFileType {get;set;}
        
    }
}