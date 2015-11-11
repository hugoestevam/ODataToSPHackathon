using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataToSPHackathon.Models
{
    public class EnterprisesModel
    {
        
        public Int64 ROWNUMBER { get; set; }

        [Key]
        public Int32 EnterpriseID { get; set; }
        public String EnterpriseName { get; set; }
        public String CorporateName { get; set; }
        public String PartnerName { get; set; }
        public Int32? PartnerID { get; set; }
        public Int64 Goal { get; set; }
        public Int32 CountersDay { get; set; }
        public Int32 CountersMonth { get; set; }
        public Int32 CountersMonthColor { get; set; }
        public Int32 CountersMonthMono { get; set; }
        public Int32 BillingMonth { get; set; }
        public Int32 EngagedPrinters { get; set; }
        public Int32 MonitoredPrinters { get; set; }
        public Boolean Monitor { get; set; }
        public Int32? SponsorAccountID { get; set; }
        public String SponsorFullName { get; set; }
        public String SponsorPhone { get; set; }
        public String SponsorEmail { get; set; }
        public Int32? VendorAccountID { get; set; }
        public String VendorFullName { get; set; }
        public String VendorPhone { get; set; }
        public String VendorEmail { get; set; }
        public String Resale { get; set; }
        public String Purchase { get; set; }
        public Boolean HasBilling { get; set; }
        public Boolean HasCounters { get; set; }

    }

}
