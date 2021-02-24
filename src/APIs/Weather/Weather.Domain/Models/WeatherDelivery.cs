using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Domain.Models
{
    public class WeatherDelivery
    {
        public Guid Id { get; set; }

        public string Location { get; set; }
        

        public string EmailAddress { get; set; }
    }
}