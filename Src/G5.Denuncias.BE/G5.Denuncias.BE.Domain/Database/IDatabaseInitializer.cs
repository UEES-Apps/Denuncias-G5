using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Database
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
