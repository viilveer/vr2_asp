using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class PostTag
    {

        public int PostTagId { get; set; }

        public virtual BlogPost PostId { get; set; }

        public virtual MultiLangString PostTagValue { get; set; }

        public string PostTagCreatedBy { get; set; }

        public DateTime PostTagCreatedAt { get; set; }

        public String PostTagUpdatedBy { get; set; }
        public DateTime PostTagUpdatedAt { get; set; }
    }
}
