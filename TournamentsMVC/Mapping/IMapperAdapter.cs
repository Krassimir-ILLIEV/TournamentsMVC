using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentsMVC.Mapping
{
    public interface IMapperAdapter
    {
        TDestination Map<TDestination>(object source);
    }
}
