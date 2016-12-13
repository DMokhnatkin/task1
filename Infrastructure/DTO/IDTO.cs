
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO
{
    public interface IDTO<TModel>
    {
        void MapFromModel(TModel model);
        TModel MapToModel();
    }
}
