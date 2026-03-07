using GymSystemG02BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02BLL.Services.Interfaces
{
    public interface IAnalyticsService
    {
       AnalyticsViewModel GetAnalyticsData();
    }
}
