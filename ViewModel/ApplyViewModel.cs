using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ApplyViewModel
    {
        public bool Error { get; set; }
        public string Msg { get; set; }
        public List<string> FilesName { get; set; }
    }
}