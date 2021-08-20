using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperOnNorthwind.Web.Models
{
    public class Customer
    {
        //id, company, last_name, first_name, email_address, job_title, business_phone, home_phone, mobile_phone, fax_number, address, city, state_province, zip_postal_code, country_region, web_page, notes, attachments
        public int Id { get; set; }

        public string Company { get; set; }

        public string Last_Name { get; set; }


        public string First_Name { get; set; }

        public string Email_Address { get; set; }
    }
}
