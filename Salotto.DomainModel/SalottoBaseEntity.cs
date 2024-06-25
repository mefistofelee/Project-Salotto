///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;

namespace Salotto.DomainModel
{
    public class SalottoBaseEntity
    {
        public SalottoBaseEntity()
        {
            Active = true;
            Deleted = false;
        }

        public bool Active { get; set; }

        /// <summary>
        /// Indicates whether the entity was previously deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Latest update
        /// </summary>
        public RecordTimeStamp TimeStamp { get; set; }

        /// <summary>
        /// Whether the record is deleted or not
        /// </summary>
        /// <returns></returns>
        public bool IsDeleted()
        {
            return Deleted;
        }

        /// <summary>
        /// Marks for deletion
        /// </summary>
        public void SoftDelete()
        {
            Deleted = true;
        }

        /// <summary>
        /// Logs time stamp
        /// </summary>
        public void Mark(bool isEdit, string? author = null)
        {
            if (isEdit)
            {
                TimeStamp?.Mark(author);
            }
            else
            {
                TimeStamp = new RecordTimeStamp();
                TimeStamp.Init(author);
            }

        }
    }
}