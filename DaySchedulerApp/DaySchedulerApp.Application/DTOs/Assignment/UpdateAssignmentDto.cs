﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.DTOs.Assignment
{
    public class UpdateAssignmentDto
    {
        public string Description { get; set; }
        public int FrequencyInDays { get; set; }
    }
}
