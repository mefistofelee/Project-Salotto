///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//




namespace Salotto.Infrastructure.Services
{
    public interface IFileService
    {
        string Load(string path, string iso = null, bool removeComments = true);
        void Save(string path, string content);
    }
}