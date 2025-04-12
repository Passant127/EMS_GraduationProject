using EMS.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.IServices
{
    public interface IContactUsAppService:
   ICrudAppService<
     ContactUsDto,
     int,
     PagedAndSortedResultRequestDto,
     CreateUpdateContactUsDto>
    {
    }


}
