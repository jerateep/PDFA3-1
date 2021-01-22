using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFA3
{
    [XmlRoot("facebookwifi")]
    public class FBWiFi_XMLBody
    {
        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("first_name")]
        public string first_name {get;set; }
        [XmlElement("last_name")]
        public string last_name {get;set; }
        [XmlElement("personal_id")]
        public string personal_id {get;set; }
        [XmlElement("mobile_no")]
        public string mobile_no {get;set; }
        [XmlElement("email")]
        public string email {get;set; }
        [XmlElement("service_point")]
        public string service_point {get;set; }
        [XmlElement("install_point")]
        public string install_point {get;set; }
        [XmlElement("register_type")]
        public string register_type {get;set; }
        [XmlElement("company_name")]
        public string company_name {get;set; }
        [XmlElement("contact_title")]
        public string contact_title {get;set; }
        [XmlElement("contact_firstname")]
        public string contact_firstname {get;set; }
        [XmlElement("contact_lastname")]
        public string contact_lastname {get;set; }
        [XmlElement("contact_position")]
        public string contact_position {get;set; }
        [XmlElement("contact_personalid")]
        public string contact_personalid {get;set; }
        [XmlElement("birthday")]
        public string birthday {get;set; }
        [XmlElement("modified_date")]
        public string modified_date {get;set; }
    }
}
