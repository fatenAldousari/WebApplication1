﻿using System.Reflection.Metadata;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CivilId { get; set; }
        public string Position { get; set; }
        public BankBranch BankBranch { get; set; }
    }
}
