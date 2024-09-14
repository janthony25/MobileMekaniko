﻿using System.ComponentModel;

namespace MobileMekaniko.Models.Dto
{
    public class AddCustomerDto
    {
        [DisplayName("Customer Name")]
        public required string CustomerName { get; set; }

        [DisplayName("Email Address")]
        public string? CustomerEmail { get; set; }

        [DisplayName("Contact #")]
        public string? CustomerNumber { get; set; }
    }
}
