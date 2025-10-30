using PostOfficeFrontendProject__all_interactive.Interface;

namespace PostOfficeFrontendProject__all_interactive.Helper
{
    public class DataPassHelper : IDataPassHelper
    {
        //postOffice

        private int _postOfficeId;

        public int GetPostOfficeId() => _postOfficeId;

        public void SetPostOfficeId(int postOfficeId) => _postOfficeId = postOfficeId;     

        //product
        
        private int _proudctId;

        public int GetProudctId() => _proudctId;

        public void SetProudctId(int proudctId) => _proudctId = proudctId;
        
        //postMan
        
        private int _postManId;

        public int GetPostManId() => _postManId;

        public void SetPostManId(int postManId) => _postManId = postManId;

        //customer

        private int _customerId;

        public int GetCustomerId() => _customerId;

        public void SetCustomerId(int customerId) => _customerId = customerId;

    }
}
