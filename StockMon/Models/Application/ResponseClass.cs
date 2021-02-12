using System;
namespace StockMon.Models.Application
{
    public class ResponseClass
    {
        public enum eStates
        {
            valid, error
        }

        private eStates State;
        public bool IsValid { get { return State.Equals(eStates.valid); } }
        private string Message;
        private int ErrorCode;

        public ResponseClass(string SuccessMessage)
        {
            this.State = eStates.valid;
            this.Message = SuccessMessage;
        }

        public ResponseClass(bool isError, int ErrCode, string ErrMessage)
        {
            this.State = eStates.error;
            this.ErrorCode = ErrCode;
            this.Message = ErrMessage;
        }

        public string GetValidResponse()
        {
            return Message;
        }

        public (int errorCode, string errorMessage) GetErrorResponse()
        {
            return (ErrorCode, Message);
        }
    }
}
