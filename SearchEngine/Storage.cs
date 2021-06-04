using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace SearchEngine
{
    //SINGLETON
    public sealed class Storage
    {
        private static readonly Storage instance = new Storage();

        public ConcurrentDictionary<string, Store> Stores { private set; get; }
        public ConcurrentBag<Date> Dates { private set; get; }
        public ConcurrentBag<Order> Orders { private set; get; }



        static Storage()
        {
        }

        private Storage()
        {
            Stores = new ConcurrentDictionary<string, Store>();
            Dates = new ConcurrentBag<Date>();
            Orders = new ConcurrentBag<Order>();
        }

        public static Storage Instance
        {
            get
            {
                return instance;
            }
        }
        public void SetStores(ConcurrentDictionary<string, Store> list)
        {
            this.Stores = list;
        }
        public void SetDates(ConcurrentBag<Date> list)
        {
            this.Dates = list;
        }
        public void SetOrders(ConcurrentBag<Order> list)
        {
            this.Orders = list;
        }
    }
}
