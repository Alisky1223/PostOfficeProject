using CommonDll.Dto;
namespace PostOfficeFrontendProject__all_interactive.Mapper
{
    public static class ValueTypesMapper
    {
        //correctors
        public static string ProductsTypeToString(this ProductTypeDto productType)
        {
            if (productType != null)
            {
                return productType.Type;

            }
            else
            {
                return "";
            }
        }     
        public static string ProductStatusToString(this TransportStatusDto transportStatus)
        {
            if (transportStatus != null)
            {
                return transportStatus.Status;

            }
            else
            {
                return "";
            }
        }
    }
}

