using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class PostTag
    {

        public int PostTagId { get; set; }

        [Required]
        public virtual BlogPost PostId { get; set; }

        [Required]
        public virtual MultiLangString PostTagValue { get; set; }

        [Required]
        [MaxLength(255)]
        public string PostTagCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostTagCreatedAt { get; set; }

        [MaxLength(255)]
        public String PostTagUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostTagUpdatedAt { get; set; }
    }
}
