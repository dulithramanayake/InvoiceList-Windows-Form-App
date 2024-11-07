using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceList
{
    public class InvTrans
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal value { get; set; }

        public static int SaveTranRec(List<InvTrans> invList)
        {
            string sql = "SELECT IDENT_CURRENT('InvMaster')as InvNo";
            int InvNo = Data.ExecuteDataReader(sql);

            foreach (InvTrans inv in invList)
            {
                inv.value = inv.Price * inv.Quantity;
                sql = "insert into InvTrans(InvNo,ItemCode,Description,Price,Quantity,Value) Values(" + InvNo +
                    ",'" + inv.ItemCode + "','" + inv.Description + "'," + inv.Price + "," + inv.Quantity + "," + inv.value + ")";
                Data.executesql(sql);
            }
            return InvNo;
        }
    }

    
}
