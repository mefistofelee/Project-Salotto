///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//





namespace Salotto.Shared.Exceptions
{
    public class PermissionDeniedException : MaximoException 
    {
        public PermissionDeniedException(string message) : base(message)
        {
        }
    }
}