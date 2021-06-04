using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class Store
    {
        public string StoreCode { get; set; }
        public string StoreLocation { get; set; }

        public override string ToString()
        {
            return String.Format("{0, -20}",
                                        (StoreLocation.Trim() +"; " +  StoreCode.Trim()));
        }
    }
}
