using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.Core.Entity
{
    /// <summary>
    /// BaseEntity sınıfı, tüm entity sınıflarının ortak özelliklerini içerir.
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
