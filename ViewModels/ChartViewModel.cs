﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ChartViewModel
    {
        public bool Active { get; set; }

        public int Year { get; set; }

        public List<int> Days { get; set; }
    }
}