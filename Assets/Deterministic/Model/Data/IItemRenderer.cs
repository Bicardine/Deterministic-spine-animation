//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
namespace Deterministic.Model.Data
{
    interface IItemRenderer<TDataType>
    {
        void Render(TDataType data);
    }
}