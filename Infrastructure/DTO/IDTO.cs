
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO
{
    public interface IDTO<in TModel>
    {
        void MapFromModel(TModel model);
    }
}
