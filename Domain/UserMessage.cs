﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class UserMessage
    {

        public int UserMessageId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        public virtual UserInt Receiver { get; set; }

        [Required]
        public virtual MultiLangString Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [MaxLength(255)]
        public String UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
