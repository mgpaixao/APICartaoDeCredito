using System;
using System.Collections.Generic;

namespace APICartaoDeCredito.Models
{
    public class LeadRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Attributes
        {
            public string type { get; set; }
            public string url { get; set; }
        }

        public class New
        {
            public Attributes attributes { get; set; }
            public DateTime LastModifiedDate { get; set; }
            public string Email { get; set; }
            public bool DoNotCall { get; set; }
            public bool HasOptedOutOfFax { get; set; }
            public string Website { get; set; }
            public string Salutation { get; set; }
            public string OwnerId { get; set; }
            public string CreatedById { get; set; }
            public string Phone { get; set; }
            public string LastModifiedByID { get; set; }
            public string Status { get; set; }
            public bool IsDeleted { get; set; }
            public string FirstName { get; set; }
            public bool IsConverted { get; set; }
            public bool IsUnreadByOwner { get; set; }
            public bool HasOptedOutOfEmail { get; set; }
            public string Gender__c { get; set; }
            public string City { get; set; }
            public DateTime SystemModstamp { get; set; }
            public string Trail__c { get; set; }
            public string CreatedByID { get; set; }
            public string State { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Plus_40__c { get; set; }
            public string Id { get; set; }
            public string First_Conversion_Date__c { get; set; }
            public string LastName { get; set; }
            public string LastTransferDate { get; set; }
            public string LastModifiedById { get; set; }
            public string Reason_Message__c { get; set; }
            public string Disabilities__c { get; set; }
        }

        public class Old
        {
            public Attributes attributes { get; set; }
            public DateTime LastModifiedDate { get; set; }
            public string Email { get; set; }
            public bool DoNotCall { get; set; }
            public bool HasOptedOutOfFax { get; set; }
            public string Website { get; set; }
            public string Salutation { get; set; }
            public string OwnerId { get; set; }
            public string CreatedById { get; set; }
            public string Phone { get; set; }
            public string LastModifiedByID { get; set; }
            public string Status { get; set; }
            public bool IsDeleted { get; set; }
            public string FirstName { get; set; }
            public bool IsConverted { get; set; }
            public bool IsUnreadByOwner { get; set; }
            public bool HasOptedOutOfEmail { get; set; }
            public string Gender__c { get; set; }
            public string City { get; set; }
            public DateTime SystemModstamp { get; set; }
            public string Trail__c { get; set; }
            public string CreatedByID { get; set; }
            public string State { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Plus_40__c { get; set; }
            public string Id { get; set; }
            public string First_Conversion_Date__c { get; set; }
            public string LastName { get; set; }
            public string LastTransferDate { get; set; }
            public string LastModifiedById { get; set; }
            public string Reason_Message__c { get; set; }
            public string Disabilities__c { get; set; }
        }

        public class Root
        {
            public List<New> @new { get; set; }
            public List<Old> old { get; set; }
            public string userId { get; set; }
        }


    }
}
