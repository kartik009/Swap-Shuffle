using System;

namespace SwapShuffle.Model
{
    public class Product
    {
        public long Pid { get; set; }

        public string Name { get; set; }

        public long Cid { get; set; }

        public long Uid { get; set; }

        public string p_images { get; set; }
        
        public string p_description { get; set; }

        public decimal price { get; set; }
		
		public bool p_status { get; set; }
		
		public DateTime p_datetime{ get; set; }
		
    }
}
