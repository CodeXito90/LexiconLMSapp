﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{   
    public class ActivityTypeDto
    {
        public int ActivityTypeId { get; set; }
        public string Type { get; set; }
    }
}
