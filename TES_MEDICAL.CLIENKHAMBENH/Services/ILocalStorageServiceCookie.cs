using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.CLIENKHAMBENH.Services
{
    public interface ILocalStorageServiceCookie
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
        Task ShowModal(string id);
        Task CloseModal(string id);

    }
}
