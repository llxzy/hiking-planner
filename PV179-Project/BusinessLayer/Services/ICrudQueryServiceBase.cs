using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface ICrudQueryServiceBase<TDto>
    {
        public Task<TDto> GetAsync(int id);

        public Task CreateAsync(TDto entityDto);

        public void Update(TDto entityDto);

        public Task DeleteAsync(int id);
    }
}
