﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irving.Web.Models
{
    public class Recurrence : DbModel
    {
        public RecurrenceType Type { get; set; }
        public int Amount { get; set; }
    }
}