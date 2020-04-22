﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BankBranch
    {
        public int AccountNumber { get; set; }//מספר חשבון בנק
        public string BankName { get; set; }
        public int BankNumber { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return string.Format(" Bank Branch:" +
                "bank-{0},Branch Number-{1}", BankName, BranchNumber);
        }
    }
}
