using System;
using System.Collections.Generic;
using System.Text;

namespace SwapShuffle.Model
{
    public class order
    {
        public long Pid { get; set; }

        public long Sid { get; set; }

        public long Puid { get; set; }
		
		public decimal final_price { get; set; }
		
		public DateTime datetime { get; set; }

    }
}
