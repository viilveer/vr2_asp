using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
    }


}