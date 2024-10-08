﻿using System.ComponentModel.DataAnnotations;

namespace Razorpaycore8.Models
{
    public class PaymentRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Amount { get; set; }

        public string? UniqueID { get; set; }
    }
}
