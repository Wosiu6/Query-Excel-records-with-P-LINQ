using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using CustomExtensions;

namespace SearchEngine
{
    class StorageManager
    {
        private static readonly StorageManager instance = new StorageManager();

        string StoreCodesFile = "C:\\Users\\wospa\\OneDrive - Staffordshire University\\YEAR 2\\TBSE\\Assignment\\SearchEngine\\bin\\Debug\\Data\\StoreCodes.csv";
        string OrdersFolder = "C:\\Users\\wospa\\OneDrive - Staffordshire University\\YEAR 2\\TBSE\\Assignment\\SearchEngine\\bin\\Debug\\Data\\Orders";

        string[] OrdersFileNames;
        string[] storeCodesData;

        static StorageManager()
        {
        }

        private StorageManager()
        {

        }

        public static StorageManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void UpdateStores(bool UserInteraction)
        {
            Storage.Instance.Stores.Clear();
            var exceptions = new ConcurrentQueue<Exception>();

            try
            {
                //Reading data for stores using LinQ
                IEnumerable<Store> queryStores =
                from storesLine in storeCodesData
                let splitStores = storesLine.Split(',')
                select new Store()
                {
                    StoreCode = splitStores[0],
                    StoreLocation = splitStores[1]
                };

                //Assigning to list for faster access
                Parallel.ForEach(queryStores, (store, state, index) =>
                {
                    try
                    {
                        Storage.Instance.Stores.GetOrAdd(store.StoreCode, store);
                    }
                    catch (Exception e)
                    {
                        state.Stop();
                        exceptions.Enqueue(e);
                    }
                });
                if (UserInteraction && exceptions.Count == 0)
                    MessageBox.Show("Stores Updated");
            }
            catch(Exception e)
            {
                MessageBox.Show("Failed to update Stores, try checking Store Codes File Path\n" + e.Message);
            }
            //STORES ADDED (0.04s)
            
        }
        public void UpdateDates(bool UserInteraction)
        {
            var exceptions = new ConcurrentQueue<Exception>();
            //START READING DATES/ORDERS
            try
            {
                IEnumerable<Date> queryDates =
                    from fileName in OrdersFileNames
                    let splitDate = Path.GetFileNameWithoutExtension(fileName).Split('_')
                    select new Date()
                    {
                        Week = Convert.ToInt32(splitDate[1]),
                        Year = Convert.ToInt32(splitDate[2])
                    };
                Storage.Instance.SetDates(new ConcurrentBag<Date>(queryDates));
            }
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }

            if (exceptions.Count > 0)
            {
                MessageBox.Show("Failed to update dates, check orders path in Path Options");
            }
            else
            {
                if (UserInteraction)
                    MessageBox.Show("Dates Updated");
            }

            //FINSHED ADDING ORDERS
        }
        public void UpdateOrders(bool UserInteraction)
        {
            var exceptions = new ConcurrentQueue<Exception>();

            ConcurrentBag<Order> orders = new ConcurrentBag<Order>();

            Parallel.ForEach(OrdersFileNames, (filePath, state, index) =>
            {
                try
                {
                    string fileNameExt = Path.GetFileName(filePath);
                    string fileName = Path.GetFileNameWithoutExtension(filePath);

                    string[] fileNameSplit = fileName.Split('_');
                    Store store = Storage.Instance.Stores[fileNameSplit[0]];
                    Date date = new Date { Week = Convert.ToInt32(fileNameSplit[1]), Year = Convert.ToInt32(fileNameSplit[2]) };

                    string[] orderData = File.ReadAllLines(OrdersFolder + @"\" + fileNameExt);

                    IEnumerable<Order> queryOrders =
                            from ordersLine in orderData
                            let splitOrders = ordersLine.Split(',')
                            select new Order()
                            {
                                Store = store,
                                Date = date,
                                SupplierName = splitOrders[0],
                                SupplierType = splitOrders[1],
                                Cost = Convert.ToDouble(splitOrders[2])
                            };


                    //Assigning to list for faster access
                    orders.AddRange<Order>(queryOrders);
                }
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                    state.Stop();
                }

            });

            if (exceptions.Count > 0)
            {
                MessageBox.Show("Failed to update orders, check orders path in Path Options");
            }
            else
            {
                Storage.Instance.SetOrders(orders);
                if (UserInteraction)
                    MessageBox.Show("Orders Updated");
            }

            //FINSHED ADDING ORDERS
        }

