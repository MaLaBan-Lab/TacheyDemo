using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel
{
    public class GroupOrderRecord
    {
        
       
        public IEnumerable<OrderRecordSuccess> Success { get; set; }
        public IEnumerable<OrderRecordOther> Other { get; set; }

      
    }
}