///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using Salotto.Shared.Exceptions;

namespace Salotto.App.Models.System
{
    public class ErrorMainViewModel : SimpleViewModelBase
    {
        public MaximoException Error { get; private set; }

        public string Path { get; set; }

        public void SetError(Exception error)
        {
            if (error is MaximoException exception)
                Error = exception;
            else
                Error = new MaximoException(error);
        }

        public bool HasError()
        {
            return Error != null;
        }
    }
}
