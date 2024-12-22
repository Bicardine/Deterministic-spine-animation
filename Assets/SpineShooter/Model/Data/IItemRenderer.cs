//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
namespace SpineShooter.Model.Data
{
    interface IItemRenderer<TDataType>
    {
        void Render(TDataType data);
    }
}