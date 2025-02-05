﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Media
    {
        public int MediaID { get; set; }
        public int MediaTypeID { get; set; }
        public string Title { get; set; }
        public bool IsArchived { get; set; }

        public List<CheckOutLog> Logs { get; set; }
        public MediaType MediaType { get; set; }
    }
}
