using System;

namespace Domain.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetAllMsgs(this Exception ex)
        {
            var InnerEx = ex;
            string MSG = ex.Message;
            while (InnerEx.InnerException != null)
            {
                InnerEx = InnerEx.InnerException;
                MSG += " -> " + InnerEx.Message;
            }
            return MSG;
        }
    }
}