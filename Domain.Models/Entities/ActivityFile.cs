using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class ActivityFile
    {
        public int ActivityFileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public Activity Activity { get; set; }
        public int ActivityId { get; set; }
    }
}
