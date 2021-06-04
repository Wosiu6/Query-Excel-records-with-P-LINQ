using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DTOs;

namespace SearchEngine
{
    public partial class MainWindow : Form
    {
        #region
        Thread UpdateDataThread = new Thread(StorageManager.Instance.UpdateData);

        Task CalcTask = null;
        Task SetChoicesTask = null;
        Task ChartTask = null;

        int Choice;
        int MainChoice;

        readonly string Line100 = MakeLine(100);
        readonly string Line45 = MakeLine(45);
        readonly string Line55 = MakeLine(55);
        #endregion 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SearchChoice.Items.AddRange(Enum.GetNames(typeof(TestEverythingEnum)));

            MainQueryChoice.Items.AddRange(Enum.GetNames(typeof(QueryChoiceEnum)));

        }
        public void DisplayMessage(String message)
        {
            Invoke(new MethodInvoker(delegate
            {
                ResultTextBox.Text = message;
            }));
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UpdateDataThread.IsAlive)
            {
                UpdateDataThread.Abort();
                UpdateDataThread = new Thread(StorageManager.Instance.UpdateData);
                UpdateDataThread.Start();
            }
            else
                MessageBox.Show("Updatinig hasnt finished yet, please wait");
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Choice = (int)Enum.Parse(typeof(TestEverythingEnum), SearchChoice.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("You must select search cryteria");
                return;
            }
            switch (Choice)
            {
                case (int)TestEverythingEnum.DATE:
                    {
                        ResultTextBox.Items.Clear();
                        ResultTextBox.BeginUpdate();

                        ResultTextBox.Items.Add(String.Format("{0,-20} | {1,20} |",
                            "YEAR", "WEEK"));
                        ResultTextBox.Items.Add(Line45);


                        Parallel.ForEach(Storage.Instance.Dates, (record, state, index) =>
                        {
                            Invoke(new MethodInvoker(delegate
                            {
                                ResultTextBox.Items.Add(String.Format("{0,-20} | {1,20} |",
                                    record.Year, record.Week));
                            }));
                            if (index == 1000)
                            {
                                state.Stop();
                                return;
                            }
                        });


                        ResultTextBox.EndUpdate();
                        break;
                    }
                case (int)TestEverythingEnum.ORDER:
                    {
                        ResultTextBox.Items.Clear();
                        ResultTextBox.BeginUpdate();

                        ResultTextBox.Items.Add(String.Format("{0, -35} | {1, 14} | {2, 20} | {3, 8} | {4, 8} |",
                            "Store", "Supplier", "Type", "Date", "Cost"));
                        ResultTextBox.Items.Add(Line100);

                        Parallel.ForEach<Order>(Storage.Instance.Orders, (record, state, index) =>
                        {
                            Invoke(new MethodInvoker(delegate
                            {
                                ResultTextBox.Items.Add(String.Format("{0, -35} | {1, 14} | {2, 20} | {3, 8} | {4, 8} |",
                                    record.Store.ToString(), record.SupplierName, record.SupplierType, record.Date.ToString(), record.Cost.ToString()));
                            }));
                            if (index == 1000)
                            {
                                state.Stop();
                                return;
                            }
                        });


                        ResultTextBox.EndUpdate();
                        break;
                    }
                case (int)TestEverythingEnum.STORE:
                    {
                        ResultTextBox.Items.Clear();
                        ResultTextBox.BeginUpdate();

                        ResultTextBox.Items.Add(String.Format("{0, -20} | {1, 30} |",
                            "STORE CODE", "STORE LOCATION"));
                        ResultTextBox.Items.Add(Line55);
                        {

                            Parallel.ForEach(Storage.Instance.Stores, (record, state, index) =>
                            {
                                Invoke(new MethodInvoker(delegate
                                {
                                    ResultTextBox.Items.Add(String.Format("{0, -20} | {1, 30} |",
                                        record.Value.StoreCode, record.Value.StoreLocation));
                                }));
                                if (index == 1000)
                                {
                                    state.Stop();
                                    return;
                                }
                            });


                            ResultTextBox.EndUpdate();
                            break;
                        }
                    }
            }
        }
        private void MainQueryChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MainChoice = (int)Enum.Parse(typeof(QueryChoiceEnum), MainQueryChoice.SelectedItem.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            switch (MainChoice)
            {
                case (int)QueryChoiceEnum.COST_BY_ALL_ORDERS:
                    {
                        SetUpChoices(false, false, false, "", "", "");

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_STORE:
                    {
                        SetUpChoices(true, false, false, "Store", "", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var stores = Storage.Instance.Stores.Values.Select(x => x.StoreLocation);

                            Invoke(new MethodInvoker(delegate { FirstChoice.Items.AddRange(stores.ToArray()); }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_SUPPLIER:
                    {
                        SetUpChoices(true, false, false, "Supplier", "", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var suppliers = Storage.Instance.Orders.Select(x => x.SupplierName).Distinct();


                            Invoke(new MethodInvoker(delegate { FirstChoice.Items.AddRange(suppliers.ToArray()); }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_SUPPLIER_TYPE:
                    {
                        SetUpChoices(true, false, false, "Supplier Type", "", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var supplierTypes = Storage.Instance.Orders.Select(x => x.SupplierType).Distinct();

                            Invoke(new MethodInvoker(delegate { FirstChoice.Items.AddRange(supplierTypes.ToArray()); }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_SUPPLIER_TYPE_AND_STORE:
                    {
                        SetUpChoices(true, true, false, "Store", "Supplier Type", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var supplierTypes = Storage.Instance.Orders.Select(x => x.SupplierType).Distinct();

                            var stores = Storage.Instance.Stores.Values.Select(x => x.StoreLocation);

                            Invoke(new MethodInvoker(delegate
                            {
                                FirstChoice.Items.AddRange(stores.ToArray());
                                SecondChoice.Items.AddRange(supplierTypes.ToArray());
                            }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_WEEK:
                    {
                        SetUpChoices(true, false, false, "Week/ Year", "", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var week = Storage.Instance.Dates.Select(x => x.ToString());

                            Invoke(new MethodInvoker(delegate { FirstChoice.Items.AddRange(week.ToArray()); }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_WEEK_AND_STORE:
                    {
                        SetUpChoices(true, true, false, "Store", "Week/ Year", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var week = Storage.Instance.Dates.Select(x => x.ToString());

                            var stores = Storage.Instance.Stores.Values.Select(x => x.StoreLocation);

                            Invoke(new MethodInvoker(delegate
                            {
                                FirstChoice.Items.AddRange(stores.ToArray());
                                SecondChoice.Items.AddRange(week.ToArray());
                            }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_WEEK_AND_SUPPLIER_TYPE:
                    {
                        SetUpChoices(true, true, false, "Supplier Type", "Week/ Year", "");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var week = Storage.Instance.Dates.Select(x => x.ToString());

                            var supplierTypes = Storage.Instance.Orders.Select(x => x.SupplierType).Distinct();

                            Invoke(new MethodInvoker(delegate
                            {
                                FirstChoice.Items.AddRange(supplierTypes.ToArray());
                                SecondChoice.Items.AddRange(week.ToArray());
                            }));
                        });

                        break;
                    }
                case (int)QueryChoiceEnum.COST_BY_WEEK_AND_SUPPLIER_TYPE_AND_STORE:
                    {
                        SetUpChoices(true, true, true, "Store", "Supplier Type", "Week/ Year");

                        SetChoicesTask = Task.Run(() =>
                        {
                            var week = Storage.Instance.Dates.Select(x => x.ToString());

                            var stores = Storage.Instance.Stores.Values.Select(x => x.StoreLocation);

                            var supplierTypes = Storage.Instance.Orders.Select(x => x.SupplierType).Distinct();


                            Invoke(new MethodInvoker(delegate
                            {
                                FirstChoice.Items.AddRange(stores.ToArray());
                                SecondChoice.Items.AddRange(supplierTypes.ToArray());
                                ThirdChoice.Items.AddRange(week.ToArray());
                            }));
                        });

                        break;
                    }
            }

        }

        void SetUpChoices(bool first, bool second, bool third, string firstLabel, string secondLabel, string thirdLabel)
        {
            FirstChoice.Visible = first;
            SecondChoice.Visible = second;
            ThirdChoice.Visible = third;
            First_Choice_lbl.Visible = first;
            Second_Choice_lbl.Visible = second;
            Third_Choice_lbl.Visible = third;

            First_Choice_lbl.Text = firstLabel;
            Second_Choice_lbl.Text = secondLabel;
            Third_Choice_lbl.Text = thirdLabel;

            FirstChoice.Items.Clear();
            SecondChoice.Items.Clear();
            ThirdChoice.Items.Clear();

            FirstChoice.ResetText();
            SecondChoice.ResetText();
            ThirdChoice.ResetText();
        }

        private void updateStoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                StorageManager.Instance.UpdateStores(true);
            });
            t.Start();

        } //Update Stores Menu Option

        private void updateDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                StorageManager.Instance.UpdateDates(true);
            });
        } //Update Dates Menu Option

        private void updateOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                StorageManager.Instance.UpdateOrders(true);
            });
        } //Update Orders Menu Option

        private void Show_Results_Btn_Click(object sender, EventArgs e)
        {
            First_Chart.Series.Clear();
            QueryResultBox.Items.Clear();

            Task.Run(() =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    First_Chart.Visible = false;
                    Chart_Loading_Label.Visible = true;
                    Show_Results_Btn.Enabled = false;
                    First_Chart.Titles.Clear();
                }));

                while (!ChartTask.IsCompleted)
                {
                    Thread.Sleep(1000);
                }

                if (ChartTask.IsCompleted)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        Show_Results_Btn.Enabled = true;
                        First_Chart.Visible = true;
                        Chart_Loading_Label.Visible = false;
                    }));
                }

            });

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (CalcTask == null || CalcTask.IsCompleted || ChartTask == null || ChartTask.IsCompleted)
                switch (MainChoice)
                {

                    case (int)QueryChoiceEnum.COST_BY_ALL_ORDERS:
                        {
                            double sum = 0;

                            //Calculation the actual query task
                            CalcTask = Task.Run(() =>
                                {
                                    sum = Storage.Instance.Orders
                                    .Select(x => x.Cost)
                                    .Sum(x => Convert.ToDouble(x));

                                    Invoke(new MethodInvoker(delegate
                                    {
                                        QueryResultBox.Items.Add(String.Format("Total sum of all orders in the database: {0:C2}", sum));

                                        stopWatch.Stop();

                                        QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                    }));
                                });

                            //Displaying the graph task
                            ChartTask = Task.Run(() =>
                            {

                                var OrdersByYears = Storage.Instance.Orders
                                .GroupBy(x => x.Date.Year);

                                Dictionary<int, double> YearsAndCosts = new Dictionary<int, double>();
                                foreach (var orderSet in OrdersByYears)
                                {
                                    double sumOrder = 0;
                                    foreach (var order in orderSet)
                                    {
                                        sumOrder += order.Cost;
                                    }
                                    YearsAndCosts.Add(orderSet.Key, sumOrder);
                                }

                                foreach (var cost in YearsAndCosts)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0].ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Years General Revenue Comparisment");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Name = "Years";
                                    First_Chart = changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });


                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_STORE:
                        {
                            double sum = 0;

                            string StoreName = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.Store.StoreLocation == StoreName)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} in the database: {1:C2}",
                                        FirstChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));
                            });

                            ChartTask = Task.Run(() =>
                            {
                                var StoresMoney = Storage.Instance.Stores
                                            .GroupBy(x => x.Key);

                                Dictionary<string, double> StoresAndMoney = new Dictionary<string, double>();

                                foreach (var storeSet in StoresMoney)
                                {
                                    sum = Storage.Instance.Orders
                                                    .Where(x => x.Store.StoreCode == storeSet.Key)
                                                    .Select(x => x.Cost)
                                                    .Sum(x => Convert.ToDouble(x));

                                    StoresAndMoney.Add(storeSet.Key, sum);
                                    if (StoresAndMoney.Count > 5)
                                        break;
                                }

                                foreach (var cost in StoresAndMoney)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0].ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Stores Revenue Comparisment");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Title = "Store Code";
                                    First_Chart = changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_SUPPLIER:
                        {
                            double sum = 0;

                            string SupplierName = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.SupplierName == SupplierName)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} in the database: {1:C2}",
                                        FirstChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            ChartTask = Task.Run(() =>
                            {
                                var StoresMoney = Storage.Instance.Orders
                                            .GroupBy(x => x.SupplierName);

                                Dictionary<string, double> StoresAndMoney = new Dictionary<string, double>();

                                foreach (var storeSet in StoresMoney)
                                {
                                    sum = Storage.Instance.Orders
                                                    .Where(x => x.SupplierName == storeSet.Key)
                                                    .Select(x => x.Cost)
                                                    .Sum(x => Convert.ToDouble(x));

                                    StoresAndMoney.Add(storeSet.Key, sum);

                                    if (StoresAndMoney.Count > 5)
                                        break;
                                }

                                foreach (var cost in StoresAndMoney)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = Math.Truncate(First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0]).ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Suppliers Revenue Comparisment");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Title = "Supplier";
                                    First_Chart = changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_SUPPLIER_TYPE:
                        {
                            double sum = 0;

                            string SupplierType = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.SupplierType == SupplierType)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} in the database: {1:C2}",
                                        FirstChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            ChartTask = Task.Run(() =>
                            {
                                var StoresMoney = Storage.Instance.Orders
                                            .GroupBy(x => x.SupplierType);

                                Dictionary<string, double> StoresAndMoney = new Dictionary<string, double>();

                                foreach (var storeSet in StoresMoney)
                                {
                                    sum = Storage.Instance.Orders
                                                    .Where(x => x.SupplierType == storeSet.Key)
                                                    .Select(x => x.Cost)
                                                    .Sum(x => Convert.ToDouble(x));

                                    StoresAndMoney.Add(storeSet.Key, sum);

                                    if (StoresAndMoney.Count > 5)
                                        break;
                                }

                                foreach (var cost in StoresAndMoney)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = Math.Truncate(First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0]).ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Supplier Types Revenue Comparisment");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Title = "Supplier Type";
                                    changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_SUPPLIER_TYPE_AND_STORE:
                        {
                            double sum = 0;

                            string SupplierType = SecondChoice.Text;
                            string StoreName = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.SupplierType == SupplierType)
                                .Where(x => x.Store.StoreLocation == StoreName)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} in {1} in the database: {2:C2}",
                                        FirstChoice.Text, SecondChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));
                            });

                            ChartTask = Task.Run(() =>
                            {
                                var StoresMoney = Storage.Instance.Orders
                                            .GroupBy(x => x.SupplierType);

                                Dictionary<string, double> StoresAndMoney = new Dictionary<string, double>();

                                foreach (var storeSet in StoresMoney)
                                {
                                    sum = Storage.Instance.Orders
                                                    .Where(x => x.SupplierType == storeSet.Key)
                                                    .Select(x => x.Cost)
                                                    .Sum(x => Convert.ToDouble(x));

                                    StoresAndMoney.Add(storeSet.Key, sum);

                                    if (StoresAndMoney.Count > 5)
                                        break;
                                }

                                foreach (var cost in StoresAndMoney)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = Math.Truncate(First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0]).ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Supplier Types + Store Revenue comparison");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Name = "Supplier Type for Store";
                                    First_Chart = changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });
                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_WEEK:
                        {
                            double sum = 0;

                            string Week = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.Date.ToString() == Week)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0}week in the database: {1:C2}",
                                        FirstChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            ChartTask = Task.Run(() =>
                            {
                                var StoresMoney = Storage.Instance.Orders
                                            .GroupBy(x => x.Date);

                                Dictionary<string, double> StoresAndMoney = new Dictionary<string, double>();

                                foreach (var storeSet in StoresMoney)
                                {
                                    sum = Storage.Instance.Orders
                                                    .Where(x => x.Date == storeSet.Key)
                                                    .Select(x => x.Cost)
                                                    .Sum(x => Convert.ToDouble(x));

                                    StoresAndMoney.Add(storeSet.Key.ToString(), sum);

                                    if (StoresAndMoney.Count > 5)
                                        break;
                                }

                                foreach (var cost in StoresAndMoney)
                                {
                                    Invoke(new MethodInvoker(delegate
                                    {
                                        First_Chart.Series.Add(cost.Key.ToString());
                                        First_Chart.Series[cost.Key.ToString()].IsValueShownAsLabel = false;
                                        First_Chart.Series[cost.Key.ToString()]["PointWidth"] = "1";
                                        First_Chart.Series[cost.Key.ToString()].Points.AddXY(cost.Key.ToString(), cost.Value);
                                        First_Chart.Series[cost.Key.ToString()].Points[0].Label = Math.Truncate(First_Chart.Series[cost.Key.ToString()].Points[0].YValues[0]).ToString();
                                    }));
                                }

                                Invoke(new MethodInvoker(delegate
                                {
                                    First_Chart.Titles.Add("Date General Revenue comparison");
                                    First_Chart.AlignDataPointsByAxisLabel();
                                    First_Chart.Legends[0].Name = "Date";
                                    First_Chart = changeYScala(First_Chart);
                                    First_Chart.Invalidate();
                                }));
                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_WEEK_AND_STORE:
                        {
                            double sum = 0;

                            string Date = SecondChoice.Text;
                            string StoreName = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.Date.ToString() == Date)
                                .Where(x => x.Store.StoreLocation == StoreName)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} in {1}week in the database: {2:C2}",
                                        FirstChoice.Text, SecondChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_WEEK_AND_SUPPLIER_TYPE:
                        {
                            double sum = 0;

                            string Date = SecondChoice.Text;
                            string SupplierType = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.Date.ToString() == Date)
                                .Where(x => x.SupplierType == SupplierType)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for supplier type of {0} in {1}week in the database: {2:C2}",
                                        FirstChoice.Text, SecondChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            break;
                        }
                    case (int)QueryChoiceEnum.COST_BY_WEEK_AND_SUPPLIER_TYPE_AND_STORE:
                        {
                            double sum = 0;

                            string Date = ThirdChoice.Text;
                            string SupplierType = SecondChoice.Text;
                            string StoreName = FirstChoice.Text;

                            CalcTask = Task.Run(() =>
                            {
                                sum = Storage.Instance.Orders
                                .Where(x => x.Store.StoreLocation == StoreName)
                                .Where(x => x.SupplierType == SupplierType)
                                .Where(x => x.Date.ToString() == Date)
                                .Select(x => x.Cost)
                                .Sum(x => Convert.ToDouble(x));

                                Invoke(new MethodInvoker(delegate
                                {
                                    QueryResultBox.Items.Add(String.Format("Total sum of orders for {0} and supplier type of {1} in {2}week in the database: {3:C2}",
                                        FirstChoice.Text, SecondChoice.Text, ThirdChoice.Text, sum));

                                    stopWatch.Stop();

                                    QueryResultBox.Items.Add("\nTime of this query: " + stopWatch.Elapsed.TotalSeconds + "s");
                                }));

                            });

                            break;
                        }

                }
            else
                MessageBox.Show("Wait for your previous query to generate the results");
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.Hide();

            UpdateDataThread.Start();

            ParameterizedThreadStart loading = new ParameterizedThreadStart(delegate
            {
                Loading window = null;

                Task.Run(() => Invoke(new MethodInvoker(delegate { window = CreateLoadingWindow(); })));


                UpdateDataThread.Join();

                Invoke(new MethodInvoker(window.Dispose));
                Invoke(new MethodInvoker(this.Show));
            });

            new Thread(loading).Start();
        }
        private Loading CreateLoadingWindow()
        {
            Loading window = new Loading();

            window.Show();

            return window;
        }
        private void storeDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.InitialDirectory = Directory.GetCurrentDirectory();
                fbd.Title = "Choose Store Codes File";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    StorageManager.Instance.ChangeStoreCodesFile(fbd.FileName);
                }
            }
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Choose Folder that contains all of the Orders";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    StorageManager.Instance.ChangeOrdersFolder(fbd.SelectedPath);
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Dispose();
        }

        private static string MakeLine(int j)
        {
            string line = "";
            for (int i = 0; i < j; i++)
            {
                line += "‾";
            }
            return line;
        }
        private Chart changeYScala(object myChart)
        {
            Chart chart = (Chart)myChart;

            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;

            double max = Double.MinValue;
            double min = Double.MaxValue;

            double leftLimit = chart.ChartAreas[0].AxisX.Minimum;
            double rightLimit = chart.ChartAreas[0].AxisX.Maximum;

            for (int s = 0; s < chart.Series.Count; s++)
            {
                foreach (DataPoint dp in chart.Series[s].Points)
                {
                    if (dp.XValue >= leftLimit && dp.XValue <= rightLimit)
                    {
                        min = Math.Min(min, dp.YValues[0]);
                        max = Math.Max(max, dp.YValues[0]);
                    }
                }
            }

            chart.ChartAreas[0].AxisY.Maximum = max;
            chart.ChartAreas[0].AxisY.Minimum = min;

            return chart;
        }
    }

}
