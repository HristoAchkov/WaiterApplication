﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Infrastructure.Data.Models
{
    public class OrderDish
    {
        [Comment("Ordered Dish Identifier")]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        public int DishId { get; set; }
        [ForeignKey(nameof(DishId))]
        public Dish Dish { get; set; } = null!;
        public int Quantity { get; set; }
        public string Comment { get; set; }
    }
}
