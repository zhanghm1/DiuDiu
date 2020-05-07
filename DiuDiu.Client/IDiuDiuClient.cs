using DiuDiu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiuDiu
{
    public interface IDiuDiuClient
    {
        Task<bool> RegisterService(DiuDiuService service);

        Task<IEnumerable<DiuDiuService>> GetService();
    }
}
