using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    interface ILocationFacade : IDisposable
    {
        void Update(LocationDto locationDto);
    }
}
