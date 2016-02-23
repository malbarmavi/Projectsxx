using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string About { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhoneNumber { get; set; }
        public int OwnerId { get; set; }

    }
}