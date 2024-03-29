﻿using ProductStoreAsp.Models.Entities;

namespace ProductStoreAsp.Models
{
    public class Order : BaseEntity
    {
        public List<Product> Products { get; set; } = null!;

        public AppUser User { get; set; } = null!;

        public bool IsAccepted { get; set; } = false;

        public string UserId { get; set; } = null!;
    }
}
