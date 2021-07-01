using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shared
{
    public class BlazorFileType
    {
        public Guid BlazorFileTypeId { get;set;} = Guid.NewGuid();
        public string Name {get;set;}
        public string Description {get;set;}
        
        [JsonIgnore]
        public virtual ICollection<BlazorFile> BlazorFiles {get;set;}
    }
}