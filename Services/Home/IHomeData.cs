using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Home
{
    public interface IHomeData
    {
        Task<int> LoginUser(string? name, string? password);

        Task<bool> LogoutUser(string? name, string? password);
    }
}
