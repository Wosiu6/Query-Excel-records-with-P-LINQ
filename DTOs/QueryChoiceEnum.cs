using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public enum TestEverythingEnum
    {
        STORE = 0,
        DATE = 1,
        ORDER = 2
    }
    public enum QueryChoiceEnum
    {
        COST_BY_ALL_ORDERS = 10,
        COST_BY_STORE = 11,
        COST_BY_WEEK = 12,
        COST_BY_WEEK_AND_STORE = 13,
        COST_BY_SUPPLIER = 14,
        COST_BY_SUPPLIER_TYPE = 15,
        COST_BY_WEEK_AND_SUPPLIER_TYPE = 16,
        COST_BY_SUPPLIER_TYPE_AND_STORE = 17,
        COST_BY_WEEK_AND_SUPPLIER_TYPE_AND_STORE = 18
    }
}
