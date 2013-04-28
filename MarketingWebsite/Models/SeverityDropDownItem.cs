﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingWebsite.Models
{
    public class SeverityDropDownItem
    {
        public int SeverityDropDownItemId { get; set; }

        public string Value { get; set; }

        public virtual Database Database {get; set; } 
    }
}