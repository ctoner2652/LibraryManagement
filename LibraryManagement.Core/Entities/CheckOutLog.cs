﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class CheckOutLog
    {
        public int CheckOutLogID { get; set; }
        public int BorrowerID { get; set; }
        public int MediaID {  get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Borrower Borrower { get; set; } 
        public Media Media { get; set; }
    }
}
