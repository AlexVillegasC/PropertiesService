using System.Collections.Generic;

namespace PropertyService.Dtos
{
   public class AddressDto
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public object district { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public object zipPlus4 { get; set; }
    }

    public class FinancialDto
    { 
        public double listPrice { get; set; }
        
        public double monthlyRent { get; set; }        
    }

    public class PhysicalDto
    {        
        public int yearBuilt { get; set; }     
    }


    public class PropertyDto
    {
        public int id { get; set; }        
        public AddressDto address { get; set; }
        public FinancialDto financial { get; set; }
        public PhysicalDto physical { get; set; }        
    }
}
