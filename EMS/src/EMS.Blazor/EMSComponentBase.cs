using EMS.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EMS.Blazor;

public abstract class EMSComponentBase : AbpComponentBase
{
    protected EMSComponentBase()
    {
        LocalizationResource = typeof(EMSResource);
    }
}
