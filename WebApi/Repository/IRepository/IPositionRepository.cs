using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.IRepository
{
    public interface IPositionRepository
    {
        bool insertPosition(PositionModel position);

        List<PositionModel> listPositions();

        PositionModel viewPosition(int id);

        PositionModel modifyPosition(PositionModel position);

        bool deletePosition(int id);
    }
}
