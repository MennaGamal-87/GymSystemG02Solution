using GymSystemG02DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Entities
{
    public class Trainer: GymUser
    {
        //CreatedAt at baseEntity class= HireDate For Trainer=>by Configuraion
            public Specialites Specialites { get; set; }

        #region 1:M RS between session trainer
        public ICollection<Session> TrainerSessions { get; set; }
        #endregion
    }
}
