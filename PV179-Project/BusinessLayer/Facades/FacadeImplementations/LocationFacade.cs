using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Facades.FacadeInterfaces;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class LocationFacade : FacadeBase, ILocationFacade
    {
        private readonly ILocationService locationService;
        public LocationFacade(IUnitOfWorkProvider provider, ILocationService locService) : base(provider)
        {
            locationService = locService;
        }

        public async Task Update(LocationDto locationDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                //nejaky check?
                locationService.Update(locationDto);
                await uow.CommitAsync();
            }
        }
    }
}
