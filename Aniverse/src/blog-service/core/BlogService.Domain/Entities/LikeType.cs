using BlogService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Domain.Entities
{
    public class LikeType : BaseEntity
    {
        public string Name { get; set; }
        public string IconSrc { get; set; }
    }
}
