using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Repozitories
{
    public interface IRepozitory<TEntity> where TEntity: class
    {
        TEntity GetItemById(Guid id);
        void Save(TEntity item);

        void Delete(TEntity item);

    }
}
