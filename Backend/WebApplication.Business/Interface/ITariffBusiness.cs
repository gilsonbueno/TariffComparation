using System.Collections.Generic;
using WebApplication.Model;

namespace WebApplication.Business.Interface
{
    public interface ITariffBusiness
    {
        List<ConsumeModel> Compare(int consumption);
    }
}
