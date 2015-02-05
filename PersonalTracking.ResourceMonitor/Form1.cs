using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTracking.ResourceMonitor
{
    public partial class Form1 : Form
    {

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        List<float> ListCpu = new List<float>();
        List<float> ListRam = new List<float>();
        DateTime startDate;
        DateTime endDate;
        bool flag = true;
        public Form1()
        {
            InitializeComponent();

            cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            Timer timer = new Timer();
            timer.Tick += Timer1_Tick;
            timer.Interval = 250;
            timer.Start();
            startDate = DateTime.Now;
            this.ShowInTaskbar = true;
            this.Hide();
        }


        public float getRam()
        {
            return ramCounter.NextValue();
        }


        private void Timer1_Tick(Object sender, EventArgs e)
        {
            if (!flag) return;
            float cpuPercent = cpuCounter.NextValue();
            float ram = ramCounter.NextValue();
            ListCpu.Add(cpuPercent);
            ListRam.Add(ram);
            if (ListCpu.Count >= 2400)
            {
                flag = false;
                endDate = DateTime.Now;
                float avg = ListCpu.Average(x => x);
                float avgMem = ListRam.Average(x => x);
                ListCpu.Clear();
                ListRam.Clear();
                Save(startDate, (double)avg, (double)avgMem, endDate);
                flag = true; startDate = DateTime.Now;
            }
        }

        private void Save(DateTime startDate, double cpu, double memory, DateTime endDate)
        {
            StorageRepository.Clients.ResouceMonitorClient client = new StorageRepository.Clients.ResouceMonitorClient();
            client.InsertOrReplace(new StorageRepository.Models.ResourceLog(startDate)
                {
                    CpuUsage = cpu,
                    EndDate = endDate,
                    FreeMemory = memory
                });
        }
    }
}
