using System;
using System.Linq;
using VPM.Models;

namespace VPM.Services
{
    public class ProjectServices
    {
        internal void SumBillableTime(Project project)
        {
            #region Billables 

            TimeSpan outPutSpan = new TimeSpan();
            decimal outPutProjectCost = 0;
            if (project.Task.Any())
            {
                foreach (Task task in project.Task)
                {
                    TimeSpan taskTime = TimeSpan.Parse(task.BillableTime?.ToString("HH:mm"));
                    decimal? taskCost = Convert.ToDecimal(taskTime.TotalHours) * task.CostPerHour;

                    outPutSpan += taskTime;
                    outPutProjectCost += taskCost.Value;
                }
                project.TotalBillableTime = Math.Truncate(outPutSpan.TotalHours).ToString("00") + ":" + outPutSpan.Minutes.ToString("00");
                project.TotalProjectCost = Math.Round(outPutProjectCost, 4).ToString();

            }
            else
            {
                project.TotalBillableTime = "00:00";
                project.TotalProjectCost = "0";
            }
            #endregion


        }

        internal void SumCustomerBillables(Customer customer)
        {
            TimeSpan outPutTime = new TimeSpan();
            decimal outPutCost = 0;
            if (customer.ClientProjects.Any())
            {
                foreach (Project project in customer.ClientProjects)
                {
                    TimeSpan time = new TimeSpan(int.Parse(project.TotalBillableTime.Split(':')[0]), int.Parse(project.TotalBillableTime.Split(':')[1]), 0);
                    decimal cost = Convert.ToDecimal(project.TotalProjectCost);

                    outPutTime += time;
                    outPutCost += cost;
                }

                customer.TotalCustomerBillableTime = Math.Truncate(outPutTime.TotalHours).ToString("00") + ":" + outPutTime.Minutes.ToString("00");
                customer.TotalCustomerProjectCost = Math.Round(outPutCost, 4).ToString();
            }
        }
    }
}
