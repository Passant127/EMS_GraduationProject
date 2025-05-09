﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EMS.Blazor;

[Dependency(ReplaceServices = true)]
public class EMSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EMS";
}
