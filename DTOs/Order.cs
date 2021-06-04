using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class Order
    {
        public Store Store { get; set; }

        public Date Date { get; set; }
        public string SupplierName { get; set; }
        public string SupplierType { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return String.Format("{0, -20:s} {1, 20:s} {2, 20:s} {3, 10:s} {4, 12:C2}",
                                    Store.ToString().Trim(), SupplierName.Trim(), SupplierType.Trim(), Date.ToString().Trim(), Cost.ToString().Trim());
        }
    }
}
