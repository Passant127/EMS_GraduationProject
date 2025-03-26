using EMS.DTO;
using EMS.Entities;
using EMS.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMS.Services
{
    public class ContactUsAppService:
    CrudAppService<
     ContactUs, // The Entity
     ContactUsDto, // Used to show entities
     int, // Primary Key
     PagedAndSortedResultRequestDto, // Paging and sorting
     CreateUpdateContactUsDto>, // Used to create/update
     IContactUsAppService
    {
        public ContactUsAppService(IRepository<ContactUs, int> repository)
            : base(repository)
    {
    }
}
}
