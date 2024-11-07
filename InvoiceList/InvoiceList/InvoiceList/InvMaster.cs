using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceList
{
    public class InvMaster
    {
        public string InvDate { get; set; }
        public decimal Total { get; set; }
        public void SaveMasterRec(InvMaster inv)
        {
            string sql = "Insert into InvMaster (InvDate, Total) values('" + inv.InvDate + "','" + inv.Total + "')";
            Data.executesql(sql);
        }
    }
}