        public void UpdateData()
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            IniPaths();

            UpdateStores(false);
            UpdateDates(false);
            UpdateOrders(false);

            stopWatch.Stop();
            MessageBox.Show("Data updated in: " + stopWatch.Elapsed.TotalSeconds + "s");
        }

        private void IniPaths()
        {
            OrdersFileNames = Directory.GetFiles(OrdersFolder);
            storeCodesData = File.ReadAllLines(StoreCodesFile);
        }

        internal void ChangeStoreCodesFile(string fileName)
        {
            StoreCodesFile = fileName;
            storeCodesData = File.ReadAllLines(StoreCodesFile);
        }

        internal void ChangeOrdersFolder(string selectedPath)
        {
            OrdersFolder = selectedPath;
            OrdersFileNames = Directory.GetFiles(OrdersFolder);
        }
    }

}

/*
 ConcurrentDictionary<string, Store> stores = new ConcurrentDictionary<string, Store>();
            ConcurrentBag<Date> dates;
            ConcurrentBag<Order> orders = new ConcurrentBag<Order>();
            ConcurrentStack<IEnumerable<Order>> ordersInLists = new ConcurrentStack<IEnumerable<Order>>();

            //START READING STORES
            StoreCodesFilePath = Directory.GetCurrentDirectory() + @"\" + OrdersFolder + @"\" + StoreCodesFile;
            string[] storeCodesData = File.ReadAllLines(StoreCodesFilePath);

            //Reading data for stores using LinQ
            IEnumerable<Store> queryStores =
            from storesLine in storeCodesData
            let splitStores = storesLine.Split(',')
            select new Store()
            {
                StoreCode = splitStores[0],
                StoreLocation = splitStores[1],
            };
            //Assigning to list for faster access
            Parallel.ForEach(queryStores, (store, state, index) =>
            {
                stores.GetOrAdd(store.StoreCode, store);
            });
            //STORES ADDED (0.04s)

            //START READING DATES/ORDERS
            OrdersFileNames = Directory.GetFiles(OrdersFolder + @"\" + StoreDataFolder);

            IEnumerable<Date> queryDates =
                from fileName in OrdersFileNames
                let splitDate = Path.GetFileNameWithoutExtension(fileName).Split('_')
                select new Date()
                {
                    Week = Convert.ToInt32(splitDate[1]),
                    Year = Convert.ToInt32(splitDate[2])
                };

            dates = new ConcurrentBag<Date>(queryDates);

            Parallel.ForEach(OrdersFileNames, (filePath, state, index) =>
            {
                string fileNameExt = Path.GetFileName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                string[] fileNameSplit = fileName.Split('_');
                Store store = stores[fileNameSplit[0]];
                Date date = new Date { Week = Convert.ToInt32(fileNameSplit[1]), Year = Convert.ToInt32(fileNameSplit[2]) };

                string[] orderData = File.ReadAllLines(OrdersFolder + @"\" + StoreDataFolder + @"\" + fileNameExt);

                IEnumerable<Order> queryOrders =
                    from ordersLine in orderData
                    let splitOrders = ordersLine.Split(',')
                    select new Order()
                    {
                        Store = store,
                        Date = date,
                        SupplierName = splitOrders[0],
                        SupplierType = splitOrders[1],
                        Cost = Convert.ToDouble(splitOrders[2])
                    };

                //Assigning to list for faster access
                orders.AddRange<Order>(queryOrders);
            });
            //FINSHED ADDING ORDERS

            Storage.Instance.SetDates(dates);
            Storage.Instance.SetOrders(orders);
            Storage.Instance.SetStores(stores);
     */
