using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Entities
{
    public class HealthRecord: BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }
        public string? Note { get; set; }
        //Updatedat at baseEntity class= LastUpdate For HealthRecord=>by Configuraion
        //ignore CreatedAt for HealthRecord=>by Configuraion
        
    }
}
