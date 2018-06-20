﻿using System;

namespace DAL.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; } 
    }
}