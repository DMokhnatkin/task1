
namespace Infrastructure.Contract
{
    public interface IDto<TBusinessObject>
    {
        /// <summary>
        /// Convert dto to business object
        /// </summary>
        TBusinessObject ToBo();
    }
}
