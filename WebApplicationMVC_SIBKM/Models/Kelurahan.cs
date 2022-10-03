using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC_SIBKM.Models
{
    public class Kelurahan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public Kecamatan Kecamatan { get; set; }
        [ForeignKey("Kecamatan")]
        public int KecamatanId { get; set; }
    }
}
